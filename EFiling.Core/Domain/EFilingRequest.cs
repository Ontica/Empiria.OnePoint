/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Empiria BaseObject                      *
*  Type     : EFilingRequest                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an electronic filing request typically submitted to a government agency.            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Data;
using System.Threading.Tasks;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Security;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Represents an electronic filing request typically submitted to a government agency.</summary>
  public class EFilingRequest : BaseObject, IProtected {


    #region Constructors and parsers

    private EFilingRequest() {
      // Required by Empiria Framework
      this.ExternalServicesHandler = new EFilingExternalServicesInteractor(this);
    }


    public EFilingRequest(Procedure procedure, Requester requestedBy) : this() {
      Assertion.AssertObject(procedure, "procedure");
      Assertion.AssertObject(requestedBy, "requestedBy");

      this.ExtensionData = new JsonObject();

      this.Procedure = procedure;
      this.SetRequester(requestedBy);
      this.SetUserContextData();
    }


    static public EFilingRequest TryParse(string uid) {
      return BaseObject.TryParse<EFilingRequest>($"FilingRequestUID = '{uid}'");
    }


    static public FixedList<EFilingRequest> GetList(EFilingRequestStatus status,
                                                    string keywords, int count = -1) {
      return EFilingRequestData.GetList(status, keywords, count);
    }


    protected override void OnLoadObjectData(DataRow row) {
      var json = this.ExtensionData.Slice("requestedByData", false);

      this.RequestedBy.Load(json);

      if (this.TransactionUID.Length != 0 && this.Transaction.UID.Length == 0) {
        this.Transaction = new EFilingTransaction(this.ExtensionData.Get<string>("transaction/uid"));
      }

      if (this.Transaction.ExtensionData.Contains("paymentData")) {
        this.PaymentOrder = EFilingPaymentOrder.Parse(this.Transaction.ExtensionData.Slice("paymentData"));

      } else if (this.ExtensionData.Contains("paymentData/receiptNo")) {
        var receiptNo = this.ExtensionData.Get<string>("paymentData/receiptNo");

        this.PaymentOrder = new EFilingPaymentOrder();
        this.PaymentOrder.ReceiptNo = receiptNo;
      }
    }

    #endregion Constructors and parsers


    #region Public properties

    [DataField("ProcedureId")]
    public Procedure Procedure {
      get;
      private set;
    }


    [DataObject()]
    public Requester RequestedBy {
      get;
      private set;
    }


    [DataField("AgencyId")]
    public Organization Agency {
      get;
      private set;
    }


    [DataField("AgentId")]
    public Contact Agent {
      get;
      private set;
    }


    public JsonObject ApplicationForm {
      get {
        return ExtensionData.Slice("appForm", false);
      }
    }


    public DateTime AuthorizationTime {
      get {
        return this.ExtensionData.Get("authorizationTime", ExecutionServer.DateMaxValue);
      }
      private set {
        if (value != ExecutionServer.DateMaxValue) {
          this.ExtensionData.SetIfValue("authorizationTime", value);
        } else {
          this.ExtensionData.Remove("authorizationTime");
        }
      }
    }


    [DataField("RequestExtData")]
    protected internal JsonObject ExtensionData {
      get;
      private set;
    } = new JsonObject();


    public virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.RequestedBy.Name,
                                           this.Procedure.DisplayName);
      }
    }


    [DataField("RequestLastUpdate", Default = "ExecutionServer.DateMinValue")]
    public DateTime LastUpdate {
      get;
      private set;
    }


    [DataField("PostingTime")]
    public DateTime PostingTime {
      get;
      private set;
    }


    [DataField("PostedById")]
    public Contact PostedBy {
      get;
      private set;
    }


    [DataField("RequestStatus", Default = EFilingRequestStatus.Pending)]
    public EFilingRequestStatus Status {
      get;
      private set;
    }


    public string StatusName {
      get {
        switch (this.Status) {
          case EFilingRequestStatus.Pending:
            return "En elaboración";

          case EFilingRequestStatus.OnSign:
            return "En firma";

          case EFilingRequestStatus.OnPayment:
            return "Por pagar";

          case EFilingRequestStatus.Submitted:
            return "Ingresada";

          case EFilingRequestStatus.Finished:
            return "Finalizada";

          case EFilingRequestStatus.Rejected:
            return "Devuelta";

          case EFilingRequestStatus.Deleted:
            return "Eliminada";

          default:
            throw Assertion.AssertNoReachThisCode("Unrecognized electronic request status.");
        }
      }
    }


    public bool IsClosed {
      get {
        return (this.Status == EFilingRequestStatus.Finished ||
                this.Status == EFilingRequestStatus.Rejected);
      }
    }


    #endregion Public properties


    #region Electronic sign


    public bool IsSigned {
      get {
        return this.ElectronicSign.Length != 0;
      }
    }


    public string ElectronicSign {
      get {
        return ExtensionData.Get("esign/signature", String.Empty);
      }
    }


    private void EnsureCanBeSigned() {
      Assertion.Assert(!this.IsSigned, "This filing was already signed.");

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsSigner, "Current user can't sign this filing.");
    }


    private void EnsureCanRevokeSign() {
      Assertion.Assert(this.IsSigned, "This filing is not signed.");

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsSigner, "Current user can't revoke sign.");

      Assertion.Assert(userContext.User.Equals(this.Agent),
                      "Current user is not the same as this filing signer.");
    }


    internal string GetSecurityHash() {
      return Cryptographer.CreateHashCode(this.GetElectronicSeal(),
                                          this.UID)
                          .Substring(0, 8)
                          .ToUpperInvariant();
    }


    internal virtual string GetElectronicSeal() {
      var text = this.GetESignInputData();

      return Cryptographer.CreateHashCode(text, this.UID);
    }


    internal string GetESignInputData() {
      return EmpiriaString.BuildDigitalString(this.Id, this.UID, this.Procedure.Id, this.RequestedBy,
                                              this.Agency.Id, this.Agent.Id, this.AuthorizationTime,
                                              this.ApplicationForm.ToString());
    }


    internal void RevokeSign(JsonObject credentials) {
      Assertion.AssertObject(credentials, "credentials");

      this.EnsureCanRevokeSign();

      var requestSigner = new EFilingRequestSigner(this);

      requestSigner.RevokeSign(credentials);

      this.AuthorizationTime = ExecutionServer.DateMaxValue;

      this.ExtensionData.Remove("esign");

      this.Status = EFilingRequestStatus.Pending;
      this.LastUpdate = ExecutionServer.DateMinValue;
    }


    internal void SendToSign() {
      EnsureCanBeEdited();

      this.Status = EFilingRequestStatus.OnSign;
    }


    internal void Sign(JsonObject credentials) {
      Assertion.AssertObject(credentials, "credentials");

      EnsureCanBeSigned();

      var requestSigner = new EFilingRequestSigner(this);

      this.AuthorizationTime = DateTime.Now;

      JsonObject signData = requestSigner.Sign(credentials);
      this.ExtensionData.Set("esign", signData);

      this.LastUpdate = DateTime.Now;

      this.Status = EFilingRequestStatus.OnPayment;
    }

    #endregion Electronic sign


    #region Transaction data

    public bool HasTransaction {
      get {
        return this.TransactionUID.Length != 0 || this.Transaction.UID.Length != 0;
      }
    }


    [DataObject()]
    public EFilingTransaction Transaction {
      get;
      private set;
    }


    private string TransactionUID {
      get {
        return ExtensionData.Get("transaction/uid", String.Empty);
      }
    }


    internal async Task CreateTransaction() {
      Assertion.Assert(!this.HasTransaction, $"A transaction was already linked to this request.");

      var createdTransaction = await this.ExternalServicesHandler.CreateTransaction();

      this.Transaction = new EFilingTransaction(createdTransaction.UID);

      await this.Transaction.Synchronize(this.ExternalServicesHandler);
    }


    internal void RemoveOldTransactionData_REFACTORING() {
      Assertion.AssertObject(this.Transaction.UID, $"NEW TRANSACTION NOT LINKED.");

      // ExtensionData.Remove("transactionuid");
      ExtensionData.Remove("transaction");

      // ExtensionData.Remove("paymentData/receiptNo");
      ExtensionData.Remove("paymentData");

    }


    #endregion Transaction data


    #region Payment data

    public bool HasPaymentOrder {
      get {
        return this.PaymentOrder.RouteNumber.Length != 0;
      }
    }


    internal EFilingPaymentOrder PaymentOrder {
      get;
      private set;
    } = EFilingPaymentOrder.Empty;


    internal async Task CreatePaymentOrder() {
      var dto = await this.ExternalServicesHandler.GeneratePaymentOrder();

      this.PaymentOrder = new EFilingPaymentOrder(dto);
    }


    internal void SetPaymentReceipt(string receiptNo) {
      Assertion.AssertObject(receiptNo, "receiptNo");

      this.PaymentOrder.ReceiptNo = receiptNo;
    }


    #endregion Payment data


    #region Integrity validation

    int IProtected.CurrentDataIntegrityVersion {
      get {
        return 1;
      }
    }


    object[] IProtected.GetDataIntegrityFieldValues(int version) {
      if (version == 1) {
        return new object[] {
          1, "Id", this.Id, "UID", this.UID,
          "ProcedureId", this.Procedure.Id, "RequestedBy", this.RequestedBy.Name,
          "AgencyId", this.Agency.Id, "AgentId", this.Agent.Id,
          "ExtensionData", this.ExtensionData.ToString(), "LastUpdateTime", this.LastUpdate,
          "PostingTime", this.PostingTime, "PostedById", this.PostedBy.Id, "Status", (char) this.Status
        };
      }
      throw new SecurityException(SecurityException.Msg.WrongDIFVersionRequested, version);
    }


    private IntegrityValidator _validator;
    public IntegrityValidator Integrity {
      get {
        if (_validator == null) {
          _validator = new IntegrityValidator(this);
        }
        return _validator;
      }
    }


    #endregion Integrity validation


    #region Public Methods

    internal void Delete() {
      EnsureCanBeEdited();

      this.Status = EFilingRequestStatus.Deleted;
    }


    protected override void OnSave() {
      if (this.IsNew) {
        this.PostingTime = DateTime.Now;
      }

      this.Transaction.ExtensionData.SetIfValue("paymentData",
                                                this.PaymentOrder.ToJson());

      EFilingRequestData.WriteFilingRequest(this);
    }


    internal void SetApplicationForm(JsonObject json) {
      Assertion.AssertObject(json, "json");

      EnsureCanBeEdited();

      this.ExtensionData.Set("appForm", json);
    }


    internal void SetRequester(Requester requester) {
      Assertion.AssertObject(requester, "requester");

      EnsureCanBeEdited();

      this.RequestedBy = requester;

      this.ExtensionData.SetIfValue("requestedBy", requester.ToJson());
    }


    internal async Task Submit() {
      EnsureCanBeSubmitted();

      await this.ExternalServicesHandler.SetPayment();

      await this.ExternalServicesHandler.Submit();

      await this.UpdateStatus(EFilingRequestStatus.Submitted);
    }


    internal async Task Synchronize() {
      if (this.HasTransaction) {
        await this.Transaction.Synchronize(this.ExternalServicesHandler);
      }
      if (this.HasTransaction || this.HasPaymentOrder) {
        await this.PaymentOrder.Synchronize(this.ExternalServicesHandler);
      }
    }


    internal async Task UpdateStatus(EFilingRequestStatus newStatus) {
      this.Status = newStatus;

      if (StatusNeedsExternalDataSynchronization(newStatus)) {
        await this.Synchronize();
      }
    }


    #endregion Public methods


    #region Private methods

    private void EnsureCanBeEdited() {
      Assertion.Assert(!this.IsSigned, "This filing is already signed, so it can't be edited.");

      Assertion.Assert(this.Status == EFilingRequestStatus.Pending,
                       "This filing is not in pending status, so it can't be edited.");

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsRegister, "Current user can't edit this filing.");
    }


    private void EnsureCanBeSubmitted() {
      Assertion.Assert(this.Status == EFilingRequestStatus.OnPayment,
                       "Invalid status for submitting. Must be OnPayment");


      Assertion.AssertObject(this.PaymentOrder.ReceiptNo,
                             "No receipt number.");

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsManager, "Current user can't submit this filing.");
    }


    private EFilingExternalServicesInteractor ExternalServicesHandler {
      get;
    }


    private void SetUserContextData() {
      var userContext = EFilingUserContext.Current();

      this.PostedBy = userContext.User;
      this.Agency = userContext.Agency;
      this.Agent = userContext.Agent;
    }


    static private bool StatusNeedsExternalDataSynchronization(EFilingRequestStatus newStatus) {
      return (newStatus == EFilingRequestStatus.Finished ||
              newStatus == EFilingRequestStatus.Rejected ||
              newStatus == EFilingRequestStatus.Submitted ||
              newStatus == EFilingRequestStatus.OnPayment);
    }


    #endregion Private methods


  } // class EFilingRequest

} // namespace Empiria.OnePoint.EFiling
