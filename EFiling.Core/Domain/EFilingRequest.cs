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

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Security;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Represents an electronic filing request typically submitted to a government agency.</summary>
  public class EFilingRequest : BaseObject, IProtected {

    #region Constructors and parsers

    private EFilingRequest() {
      // Required by Empiria Framework
    }


    public EFilingRequest(JsonObject data) {
      this.ExtensionData = new JsonObject();

      this.LoadData(data);
    }


    static public EFilingRequest TryParse(string uid) {
      return BaseObject.TryParse<EFilingRequest>($"FilingRequestUID = '{uid}'");
    }


    static public FixedList<EFilingRequest> GetList(EFilingRequestStatus status,
                                                    string keywords) {
      return EFilingRequestData.GetList(status, keywords);
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("ProcedureId")]
    public Procedure Procedure {
      get;
      private set;
    }


    [DataField("RequestedBy")]
    public string RequestedBy {
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


    [DataField("RequestExtData")]
    protected internal JsonObject ExtensionData {
      get;
      private set;
    } = new JsonObject();


    public virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.RequestedBy);
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

          case EFilingRequestStatus.Signed:
            return "Firmada";

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
        return ExtensionData.Get("esign/sign", String.Empty);
      }
    }


    public string TransactionUID {
      get {
        return ExtensionData.Get("transaction/uid", String.Empty);
      }
    }

    #endregion Fields


    int IProtected.CurrentDataIntegrityVersion {
      get {
        return 1;
      }
    }


    object[] IProtected.GetDataIntegrityFieldValues(int version) {
      if (version == 1) {
        return new object[] {
          1, "Id", this.Id, "UID", this.UID,
          "ProcedureId", this.Procedure.Id, "RequestedBy", this.RequestedBy,
          "AgencyId", this.Agency.Id, "AgentId", this.Agent.Id,
          "ExtensionData", this.ExtensionData.ToString(), "LastUpdateTime", this.LastUpdateTime,
          "PostingTime", this.PostingTime, "PostedById", this.PostedBy.Id, "Status", (char) this.Status
        };
      }
      throw new SecurityException(SecurityException.Msg.WrongDIFVersionRequested, version);
    }


    private IntegrityValidator _validator = null;

    public IntegrityValidator Integrity {
      get {
        if (_validator == null) {
          _validator = new IntegrityValidator(this);
        }
        return _validator;
      }
    }


    internal void GeneratePaymentOrder() {
      if (this.TransactionUID.Length != 0) {
        return;
      }

      var transactionProvider = this.Procedure.GetFilingTransactionProvider();

      var transaction = transactionProvider.CreateTransaction(this);

      ExtensionData.Set("transaction/uid", transaction.UID);

      this.Status = EFilingRequestStatus.OnPayment;
    }


    internal void Submit() {
      if (this.TransactionUID.Length == 0) {
        return;
      }

      var transactionProvider = this.Procedure.GetFilingTransactionProvider();

      transactionProvider.SubmitTransaction(this.TransactionUID);

      this.Status = EFilingRequestStatus.Submitted;
    }

    #region Public methods

    private void AssertCanBeClosed() {

    }

    internal void Delete() {

    }

    internal void RevokeSign(JsonObject bodyAsJson) {
      this.ExtensionData.Remove("esign");

      this.Status = EFilingRequestStatus.Pending;
      this.LastUpdateTime = ExecutionServer.DateMinValue;
    }


    internal void Sign(JsonObject bodyAsJson) {
      var signToken = bodyAsJson.Get<string>("signToken");

      // var securedToken = Cryptographer.ConvertToSecureString(signToken);

      var esign = Cryptographer.SignTextWithSystemCredentials(this.GetElectronicSeal());    // securedToken 2nd arg

      this.ExtensionData.Set("esign/sign", esign);
      this.Status = EFilingRequestStatus.Signed;
      this.LastUpdateTime = DateTime.Now;
    }


    public virtual string GetElectronicSeal() {
      var seed = EmpiriaString.BuildDigitalString(this.Id, this.UID, this.Procedure.Id, this.RequestedBy,
                                                  this.Agency.Id, this.Agent.Id, this.LastUpdateTime);

      return Cryptographer.CreateHashCode(seed, this.UID);
    }


    protected override void OnSave() {
      if (this.IsNew) {
        this.PostedBy = Contact.Parse(ExecutionServer.CurrentUserId);
        this.PostingTime = DateTime.Now;
        this.Agency = Organization.Parse(511);
        this.Agent = Contact.Parse(1090);
      }
      EFilingRequestData.WriteFilingRequest(this);
    }


    internal void Update(JsonObject data) {
      Assertion.AssertObject(data, "data");

      this.LoadData(data);
    }


    #endregion Public methods


    #region Private methods


    private void LoadData(JsonObject data) {
      this.RequestedBy = data.GetClean("requestedBy").ToUpperInvariant();
    }


    #endregion Private methods

  } // class EFilingRequest

} // namespace Empiria.OnePoint.EFiling
