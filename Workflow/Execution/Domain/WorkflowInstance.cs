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

using Empiria.Workflow.Requests;
using Empiria.Workflow.Definition;

using Empiria.Workflow.Execution.Data;

namespace Empiria.Workflow.Execution {

  /// <summary>A workflow instance is a runtime representation of a workflow process model definition.</summary>
  public class WorkflowInstance : BaseObject {

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

    static internal WorkflowInstance Empty => ParseEmpty<WorkflowInstance>();

    #endregion Constructors and parsers

    #region Properties

    [DataField("WKF_INSTANCE_MODEL_ID")]
    public ProcessDef ProcessDefinition {
      get; private set;
    }


    [DataField("WKF_INSTANCE_REQUEST_ID")]
    private int _requestId = -1;

    public Request Request {
      get {
        return Request.Parse(_requestId);
      }
      private set {
        _requestId = value.Id;
      }
    }

    [DataField("WKF_INSTANCE_PARENT_ID")]
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


    [DataField("WKF_INSTANCE_EXT_DATA")]
    internal JsonObject ExtensionData {
      get; private set;
    }


    [DataField("WKF_INSTANCE_START_TIME")]
    public DateTime StartTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WKF_INSTANCE_CLOSE_TIME")]
    public DateTime CloseTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WKF_INSTANCE_STATUS", Default = ActivityStatus.Pending)]
    public ActivityStatus Status {
      get; private set;
    }


    internal protected virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Request.Keywords);
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

    public FixedList<WorkflowTask> GetTasks() {
      if (!IsStarted) {
        return new FixedList<WorkflowTask>();
      }
      return WorkflowEngine.GetTasks();
    }


    protected override void OnSave() {
      if (base.IsDirty) {
        WorkflowExecutionData.Write(this);
      }
      WorkflowEngine.SaveChanges();
    }


    internal void Start() {
      Assertion.Require(!IsStarted,
                        $"Workflow instance was already started and has status {Status.GetName()}.");

      WorkflowEngine.Start();

      StartTime = EmpiriaDateTime.NowWithCentiseconds;
      Status = ActivityStatus.Active;

      base.MarkAsDirty();
    }

    #endregion Methods

  }  // class WorkflowInstance

}  // namespace Empiria.Workflow.Execution
