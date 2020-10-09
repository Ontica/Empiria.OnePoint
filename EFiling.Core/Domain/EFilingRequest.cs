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

    #region Collaborators

    private readonly EFilingExternalServicesInteractor _externalServicesHandler;

    private readonly PaymentOrderHandler _paymentOrderHandler;

    private readonly RequestSigner _requestSigner;

    #endregion Collaborators


    #region Constructors and parsers

    private EFilingRequest() {
      _requestSigner = new RequestSigner(this);
      _externalServicesHandler = new EFilingExternalServicesInteractor(this);
      _paymentOrderHandler = new PaymentOrderHandler(this, _externalServicesHandler);
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
    }


    #endregion Constructors and parsers


    #region Public properties

    [DataField("ProcedureId")]
    public Procedure Procedure {
      get;
      private set;
    }


    [DataObject]
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
        return _requestSigner.AuthorizationTime;
      }
    }

    public bool IsSigned {
      get {
        return _requestSigner.IsSigned;
      }
    }


    public SecurityData SecurityData {
      get {
        return _requestSigner.SecurityData;
      }
    }


    internal void SendToSign() {
      this.EnsureCanBeEdited();

      this.Status = RequestStatus.OnSign;
    }


    internal void Sign(JsonObject signInputData) {
      _requestSigner.Sign(signInputData);
    }


    internal void RevokeSign(JsonObject revokeSignData) {
      _requestSigner.RevokeSign(revokeSignData);
    }


    internal void OnSigned(JsonObject signData) {
      this.ExtensionData.Set("esign", signData);

      this.LastUpdate = DateTime.Now;
      this.Status = RequestStatus.OnPayment;
    }


    internal void OnSignRevoked() {
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


    [DataObject]
    public EFilingTransaction Transaction {
      get;
      private set;
    }


    internal async Task CreateTransaction() {
      Assertion.Assert(!this.HasTransaction, $"A transaction was already linked to this request.");

      var createdTransaction = await _externalServicesHandler.CreateTransaction();

      this.Transaction = new EFilingTransaction(createdTransaction.UID);

      await this.Transaction.Synchronize(_externalServicesHandler);
    }


    #endregion Transaction data


    #region Payment data


    public bool HasPaymentOrder {
      get {
        return _paymentOrderHandler.HasPaymentOrder;
      }
    }


    internal PaymentOrder PaymentOrder {
      get {
        return _paymentOrderHandler.PaymentOrder;
      }
    }


    internal async Task CreatePaymentOrder() {
      await _paymentOrderHandler.CreatePaymentOrder();
    }


    internal void SetPaymentReceipt(string receiptNo) {
      _paymentOrderHandler.SetPaymentReceipt(receiptNo);
    }


    private void SetPaymentData() {
      _paymentOrderHandler.SetPaymentData();
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

      await _externalServicesHandler.SetPayment();

      await _externalServicesHandler.Submit();

      await this.UpdateStatus(RequestStatus.Submitted)
                .ConfigureAwait(false);
    }


    internal async Task Synchronize() {
      if (this.HasTransaction) {
        await this.Transaction.Synchronize(_externalServicesHandler);
      }
      if (this.HasPaymentOrder) {
        await _paymentOrderHandler.Synchronize();
      }
    }


    internal async Task UpdateStatus(RequestStatus newStatus) {
      this.Status = newStatus;

      if (StatusNeedsExternalDataSynchronization(newStatus)) {
        await this.Synchronize()
              .ConfigureAwait(false);
      }
    }


    #endregion Public methods


    #region Private methods


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
