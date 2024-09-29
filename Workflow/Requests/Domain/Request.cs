/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Partitioned Type                        *
*  Type     : Request                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Partitioned type that represents a request.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.DataObjects;
using Empiria.Json;
using Empiria.Ontology;
using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.Workflow.Definition;

using Empiria.Workflow.Execution;

using Empiria.Workflow.Requests.Data;
using Empiria.Workflow.Requests.Adapters;

namespace Empiria.Workflow.Requests {

  /// <summary>Partitioned type that represents a request.</summary>
  [PartitionedType(typeof(RequestType))]
  public class Request : BaseObject, INamedEntity {

    #region Fields

    private readonly Lazy<RequestWorkflowEngine> _engine;

    #endregion Fields

    #region Constructors and parsers

    protected Request(RequestType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.

      _engine = new Lazy<RequestWorkflowEngine>(() => new RequestWorkflowEngine(this));
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


    [DataField("WMS_REQUEST_DEF_ID")]
    public RequestDef RequestDef {
      get;
      private set;
    }


    [DataField("WMS_REQ_REQUEST_NO")]
    public string RequestNo {
      get; private set;
    }


    [DataField("WMS_REQ_INTERNAL_CONTROL_NO")]
    public string InternalControlNo {
      get; private set;
    }


    public string Name {
      get {
        return ExtensionData.Get("name", this.RequestDef.Name);
      } protected set {
        ExtensionData.SetIfValue("name", EmpiriaString.TrimAll(value));
      }
    }


    [DataField("WMS_REQ_DESCRIPTION")]
    public string Description {
      get; protected set;
    }


    [DataField("WMS_REQ_REQUESTED_BY_ID")]
    public Person RequestedBy {
      get; private set;
    }


    [DataField("WMS_REQ_REQUESTED_BY_ORG_UNIT_ID")]
    public OrganizationalUnit RequestedByOrgUnit {
      get; private set;
    }


    [DataField("WMS_REQ_RESPONSIBLE_ORG_UNIT_ID")]
    public OrganizationalUnit ResponsibleOrgUnit {
      get; private set;
    }


    [DataField("WMS_REQ_PRIORITY", Default = Priority.Normal)]
    public Priority Priority {
      get;
      internal set;
    }


    [DataField("WMS_REQ_DUE_TIME")]
    public DateTime DueTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WMS_REQ_STARTED_BY_ID")]
    public Party StartedBy {
      get; private set;
    }


    [DataField("WMS_REQ_START_TIME")]
    public DateTime StartTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WMS_REQ_END_TIME")]
    public DateTime EndTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WMS_REQ_EXT_DATA")]
    protected JsonObject ExtensionData {
      get; private set;
    }


    [DataField("WMS_REQ_STATUS", Default = ActivityStatus.Pending)]
    public ActivityStatus Status {
      get; private set;
    }

    protected internal virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(RequestNo, InternalControlNo, Name, Description,
                                           RequestDef.Name, RequestedBy.Name,
                                           RequestedByOrgUnit.Name, ResponsibleOrgUnit.Name);
      }
    }


    public RequestActions Actions {
      get {
        return new RequestActions(this);
      }
    }


    public virtual FixedList<FieldValue> RequestTypeFields {
      get {
        return new FixedList<FieldValue>();
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

      EndTime = EmpiriaDateTime.NowWithCentiseconds;

      Status = ActivityStatus.Canceled;

      base.MarkAsDirty();
    }


    public void Complete() {
      Assertion.Require(Actions.CanComplete(), InvalidOperationMessage("complete"));

      EndTime = EmpiriaDateTime.NowWithCentiseconds;

      Status = ActivityStatus.Completed;

      base.MarkAsDirty();
    }


    public void Delete() {
      Assertion.Require(Actions.CanDelete(), InvalidOperationMessage("delete"));

      EndTime = EmpiriaDateTime.NowWithCentiseconds;

      this.Status = ActivityStatus.Deleted;

      base.MarkAsDirty();
    }


    internal RequestWorkflowEngine GetEngine() {
      return _engine.Value;
    }

    public FixedList<WorkflowStep> GetSteps() {
      return GetEngine().GetSteps();
    }


    public FixedList<WorkflowInstance> GetWorkflowInstances() {
      return GetEngine().GetWorkflowInstances();
    }


    protected override void OnSave() {
      if (base.IsNew) {
        this.RequestNo = RequestData.GetNextRequestNo(ResponsibleOrgUnit.Acronym, DateTime.Today.Year);
        this.InternalControlNo = "Sin asignar";
      }

      RequestData.Write(this, this.ExtensionData.ToString());
    }


    internal void OnStart() {
      this.InternalControlNo = RequestData.GetNextInternalControlNo(DateTime.Today.Year);
      this.StartTime = EmpiriaDateTime.NowWithCentiseconds;
      this.StartedBy = Party.ParseWithContact(ExecutionServer.CurrentContact);
      this.Status = ActivityStatus.Active;

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

      if (fields.RequestDefUID != this.RequestDef.UID) {
        this.RequestDef = RequestDef.Parse(fields.RequestDefUID);
        if (!this.RequestDef.RequestType.Equals(this.RequestType)) {
          base.ReclassifyAs(RequestDef.RequestType);
        }
      }

      if (fields.RequestedByUID.Length != 0) {
        this.RequestedBy = Person.Parse(fields.RequestedByUID);
      } else {
        this.RequestedBy = Person.Parse(ExecutionServer.CurrentUserId);
      }

      if (fields.Description.Length != 0) {
        this.Description = fields.Description;
      } else {
        this.Description = this.RequestDef.Name;
      }

      this.RequestedByOrgUnit = OrganizationalUnit.Parse(fields.RequesterOrgUnitUID);
      this.ResponsibleOrgUnit = RequestDef.ResponsibleOrgUnit;

      base.MarkAsDirty();
    }

    #endregion Methods

    #region Helpers

    private string InvalidOperationMessage(string operationName) {
      return $"Can not {operationName} this request. Its status is {Status.GetName()}.";
    }

    #endregion Helpers

  }  // class Request

}  // namespace Empiria.Workflow.Requests
