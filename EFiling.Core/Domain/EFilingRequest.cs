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

namespace Empiria.OnePoint.EFiling {

  /// <summary>Represents an electronic filing request typically submitted to a government agency.</summary>
  public class EFilingRequest : BaseObject {

    #region Constructors and parsers

    private EFilingRequest() {
      this.SecurityHandler = new RequestSigner(this);
      this.ExternalServicesHandler = new EFilingExternalServicesInteractor(this);
    }


    public EFilingRequest(Procedure procedure, RequesterDto requestedBy) : this() {
      Assertion.AssertObject(procedure, "procedure");
      Assertion.AssertObject(requestedBy, "requestedBy");

      this.ExtensionData = new JsonObject();

      this.Procedure = procedure;
      this.SetRequesterData(requestedBy);
      this.SetUserContextData();
    }


    static public EFilingRequest TryParse(string uid) {
      return BaseObject.TryParse<EFilingRequest>($"FilingRequestUID = '{uid}'");
    }


    static public FixedList<EFilingRequest> GetList(RequestStatus status,
                                                    string keywords, int count) {
      return EFilingRepository.GetList(status, keywords, count);
    }


    protected override void OnLoadObjectData(DataRow row) {
      this.LoadRequesterData();
      this.LoadPaymentData();
    }


    #endregion Constructors and parsers


    #region Public properties

    [DataField("ProcedureId")]
    public Procedure Procedure {
      get;
      private set;
    }


