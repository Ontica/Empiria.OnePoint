﻿/* Empiria OnePoint ******************************************************************************************
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
using Empiria.Workflow.Execution.Adapters;

namespace Empiria.Workflow.Execution {

  /// <summary>A workflow instance is a runtime representation of a workflow process model definition.</summary>
  public class WorkflowInstance : BaseObject, INamedEntity {

    #region Fields

    private readonly Lazy<WorkflowEngine> _workflowEngine;

    #endregion Fields

    #region Constructors and parsers

    private WorkflowInstance() {
      // Required by Empiria Framework.
      _workflowEngine = new Lazy<WorkflowEngine>(() => new WorkflowEngine(this));
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

      _workflowEngine = new Lazy<WorkflowEngine>(() => new WorkflowEngine(this));
    }


    static internal WorkflowInstance Parse(int id) {
      return BaseObject.ParseId<WorkflowInstance>(id);
    }


    static internal WorkflowInstance Parse(string uid) {
      return BaseObject.ParseKey<WorkflowInstance>(uid);
    }


    static internal FixedList<WorkflowInstance> GetList(Request request) {
      Assertion.Require(request, nameof (request));

      return WorkflowExecutionData.GetWorkflowInstances(request);
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


    public bool IsRequestActive {
      get {
        return Request.Status == ActivityStatus.Active;
      }
    }


    public bool IsStarted {
      get {
        return !IsEmptyInstance &&
               StartTime != ExecutionServer.DateMaxValue &&
               Status != ActivityStatus.Pending;
      }
    }


    private WorkflowEngine WorkflowEngine {
      get {
        return _workflowEngine.Value;
      }
    }

    #endregion Properties

    #region Methods

    internal void AddStep(WorkflowStep step) {
      Assertion.Require(step, nameof(step));

      WorkflowEngine.Steps.Add(step);
    }


    internal WorkflowStep CreateStep(WorkflowModelItem workflowModelItem) {
      Assertion.Require(workflowModelItem, nameof(workflowModelItem));

      return WorkflowEngine.CreateStep(workflowModelItem);
    }


    public FixedList<WorkflowStep> GetSteps() {
      if (!IsStarted) {
        return new FixedList<WorkflowStep>();
      }
      return WorkflowEngine.Steps.ToFixedList();
    }


    protected override void OnSave() {
      if (base.IsDirty) {
        WorkflowExecutionData.Write(this, this.ExtensionData.ToString());
      }
      WorkflowEngine.SaveChanges();
    }


    internal void RemoveStep(WorkflowStep workflowStep) {
      workflowStep.Delete();
    }


    internal void Start() {
      Assertion.Require(!IsStarted,
                        $"Workflow instance was already started and has status {Status.GetName()}.");

      WorkflowEngine.Start();

      StartTime = EmpiriaDateTime.NowWithCentiseconds;
      Status = ActivityStatus.Active;

      base.MarkAsDirty();
    }


    internal void UpdateStep(WorkflowStep workflowStep, WorkflowStepFields fields) {
      workflowStep.Update(fields);
    }

    #endregion Methods

  }  // class WorkflowInstance

}  // namespace Empiria.Workflow.Execution
