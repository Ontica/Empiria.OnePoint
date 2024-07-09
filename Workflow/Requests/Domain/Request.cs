/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Abstract Partitioned Type               *
*  Type     : Request                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract partitioned type that represents a request that can be filed.                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.DataObjects;
using Empiria.Json;
using Empiria.Ontology;
using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.Workflow.Execution;

using Empiria.Workflow.Requests.Data;
using Empiria.Workflow.Requests.Adapters;
using Empiria.Workflow.Definition;

namespace Empiria.Workflow.Requests {

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

    static internal Request Empty => ParseEmpty<Request>();

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


    [DataField("REQ_WKF_INSTANCE_ID")]
    public WorkflowInstance WorkflowInstance {
      get;
      private set;
    }

    public abstract FixedList<FieldValue> RequestTypeFields {
      get;
    }

    #endregion Properties

    #region Methods

    internal void Activate() {
      Assertion.Require(CanActivate(),
                        $"Can not activate this request because its status is {Status.GetName()}.");

      Status = ActivityStatus.Active;
    }


    internal bool CanActivate() {
      if (Status == ActivityStatus.Suspended) {
        return true;
      }
      return false;
    }


    public bool CanCancel() {
      if (Status == ActivityStatus.Active || Status == ActivityStatus.Suspended) {
        return true;
      }
      return false;
    }


    public bool CanClose() {
      if (Status == ActivityStatus.Active) {
        return true;
      }
      return false;
    }


    public bool CanDelete() {
      if (Status == ActivityStatus.Pending) {
        return true;
      }
      return false;
    }


    public bool CanReject() {
      if (Status == ActivityStatus.Active) {
        return true;
      }
      return false;
    }


    public bool CanStart() {
      if (Status == ActivityStatus.Pending) {
        return true;
      }
      return false;
    }


    public bool CanSuspend() {
      if (Status == ActivityStatus.Active) {
        return true;
      }
      return false;
    }


    protected virtual internal bool CanUpdate() {
      if (Status == ActivityStatus.Pending || Status == ActivityStatus.Active) {
        return true;
      }
      return false;
    }


    public void Cancel() {
      Assertion.Require(CanCancel(),
                        $"Can not cancel this request because its status is {Status.GetName()}, " +
                        $"or because has one or more completed tasks that are not reversible.");

      Status = ActivityStatus.Canceled;
    }


    public void Close() {
      ClosingTime = EmpiriaDateTime.NowWithCentiseconds;
      ClosedBy = ExecutionServer.CurrentContact;

      Status = ActivityStatus.Completed;
    }


    public void Delete() {
      Assertion.Require(CanDelete(),
                        $"Can not delete this request because its status is {Status.GetName()}.");

      this.Status = ActivityStatus.Deleted;
    }


    protected override void OnSave() {
      if (base.IsNew) {
        PostingTime = EmpiriaDateTime.NowWithCentiseconds;
        PostedBy = ExecutionServer.CurrentContact;
      }
      RequestData.Write(this, this.ExtensionData.ToString());
    }


    public void Reject() {
      Assertion.Require(CanReject(),
                        $"Can not reject this request because its status is {Status.GetName()}.");

      this.Status = ActivityStatus.Rejected;
    }


    internal void Start(ProcessDef processDefinition) {
      Assertion.Require(processDefinition, nameof(processDefinition));

      Assertion.Require(CanStart(),
                        $"Can not start this request because its status is {Status.GetName()}.");

      this.WorkflowInstance = new WorkflowInstance(processDefinition, this);

      this.WorkflowInstance.Start();

      this.FilingTime = this.WorkflowInstance.StartTime;
      this.FiledBy = ExecutionServer.CurrentContact;
    }


    internal void Suspend() {
      Assertion.Require(CanSuspend(),
                        $"Can not suspend this request because its status is {Status.GetName()}.");

      Status = ActivityStatus.Suspended;
    }


    protected virtual internal void Update(RequestFieldsDto fields) {
      Assertion.Require(CanUpdate(),
                       $"Can not update this request because its status is {Status.GetName()}.");

      this.Description = RequestType.DisplayName;
      this.RequesterOrgUnit = OrganizationalUnit.Parse(fields.RequesterOrgUnitUID);
    }

    #endregion Methods

  }  // class Request

}  // namespace Empiria.Workflow.Requests
