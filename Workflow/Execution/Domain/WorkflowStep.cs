/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowStep                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : A workflow step is an actual runtime workflow event, activity or gateway,                      *
*             under the execution context of a workflow instance.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Data;

using Empiria.Json;
using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.Workflow.Definition;

using Empiria.Workflow.Requests;

using Empiria.Workflow.Execution.Adapters;
using Empiria.Workflow.Execution.Data;


namespace Empiria.Workflow.Execution {

  /// <summary> A workflow step is an actual runtime workflow event, activity or gateway,
  /// under the execution context of a workflow instance.</summary>
  public class WorkflowStep : BaseObject {

    #region Fields

    private Lazy<WorkflowStep> _previousStep = new Lazy<WorkflowStep>(() => Empty);
    private Lazy<WorkflowStep> _nextStep = new Lazy<WorkflowStep>(() => Empty);

    #endregion Fields

    #region Constructors and parsers

    private WorkflowStep() {
      // Required by Empiria Framework.
    }

    public WorkflowStep(WorkflowInstance workflowInstance,
                        WorkflowModelItem workflowModelItem) {

      Assertion.Require(workflowInstance, nameof(workflowInstance));
      Assertion.Require(workflowModelItem, nameof(workflowModelItem));

      base.GenerateId();

      this.WorkflowInstance = workflowInstance;
      this.WorkflowModelItem = workflowModelItem;
      this.StepDefinition = (StepDef) WorkflowModelItem.TargetObject;

      LoadDefaultData();
    }


    static internal WorkflowStep Parse(int id) {
      return ParseId<WorkflowStep>(id);
    }

    static internal WorkflowStep Parse(string uid) {
      return ParseKey<WorkflowStep>(uid);
    }

    static internal WorkflowStep Empty => ParseEmpty<WorkflowStep>();

    protected override void OnLoadObjectData(DataRow row) {
      _previousStep = new Lazy<WorkflowStep>(() => Parse((int) row["WMS_STEP_PREVIOUS_STEP_ID"]));
      _nextStep = new Lazy<WorkflowStep>(() => Parse((int) row["WMS_STEP_NEXT_STEP_ID"]));
    }

    #endregion Constructors and parsers

    #region Properties

    public Request Request {
      get {
        return WorkflowInstance.Request;
      }
    }

    [DataField("WMS_INSTANCE_ID")]
    public WorkflowInstance WorkflowInstance {
      get; private set;
    }


    [DataField("WMS_MODEL_ITEM_ID")]
    public WorkflowModelItem WorkflowModelItem {
      get; private set;
    }


    [DataField("WMS_STEP_DEF_ID")]
    public StepDef StepDefinition {
      get; private set;
    }


    [DataField("WMS_STEP_NO")]
    public string StepNo {
      get; private set;
    }


    public string Name {
      get {
        if (WorkflowModelItem.TargetObject.Distinct(StepDefinition)) {
          return StepDefinition.Name;
        }
        if (WorkflowModelItem.Name.Length != 0) {
          return WorkflowModelItem.Name;
        }
        return WorkflowModelItem.TargetObject.Name;
      }
    }


    [DataField("WMS_STEP_DESCRIPTION")]
    public string Description {
      get; private set;
    }


    [DataField("WMS_STEP_TAGS")]
    public string Tags {
      get; private set;
    }


    [DataField("WMS_STEP_REQUESTED_BY_ID")]
    public Party RequestedBy {
      get; private set;
    }


    [DataField("WMS_STEP_REQUESTED_BY_ORG_UNIT_ID")]
    public OrganizationalUnit RequestedByOrgUnit {
      get; private set;
    }


    [DataField("WMS_STEP_ASSIGNED_TO_ID")]
    public Party AssignedTo {
      get; private set;
    }


    [DataField("WMS_STEP_ASSIGNED_TO_ORG_UNIT_ID")]
    public OrganizationalUnit AssignedToOrgUnit {
      get; private set;
    }


    [DataField("WMS_STEP_PRIORITY", Default = Priority.Normal)]
    public Priority Priority {
      get; private set;
    }


    [DataField("WMS_STEP_DUE_TIME")]
    public DateTime DueTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WMS_STEP_START_TIME")]
    public DateTime StartTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WMS_STEP_END_TIME")]
    public DateTime EndTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    public WorkflowStep PreviousStep {
      get {
        return _previousStep.Value;
      }
      private set {
        _previousStep = new Lazy<WorkflowStep>(() => value);
      }
    }


    public WorkflowStep NextStep {
      get {
        return _nextStep.Value;
      }
      private set {
        _nextStep = new Lazy<WorkflowStep>(() => value);
      }
    }


    [DataField("WMS_STEP_EXT_DATA")]
    private JsonObject ExtensionData {
      get; set;
    }


