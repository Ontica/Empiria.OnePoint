/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowInstance                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : A workflow instance is a runtime representation of a workflow process model definition.        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;
using Empiria.StateEnums;
using Empiria.Parties;

using Empiria.Workflow.Requests;
using Empiria.Workflow.Definition;

using Empiria.Workflow.Execution.Data;

namespace Empiria.Workflow.Execution {

  /// <summary>A workflow instance is a runtime representation of a workflow process model definition.</summary>
  public class WorkflowInstance : BaseObject, INamedEntity {

    #region Fields

    private readonly Lazy<WorkflowInstanceEngine> _engine;

    #endregion Fields

    #region Constructors and parsers

    private WorkflowInstance() {
      // Required by Empiria Framework.
      _engine = new Lazy<WorkflowInstanceEngine>(() => new WorkflowInstanceEngine(this));
    }


    public WorkflowInstance(ProcessDef processDefinition, Request request) {
      Assertion.Require(processDefinition, nameof(processDefinition));
      Assertion.Require(request, nameof(request));

      Assertion.Require(!processDefinition.IsEmptyInstance,
                        "Process definition must not be the empty instance.");
      Assertion.Require(!request.IsEmptyInstance,
                        "Request must not be the empty instance.");

      this.ProcessDefinition = processDefinition;
      this.Request = request;

      _engine = new Lazy<WorkflowInstanceEngine>(() => new WorkflowInstanceEngine(this));
    }


    static internal WorkflowInstance Parse(int id) {
      return BaseObject.ParseId<WorkflowInstance>(id);
    }


    static internal WorkflowInstance Parse(string uid) {
      return BaseObject.ParseKey<WorkflowInstance>(uid);
    }


    static internal WorkflowInstance Empty => ParseEmpty<WorkflowInstance>();

    #endregion Constructors and parsers

    #region Properties

    [DataField("WMS_INST_PROCESS_DEF_ID")]
    public ProcessDef ProcessDefinition {
      get; private set;
    }


    [DataField("WMS_INST_REQUEST_ID")]
    private int _requestId = -1;

    public Request Request {
      get {
        return Request.Parse(_requestId);
      }
      private set {
        _requestId = value.Id;
      }
    }


    public string Name {
      get {
        return ExtensionData.Get("name", ProcessDefinition.Name);
      }
      private set {
        ExtensionData.SetIfValue("name", EmpiriaString.TrimAll(value));
      }
    }


    public string Description {
      get {
        return ExtensionData.Get("description", ProcessDefinition.Description);
      }
      private set {
        ExtensionData.SetIfValue("description", EmpiriaString.TrimAll(value));
      }
    }

    [DataField("WMS_INST_REQUESTED_BY_ID")]
    public Person RequestedBy {
      get; private set;
    }


    [DataField("WMS_INST_REQUESTED_BY_ORG_UNIT_ID")]
    public OrganizationalUnit RequestedByOrgUnit {
      get; private set;
    }


    [DataField("WMS_INST_RESPONSIBLE_ORG_UNIT_ID")]
    public OrganizationalUnit ResponsibleOrgUnit {
      get; private set;
    }


    [DataField("WMS_INST_PRIORITY", Default = Priority.Normal)]
    public Priority Priority {
      get;
      internal set;
    }


    [DataField("WMS_INST_DUE_TIME")]
    public DateTime DueTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WMS_INST_STARTED_BY_ID")]
    public Party StartedBy {
      get; private set;
    }


    [DataField("WMS_INST_START_TIME")]
    public DateTime StartTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WMS_INST_END_TIME")]
    public DateTime EndTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WMS_INST_PARENT_ID")]
    private int _parentId;

    public WorkflowInstance Parent {
      get {
        if (this.IsEmptyInstance) {
          return this;
        }
        return WorkflowInstance.Parse(_parentId);
      }
      private set {
        _parentId = value.Id;
      }
    }


    [DataField("WMS_INST_EXT_DATA")]
    private JsonObject ExtensionData {
      get; set;
    }


    [DataField("WMS_INST_STATUS", Default = ActivityStatus.Pending)]
    public ActivityStatus Status {
      get; private set;
    }


    internal protected virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Request.Keywords);
      }
    }


    public WorkflowInstanceActions Actions {
      get {
        return new WorkflowInstanceActions(this);
      }
    }


    public bool IsOptional {
      get {
        return this.ExtensionData.Get(WorkflowConstants.IS_OPTIONAL, false);
      }
    }


    public bool IsStarted {
      get {
        return !IsEmptyInstance &&
               StartTime != ExecutionServer.DateMaxValue &&
               Status != ActivityStatus.Pending;
      }
    }

    #endregion Properties

    #region Methods

    internal WorkflowInstanceEngine GetEngine() {
      return _engine.Value;
    }


    public WorkflowStep GetStep(string workflowStepUID) {
      Assertion.Require(workflowStepUID, nameof(workflowStepUID));

      WorkflowStep step = GetSteps().Find(x => x.UID == workflowStepUID);

      Assertion.Require(step, "Step not found.");

      return step;
    }


    public FixedList<WorkflowStep> GetSteps() {
      if (!IsStarted) {
        return new FixedList<WorkflowStep>();
      }
      return GetEngine().GetSteps();
    }


    protected override void OnSave() {
      if (base.IsDirty) {
        WorkflowExecutionData.Write(this, this.ExtensionData.ToString());
      }
    }


    internal void OnStart() {
      StartTime = ExecutionServer.NowWithCentiseconds;
      Status = ActivityStatus.Active;

      base.MarkAsDirty();
    }

    #endregion Methods

  }  // class WorkflowInstance

}  // namespace Empiria.Workflow.Execution