    [DataObject()]
    public RequesterDto RequestedBy {
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


    [DataField("RequestStatus", Default = RequestStatus.Pending)]
    public RequestStatus Status {
      get;
      private set;
    }


    public string StatusName {
      get {
        switch (this.Status) {
          case RequestStatus.Pending:
            return "En elaboración";

          case RequestStatus.OnSign:
            return "En firma";

          case RequestStatus.OnPayment:
            return "Por pagar";

          case RequestStatus.Submitted:
            return "Ingresada";

          case RequestStatus.Finished:
            return "Finalizada";

          case RequestStatus.Rejected:
            return "Devuelta";

          case RequestStatus.Deleted:
            return "Eliminada";

          default:
            throw Assertion.AssertNoReachThisCode("Unrecognized electronic request status.");
        }
      }
    }


    public bool IsClosed {
      get {
        return (this.Status == RequestStatus.Finished ||
                this.Status == RequestStatus.Rejected);
      }
    }


    #endregion Public properties

    #region Security members

    public DateTime AuthorizationTime {
      get {
        return this.ExtensionData.Get("authorizationTime", ExecutionServer.DateMaxValue);
      }
      private set {
        if (value != ExecutionServer.DateMaxValue) {
          this.ExtensionData.Set("authorizationTime", value);
        } else {
          this.ExtensionData.Remove("authorizationTime");
        }
      }
    }

    public bool IsSigned {
      get {
        return this.SecurityData.ElectronicSign.Length != 0;
      }
    }

    public SecurityData SecurityData {
      get {
        return this.SecurityHandler.SecurityData;
      }
    }


    private RequestSigner SecurityHandler {
      get;
    }


    internal void SendToSign() {
      this.EnsureCanBeEdited();

      this.Status = RequestStatus.OnSign;
    }


    internal void OnBeforeSign() {
      this.AuthorizationTime = DateTime.Now;
    }

    internal void Sign(JsonObject signInputData) {
      this.SecurityHandler.Sign(signInputData);
    }


    internal void RevokeSign(JsonObject revokeSignData) {
      this.SecurityHandler.RevokeSign(revokeSignData);
    }


    internal void OnSigned(JsonObject signData) {
      this.ExtensionData.Set("esign", signData);

      this.LastUpdate = DateTime.Now;
      this.Status = RequestStatus.OnPayment;
    }


    internal void OnSignRevoked() {
      this.AuthorizationTime = ExecutionServer.DateMaxValue;

      this.ExtensionData.Remove("esign");

      this.Status = RequestStatus.Pending;
      this.LastUpdate = ExecutionServer.DateMinValue;
    }

    #endregion Security members


    #region Transaction data

    public bool HasTransaction {
      get {
        return this.Transaction.UID.Length != 0;
      }
    }


    [DataObject()]
    public EFilingTransaction Transaction {
      get;
      private set;
    }


    internal async Task CreateTransaction() {
      Assertion.Assert(!this.HasTransaction, $"A transaction was already linked to this request.");

      var createdTransaction = await this.ExternalServicesHandler.CreateTransaction();

      this.Transaction = new EFilingTransaction(createdTransaction.UID);

      await this.Transaction.Synchronize(this.ExternalServicesHandler);
    }


    #endregion Transaction data


    #region Payment data

    public bool HasPaymentOrder {
      get {
        return this.PaymentOrder.RouteNumber.Length != 0;
      }
    }


    internal PaymentOrder PaymentOrder {
      get;
      private set;
    } = PaymentOrder.Empty;


    internal async Task CreatePaymentOrder() {
      var dto = await this.ExternalServicesHandler.GeneratePaymentOrder();

      this.PaymentOrder = new PaymentOrder(dto);
    }


    internal void SetPaymentReceipt(string receiptNo) {
      Assertion.AssertObject(receiptNo, "receiptNo");

      this.PaymentOrder.ReceiptNo = receiptNo;
    }


    private void LoadPaymentData() {
      if (this.Transaction.ExtensionData.Contains("paymentData")) {
        var paymentData = this.Transaction.ExtensionData.Slice("paymentData");

        this.PaymentOrder = PaymentOrder.Parse(paymentData);
      }
    }


    private void SetPaymentData() {
      this.Transaction.ExtensionData.SetIfValue("paymentData",
                                                this.PaymentOrder.ToJson());
    }

    #endregion Payment data


    #region Public Methods

    internal void Delete() {
      EnsureCanBeEdited();

      this.Status = RequestStatus.Deleted;
    }


    protected override void OnSave() {
      if (this.IsNew) {
        this.PostingTime = DateTime.Now;
      }

      this.SetPaymentData();

      EFilingRepository.WriteFilingRequest(this);
    }


    internal void SetApplicationForm(JsonObject json) {
      Assertion.AssertObject(json, "json");

      EnsureCanBeEdited();

      this.ExtensionData.Set("appForm", json);
    }


    internal void SetRequesterData(RequesterDto requester) {
      Assertion.AssertObject(requester, "requester");

      EnsureCanBeEdited();

      this.RequestedBy = requester;

      this.ExtensionData.SetIfValue("requestedByData", requester.ToJson());
    }


    internal async Task Submit() {
      EnsureCanBeSubmitted();

      await this.ExternalServicesHandler.SetPayment();

      await this.ExternalServicesHandler.Submit();

      await this.UpdateStatus(RequestStatus.Submitted);
    }


    internal async Task Synchronize() {
      if (this.HasTransaction) {
        await this.Transaction.Synchronize(this.ExternalServicesHandler);
      }
      if (this.HasTransaction || this.HasPaymentOrder) {
        await this.PaymentOrder.Synchronize(this.ExternalServicesHandler);
      }
    }


    internal async Task UpdateStatus(RequestStatus newStatus) {
      this.Status = newStatus;

      if (StatusNeedsExternalDataSynchronization(newStatus)) {
        await this.Synchronize();
      }
    }


    #endregion Public methods


    #region Private methods

    private EFilingExternalServicesInteractor ExternalServicesHandler {
      get;
    }


    private void EnsureCanBeEdited() {
      Assertion.Assert(!this.IsSigned, "This filing is already signed, so it can't be edited.");

      Assertion.Assert(this.Status == RequestStatus.Pending,
                       "This filing is not in pending status, so it can't be edited.");

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsRegister, "Current user can't edit this filing.");
    }


    private void EnsureCanBeSubmitted() {
      Assertion.Assert(this.Status == RequestStatus.OnPayment,
                       "Invalid status for submitting. Must be OnPayment");


      Assertion.AssertObject(this.PaymentOrder.ReceiptNo,
                             "No receipt number.");

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsManager, "Current user can't submit this filing.");
    }


    private void LoadRequesterData() {
      var json = this.ExtensionData.Slice("requestedByData", false);

      this.RequestedBy.Load(json);
    }


    internal void SetUserContextData() {
      var userContext = EFilingUserContext.Current();

      this.PostedBy = userContext.User;
      this.Agency = userContext.Agency;
      this.Agent = userContext.Agent;
    }


    static private bool StatusNeedsExternalDataSynchronization(RequestStatus newStatus) {
      return (newStatus == RequestStatus.Finished ||
              newStatus == RequestStatus.Rejected ||
              newStatus == RequestStatus.Submitted ||
              newStatus == RequestStatus.OnPayment);
    }


    #endregion Private methods


  } // class EFilingRequest

} // namespace Empiria.OnePoint.EFiling
