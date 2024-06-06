/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Requests.dll              Pattern   : Information Holder                      *
*  Type     : Request                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract partitioned type that represents a request that can be filed.                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

using Empiria.Ontology;
using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.OnePoint.Requests.Data;
using Empiria.OnePoint.Requests.Adapters;

namespace Empiria.OnePoint.Requests {

  /// <summary>Abstract partitioned type that represents a request that can be filed.</summary>
  [PartitionedType(typeof(RequestType))]
  public abstract class Request : BaseObject {

    #region Constructors and parsers

    protected Request(RequestType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }

    static internal Request Parse(int id) {
      return BaseObject.ParseId<Request>(id);
    }

    static internal Request Parse(string uid) {
      return BaseObject.ParseKey<Request>(uid);
    }

    static internal FixedList<Request> GetList(string filter, string sort, int pageSize) {
      return BaseObject.GetFullList<Request>(filter, sort)
                       .ToFixedList();
    }

    #endregion Constructors and parsers

    #region Properties

    public RequestType RequestType {
      get {
        return (RequestType) base.GetEmpiriaType();
      }
    }


    [DataField("REQ_UNIQUE_ID")]
    public string UniqueID {
      get; protected set;
    }


    [DataField("REQ_CONTROL_ID")]
    public string ControlID {
      get; protected set;
    }


    [DataField("REQ_REQUESTER_NAME")]
    public string RequesterName {
      get; protected set;
    }


    [DataField("REQ_DESCRIPTION")]
    public string Description {
      get; protected set;
    }


    [DataField("REQ_NOTES")]
    public string Notes {
      get; protected set;
    }


    [DataField("REQ_REQUESTER_ORG_UNIT_ID")]
    public OrganizationalUnit RequesterOrgUnit {
      get; protected set;
    }


    [DataField("REQ_REQUESTER_ID")]
    public Person Requester {
      get; protected set;
    }


    [DataField("REQ_RESPONSIBLE_ORG_UNIT_ID")]
    public OrganizationalUnit ResponsibleOrgUnit {
      get; protected set;
    }


    [DataField("REQ_FILED_BY_ID")]
    public Contacts.Contact FiledBy {
      get; private set;
    }


    [DataField("REQ_FILING_TIME")]
    public DateTime FilingTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("REQ_CLOSED_BY_ID")]
    public Contacts.Contact ClosedBy {
      get; private set;
    }


    [DataField("REQ_CLOSING_TIME")]
    public DateTime ClosingTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("REQ_EXT_DATA")]
    protected JsonObject ExtensionData {
      get; private set;
    }


    [DataField("REQ_POSTED_BY_ID")]
    public Contacts.Contact PostedBy {
      get; private set;
    }


    [DataField("REQ_POSTING_TIME")]
    public DateTime PostingTime {
      get; private set;
    }


    [DataField("REQ_STATUS", Default = ActivityStatus.Pending)]
    public ActivityStatus Status {
      get; private set;
    }


    internal protected virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(UniqueID, ControlID, RequesterName, RequestType.DisplayName);
      }
    }

    #endregion Properties

    #region Methods

    public void Close() {
      ClosingTime = EmpiriaDateTime.NowWithCentiseconds;
      ClosedBy = ExecutionServer.CurrentContact;
    }


    public void File() {
      FilingTime = EmpiriaDateTime.NowWithCentiseconds;
      FiledBy = ExecutionServer.CurrentContact;
    }


    protected override void OnSave() {
      if (base.IsNew) {
        PostingTime = EmpiriaDateTime.NowWithCentiseconds;
        PostedBy = ExecutionServer.CurrentContact;
      }
      RequestData.Write(this, this.ExtensionData.ToString());
    }


    protected virtual internal void Update(RequestFieldsDto fields) {
      this.Description = RequestType.DisplayName;
      this.RequesterOrgUnit = OrganizationalUnit.Parse(fields.RequesterOrgUnitUID);
    }

    #endregion Methods

  }  // class Request

}  // namespace Empiria.OnePoint.Requests
