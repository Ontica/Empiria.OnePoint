/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Empiria Object Type                     *
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

using Empiria.OnePoint.EPayments;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Represents an electronic filing request typically submitted to a government agency.</summary>
  public class EFilingRequest : BaseObject, IProtected {


    #region Fields

    static private readonly bool USE_PAYMENT_ORDER_MOCK_SERVICE =
                                                ConfigurationData.Get("UsePaymentOrderMockService", false);

    #endregion Fields


    #region Constructors and parsers

    private EFilingRequest() {
      // Required by Empiria Framework
    }


    public EFilingRequest(Procedure procedure, Requester requestedBy) {
      Assertion.AssertObject(procedure, "procedure");
      Assertion.AssertObject(requestedBy, "requestedBy");

      this.ExtensionData = new JsonObject();

      this.Procedure = procedure;
      this.SetRequester(requestedBy);
    }


    static public EFilingRequest TryParse(string uid) {
      return BaseObject.TryParse<EFilingRequest>($"FilingRequestUID = '{uid}'");
    }


    static public FixedList<EFilingRequest> GetList(EFilingRequestStatus status,
                                                    string keywords) {
      return EFilingRequestData.GetList(status, keywords);
    }


    protected override void OnLoadObjectData(DataRow row) {
      this.RequestedBy.email = this.ExtensionData.Get("requestedByData/email", String.Empty);
      this.RequestedBy.phone = this.ExtensionData.Get("requestedByData/phone", String.Empty);
      this.RequestedBy.rfc = this.ExtensionData.Get("requestedByData/rfc", String.Empty);
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
        return EmpiriaString.BuildKeywords(this.RequestedBy.name,
                                           this.Procedure.DisplayName);
      }
    }


    [DataField("LastUpdateTime", Default = "ExecutionServer.DateMinValue")]
    public DateTime LastUpdateTime {
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


    public string ElectronicSign {
      get {
        return ExtensionData.Get("esign/signature", String.Empty);
      }
    }


    public bool HasTransaction {
      get {
        return this.TransactionUID.Length != 0;
      }
    }


    public bool IsSigned {
      get {
        return this.ElectronicSign.Length != 0;
      }
    }


    public string TransactionUID {
      get {
        return ExtensionData.Get("transaction/uid", String.Empty);
      }
    }


    int IProtected.CurrentDataIntegrityVersion {
      get {
        return 1;
      }
    }

    object[] IProtected.GetDataIntegrityFieldValues(int version) {
      if (version == 1) {
        return new object[] {
          1, "Id", this.Id, "UID", this.UID,
          "ProcedureId", this.Procedure.Id, "RequestedBy", this.RequestedBy.name,
          "AgencyId", this.Agency.Id, "AgentId", this.Agent.Id,
          "ExtensionData", this.ExtensionData.ToString(), "LastUpdateTime", this.LastUpdateTime,
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


    #endregion Fields


    #region Public methods


    internal void CreateTransaction() {
      if (this.HasTransaction) {
        return;
      }

      var transactionProvider = this.Procedure.GetFilingTransactionProvider();

      var transaction = transactionProvider.CreateTransaction(this);

      ExtensionData.Set("transaction/uid", transaction.UID);
    }


    internal void UpdateTransaction() {
      var transactionProvider = this.Procedure.GetFilingTransactionProvider();

      transactionProvider.UpdateTransaction(this);
    }


    internal void Delete() {
      EnsureCanBeEdited();

      this.Status = EFilingRequestStatus.Deleted;
    }


    internal void Finish() {
      this.Status = EFilingRequestStatus.Finished;
    }


    internal async Task GeneratePaymentOrder() {
      if (!this.HasTransaction) {
        return;
      }

      var transactionProvider = this.Procedure.GetFilingTransactionProvider();

      var transaction = transactionProvider.GetTransactionAsPayable(this.TransactionUID);

      EPayments.PaymentOrderDTO paymentOrder = await GeneratePaymentOrder(transaction).ConfigureAwait(false);

      transactionProvider.SetPaymentOrder(transaction, paymentOrder);

      this.Status = EFilingRequestStatus.OnPayment;
    }


    private async Task<EPayments.PaymentOrderDTO> GeneratePaymentOrder(IPayable transaction) {
      try {
        if (!USE_PAYMENT_ORDER_MOCK_SERVICE) {
          return await EPaymentsUseCases.RequestPaymentOrderData(transaction)
                                        .ConfigureAwait(false);
        } else {
          return PaymentOrderMockData();
        }
      } catch (Exception e) {
        throw new InvalidCastException("No puedo generar la orden de pago", e);
      }
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


    internal EPayments.PaymentOrderDTO GetPaymentOrder() {
      Assertion.AssertObject(this.HasTransaction,
                             "This filing has not be linked to a transaction yet.");

      var transactionProvider = this.Procedure.GetFilingTransactionProvider();

      return transactionProvider.TryGetPaymentOrderData(this.TransactionUID);
    }


    internal string GetPaymentReceipt() {
      return this.ExtensionData.Get("paymentData/receiptNo", String.Empty);
    }


    internal string GetSecurityHash() {
      return Cryptographer.CreateHashCode(this.GetElectronicSeal(),
                                          this.UID)
                           .Substring(0, 8)
                           .ToUpperInvariant();
    }


    internal IFilingTransaction GetTransaction() {
      Assertion.AssertObject(this.HasTransaction,
                             "This filing has not be linked to a transaction yet.");

      var transactionProvider = this.Procedure.GetFilingTransactionProvider();

      return transactionProvider.GetTransaction(this.TransactionUID);
    }


    protected override void OnSave() {
      if (this.IsNew) {
        SetUserContextData();
        this.PostingTime = DateTime.Now;
      }

      EFilingRequestData.WriteFilingRequest(this);
    }


    internal void RevokeSign(JsonObject credentials) {
      Assertion.AssertObject(credentials, "credentials");

      this.EnsureCanRevokeSign();

      var requestSigner = new EFilingRequestSigner(this);

      requestSigner.RevokeSign(credentials);

      this.AuthorizationTime = ExecutionServer.DateMaxValue;

      this.ExtensionData.Remove("esign");

      this.Status = EFilingRequestStatus.Pending;
      this.LastUpdateTime = ExecutionServer.DateMinValue;
    }


    internal void SendToSign() {
      EnsureCanBeEdited();

      this.Status = EFilingRequestStatus.OnSign;
    }


    internal void SetApplicationForm(JsonObject json) {
      EnsureCanBeEdited();

      this.ExtensionData.Set("appForm", json);
    }


    internal void SetPaymentReceipt(string receiptNo) {
      this.ExtensionData.Set("paymentData/receiptNo", receiptNo);
    }


    internal void SetRequester(Requester requester) {
      EnsureCanBeEdited();

      this.RequestedBy = requester;

      this.ExtensionData.SetIfValue("requestedByData/email", requester.email);
      this.ExtensionData.SetIfValue("requestedByData/phone", requester.phone);
      this.ExtensionData.SetIfValue("requestedByData/rfc", requester.rfc);
    }


    internal void Sign(JsonObject credentials) {
      Assertion.AssertObject(credentials, "credentials");

      EnsureCanBeSigned();

      var requestSigner = new EFilingRequestSigner(this);

      this.AuthorizationTime = DateTime.Now;

      JsonObject signData = requestSigner.Sign(credentials);
      this.ExtensionData.Set("esign", signData);

      this.Status = EFilingRequestStatus.OnPayment;
      this.LastUpdateTime = DateTime.Now;
    }


    internal void Submit() {
      EnsureCanBeSubmitted();

      var transactionProvider = this.Procedure.GetFilingTransactionProvider();

      transactionProvider.SetPayment(this.TransactionUID, this.GetPaymentReceipt());

      transactionProvider.SubmitTransaction(this.TransactionUID);

      this.Status = EFilingRequestStatus.Submitted;
    }


    internal void Update(Requester requestedBy) {
      Assertion.AssertObject(requestedBy, "requestedBy");

      EnsureCanBeEdited();

      this.SetRequester(requestedBy);
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


    private void EnsureCanBeSigned() {
      Assertion.Assert(!this.IsSigned, "This filing was already signed.");

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsSigner, "Current user can't sign this filing.");
    }


    private void EnsureCanBeSubmitted() {
      if (this.HasTransaction) {
        return;
      }

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsManager, "Current user can't submit this filing.");
    }


    private void EnsureCanRevokeSign() {
      Assertion.Assert(this.IsSigned, "This filing is not signed.");

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsSigner, "Current user can't revoke sign.");

      Assertion.Assert(userContext.User.Equals(this.Agent),
                      "Current user is not the same as this filing signer.");
    }


    private EPayments.PaymentOrderDTO PaymentOrderMockData() {
      var routeNumber = EmpiriaString.BuildRandomString(16, 16);
      var controlTag = EmpiriaString.BuildRandomString(6, 6);

      return new OnePoint.EPayments.PaymentOrderDTO(routeNumber, DateTime.Today.AddDays(20), controlTag);
    }


    private void SetUserContextData() {
      var userContext = EFilingUserContext.Current();

      this.PostedBy = userContext.User;
      this.Agency = userContext.Agency;
      this.Agent = userContext.Agent;
    }


    #endregion Private methods


  } // class EFilingRequest

} // namespace Empiria.OnePoint.EFiling
