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
using System.Collections.Generic;
using Empiria.DataObjects;
using Empiria.Json;
using Empiria.Ontology;
using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.Workflow.Execution;

using Empiria.Workflow.Requests.Data;
using Empiria.Workflow.Requests.Adapters;

namespace Empiria.Workflow.Requests {

  /// <summary>Abstract partitioned type that represents a request that can be filed.</summary>
  [PartitionedType(typeof(RequestType))]
  public abstract class Request : BaseObject {

    #region Fields

    private Lazy<FixedList<WorkflowInstance>> _workflowInstances;

    #endregion Fields

    #region Constructors and parsers

    protected Request(RequestType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
      ResetWorkflowInstances();
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
      get; private set;
    }


    [DataField("REQ_CONTROL_ID")]
    public string ControlID {
      get; private set;
    }


    [DataField("REQ_REQUESTER_NAME")]
    public string RequesterName {
      get; private set;
    }


    [DataField("REQ_DESCRIPTION")]
    public string Description {
      get; protected set;
    }


    [DataField("REQ_REQUESTER_ORG_UNIT_ID")]
    public OrganizationalUnit RequesterOrgUnit {
      get; private set;
    }


    [DataField("REQ_REQUESTER_ID")]
    public Person Requester {
      get; private set;
    }


    [DataField("REQ_RESPONSIBLE_ORG_UNIT_ID")]
    public OrganizationalUnit ResponsibleOrgUnit {
      get; private set;
    }


    [DataField("REQ_STARTED_BY_ID")]
    public Contacts.Contact StartedBy {
      get; private set;
    }


    [DataField("REQ_START_TIME")]
    public DateTime StartTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("REQ_CLOSED_BY_ID")]
    public Contacts.Contact EndedBy {
      get; private set;
    }


    [DataField("REQ_CLOSING_TIME")]
    public DateTime EndTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("REQ_EXT_DATA")]
    protected JsonObject ExtensionData {
      get; private set;
    }


    [DataField("REQ_STATUS", Default = ActivityStatus.Pending)]
    public ActivityStatus Status {
      get; private set;
    }


    internal protected virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(UniqueID, ControlID, Description, RequesterName, RequestType.DisplayName);
      }
    }


    public bool HasWorkflowInstance {
      get {
        return WorkflowInstances.Count != 0;
      }
    }


    public RequestActions Actions {
      get {
        return new RequestActions(this);
      }
    }


    public abstract FixedList<FieldValue> RequestTypeFields {
      get;
    }


    public FixedList<WorkflowInstance> WorkflowInstances {
      get {
        return _workflowInstances.Value;
      }
    }

    #endregion Properties

    #region Methods

    internal void Activate() {
      Assertion.Require(Actions.CanActivate(), InvalidOperationMessage("activate"));

      Status = ActivityStatus.Active;
      base.MarkAsDirty();
    }


    public void Cancel() {
      Assertion.Require(Actions.CanCancel(), InvalidOperationMessage("cancel"));

      Status = ActivityStatus.Canceled;
      base.MarkAsDirty();
    }


    public void Complete() {
      Assertion.Require(Actions.CanComplete(), InvalidOperationMessage("complete"));

      EndTime = EmpiriaDateTime.NowWithCentiseconds;
      EndedBy = ExecutionServer.CurrentContact;

      Status = ActivityStatus.Completed;

      base.MarkAsDirty();
    }


    public void Delete() {
      Assertion.Require(Actions.CanDelete(), InvalidOperationMessage("delete"));

      this.Status = ActivityStatus.Deleted;
      base.MarkAsDirty();
    }


    internal FixedList<WorkflowTask> GetTasks() {
      var allTasksList = new List<WorkflowTask>(32);

      foreach (WorkflowInstance instance in WorkflowInstances) {
        var tasks = instance.GetTasks();

        allTasksList.AddRange(tasks);
      }
      return allTasksList.ToFixedList();
    }


    protected override void OnSave() {
      if (base.IsNew) {
        this.UniqueID = RequestData.GetNextUniqueID(ResponsibleOrgUnit.Acronym, DateTime.Today.Year);
        this.ControlID = "No determinado";
      }

      RequestData.Write(this, this.ExtensionData.ToString());
    }


    internal void Start() {
      Assertion.Require(Actions.CanStart(), InvalidOperationMessage("start"));

      this.ControlID = RequestData.GetNextControlNumber(DateTime.Today.Year);
      this.StartTime = EmpiriaDateTime.NowWithCentiseconds;
      this.StartedBy = ExecutionServer.CurrentContact;
      this.Status = ActivityStatus.Active;

      ResetWorkflowInstances();

      Assertion.Ensure(WorkflowInstances.Count != 0,
                      "At least one workflow instance must be created for this request.");

      base.MarkAsDirty();
    }


    internal void Suspend() {
      Assertion.Require(Actions.CanSuspend(), InvalidOperationMessage("suspend"));

      Status = ActivityStatus.Suspended;
      base.MarkAsDirty();
    }


    protected virtual internal void Update(RequestFieldsDto fields) {
      Assertion.Require(fields, nameof(fields));

      Assertion.Require(Actions.CanUpdate(), InvalidOperationMessage("update"));

      if (fields.RequestTypeUID != RequestType.UID) {
        base.ReclassifyAs(RequestType.Parse(fields.RequestTypeUID));
      }

      if (fields.RequestedByUID.Length != 0) {
        this.Requester = Person.Parse(fields.RequestedByUID);
      } else {
        this.Requester = Person.Parse(ExecutionServer.CurrentUserId);
      }
      this.RequesterName = Requester.Name;

      if (fields.Description.Length != 0) {
        this.Description = fields.Description;
      } else {
        this.Description = this.RequestType.DisplayName;
      }

      this.RequesterOrgUnit = OrganizationalUnit.Parse(fields.RequesterOrgUnitUID);
      this.ResponsibleOrgUnit = RequestType.ResponsibleOrgUnit;
      base.MarkAsDirty();
    }

    #endregion Methods

    #region Helpers

    private string InvalidOperationMessage(string operationName) {
      return $"Can not {operationName} this request. Its status is {Status.GetName()}.";
    }


    private void ResetWorkflowInstances() {
      _workflowInstances = new Lazy<FixedList<WorkflowInstance>>(() => WorkflowInstance.GetList(this));
    }

    #endregion Helpers

  }  // class Request

}  // namespace Empiria.Workflow.Requests
