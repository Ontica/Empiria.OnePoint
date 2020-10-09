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

    private readonly RequestStatusHandler _statusHandler;

    #endregion Collaborators


    #region Constructors and parsers

    private EFilingRequest() {
      _externalServicesHandler = new EFilingExternalServicesInteractor(this);
      _statusHandler = new RequestStatusHandler(this, _externalServicesHandler);
      _paymentOrderHandler = new PaymentOrderHandler(this, _externalServicesHandler);
      _requestSigner = new RequestSigner(this, _statusHandler);
    }


    public EFilingRequest(Procedure procedure, RequesterDto requestedBy) : this() {
      Assertion.AssertObject(procedure, "procedure");
      Assertion.AssertObject(requestedBy, "requestedBy");

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

      _paymentOrderHandler.LoadPaymentOrder();
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
                                           this.Transaction.UID);
      }
    }


    [DataField("RequestLastUpdate", Default = "ExecutionServer.DateMinValue")]
    public DateTime LastUpdate {
      get;
      internal set;
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
        return _statusHandler.StatusName;
      }
    }


    public bool IsClosed {
      get {
        return _statusHandler.IsClosed;
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
      _statusHandler.SendToSign();
    }


    internal void Sign(JsonObject signInputData) {
      _requestSigner.Sign(signInputData);
    }


    internal void RevokeSign(JsonObject revokeSignData) {
      _requestSigner.RevokeSign(revokeSignData);
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
      Assertion.Assert(!this.HasTransaction, "A transaction was already linked to this request.");

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


    #region Methods


    internal void Delete() {
      _statusHandler.Delete();
    }


    private void LoadRequesterData() {
      var json = this.ExtensionData.Slice("requestedByData", false);

      this.RequestedBy.Load(json);
    }


    protected override void OnSave() {
      if (this.IsNew) {
        this.PostingTime = DateTime.Now;
      }

      this.SetPaymentData();

      EFilingRepository.WriteFilingRequest(this);
    }


    internal void OnStatusChanged(RequestStatus newStatus) {
      this.Status = newStatus;
    }


    internal void SetApplicationForm(JsonObject json) {
      Assertion.AssertObject(json, "json");

      _statusHandler.EnsureCanBeEdited();

      this.ExtensionData.Set("appForm", json);
    }


    internal void SetRequesterData(RequesterDto requester) {
      Assertion.AssertObject(requester, "requester");

      _statusHandler.EnsureCanBeEdited();

      this.RequestedBy = requester;

      this.ExtensionData.SetIfValue("requestedByData", requester.ToJson());
    }


    internal void SetUserContextData() {
      _statusHandler.EnsureCanBeEdited();

      var userContext = EFilingUserContext.Current();

      this.PostedBy = userContext.User;
      this.Agency = userContext.Agency;
      this.Agent = userContext.Agent;
    }


    internal async Task Submit() {
      await _statusHandler.Submit()
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
      await _statusHandler.UpdateStatus(newStatus)
                          .ConfigureAwait(false);
    }

    #endregion Methods


  } // class EFilingRequest

} // namespace Empiria.OnePoint.EFiling