    [DataField("WMS_STEP_STATUS", Default = ActivityStatus.Pending)]
    public ActivityStatus Status {
      get; private set;
    }


    public WorkflowStepActions Actions {
      get {
        return new WorkflowStepActions(this);
      }
    }


    public bool IsOptional {
      get {
        if (this.ExtensionData.Get(WorkflowConstants.IS_OPTIONAL, false)) {
          return true;
        }
        return this.WorkflowModelItem.IsOptional;
      }
    }


    public bool IsProcessActive {
      get {
        return this.WorkflowInstance.Request.Status == ActivityStatus.Active &&
               this.WorkflowInstance.Status == ActivityStatus.Active;
      }
    }


    internal protected virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.Description, WorkflowInstance.Request.Description,
                                           this.WorkflowModelItem.Keywords,
                                           this.AssignedTo.Name, this.AssignedToOrgUnit.FullName);
      }
    }


    public ActivityStatus RuntimeStatus {
      get {
        if (this.Status == ActivityStatus.Completed ||
            this.Status == ActivityStatus.Deleted) {
          return this.Status;
        }

        if (this.WorkflowInstance.Request.Status != ActivityStatus.Active) {
          return this.WorkflowInstance.Request.Status;
        }

        if (this.WorkflowInstance.Status != ActivityStatus.Active) {
          return this.WorkflowInstance.Status;
        }

        if (this.WorkflowInstance.Request.Status == ActivityStatus.Suspended) {
          return ActivityStatus.Suspended;
        }

        return this.Status;
      }
    }

    #endregion Properties

    #region Methods

    internal void OnPrepare() {
      if (this.Status != ActivityStatus.Waiting) {
        return;
      }

      if (this.WorkflowModelItem.Autoactivate &&
         !this.AssignedTo.IsEmptyInstance) {
        this.Status = ActivityStatus.Active;
      } else {
        this.Status = ActivityStatus.Pending;
      }

      base.MarkAsDirty();
    }


    internal void OnRemove() {
      Status = ActivityStatus.Deleted;

      base.MarkAsDirty();
    }


    protected override void OnSave() {
      if (IsDirty) {
        WorkflowExecutionData.Write(this, ExtensionData.ToString());
      }
    }


    internal void SetStepNo(string newStepNo) {
      Assertion.Require(newStepNo, nameof(newStepNo));

      this.StepNo = newStepNo;

      base.MarkAsDirty();
    }


    internal void SetPreviousStep(WorkflowStep previousStep) {
      Assertion.Require(previousStep, nameof(previousStep));
      Assertion.Require(previousStep.IsEmptyInstance ||
                        previousStep.WorkflowInstance.Equals(this.WorkflowInstance),
                        "previousStep.WorkflowInstance mismatch.");
      if (this.IsEmptyInstance) {
        return;
      }

      this.PreviousStep = previousStep;

      base.MarkAsDirty();
    }


    internal void SetNextStep(WorkflowStep nextStep) {
      Assertion.Require(nextStep, nameof(nextStep));
      Assertion.Require(nextStep.IsEmptyInstance ||
                        nextStep.WorkflowInstance.Equals(this.WorkflowInstance),
                        "nextStep.WorkflowInstance mismatch.");
      if (this.IsEmptyInstance) {
        return;
      }

      this.NextStep = nextStep;

      base.MarkAsDirty();
    }


    internal void Update(WorkflowStepFields fields) {
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      Description = EmpiriaString.TrimAll(fields.Description);
      DueTime = fields.DueTime;
      Priority = fields.Priority;
      RequestedByOrgUnit = fields.GetRequestedByOrgUnit();
      RequestedBy = fields.GetRequestedBy();
      AssignedTo = fields.GetAssignedTo();
      AssignedToOrgUnit = fields.GetAssignedToOrgUnit();

      base.MarkAsDirty();
    }

    #endregion Methods

    #region Helpers

    private void LoadDefaultData() {
      var defaultRules = new DefaultWorkflowStepRulesBuilder(this);

      this.StepNo = defaultRules.StepNo;
      this.Description = defaultRules.Description;
      this.RequestedBy = defaultRules.RequestedBy;
      this.RequestedByOrgUnit = defaultRules.RequestedByOrgUnit;
      this.AssignedTo = defaultRules.AssignedTo;
      this.AssignedToOrgUnit = defaultRules.AssignedToOrgUnit;
      this.Priority = defaultRules.Priority;
      this.DueTime = defaultRules.DueTime;
      this.StartTime = defaultRules.StartTime;
      this.Status = defaultRules.Status;
    }

    #endregion Helpers

  }  // class WorkflowStep

}  // namespace Empiria.Workflow.Execution
