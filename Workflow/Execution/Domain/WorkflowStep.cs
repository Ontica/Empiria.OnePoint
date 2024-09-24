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

using Empiria.Json;
using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.Workflow.Definition;

using Empiria.Workflow.Execution.Data;

namespace Empiria.Workflow.Execution {

  /// <summary> A workflow step is an actual runtime workflow event, activity or gateway,
  /// under the execution context of a workflow instance.</summary>
  public class WorkflowStep : BaseObject {

    #region Constructors and parsers

    private WorkflowStep() {
      // Required by Empiria Framework.
    }

    public WorkflowStep(WorkflowInstance workflowInstance, WorkflowModelItem workflowModelItem) {
      Assertion.Require(workflowInstance, nameof(workflowInstance));
      Assertion.Require(workflowModelItem, nameof(workflowModelItem));

      this.WorkflowInstance = workflowInstance;
      this.WorkflowModelItem = workflowModelItem;
      this.StepDefinition = (StepDef) WorkflowModelItem.TargetObject;

      LoadDefaultData();
    }


    static internal WorkflowStep Parse(int id) {
      return BaseObject.ParseId<WorkflowStep>(id);
    }

    static internal WorkflowStep Parse(string uid) {
      return BaseObject.ParseKey<WorkflowStep>(uid);
    }

    static internal WorkflowStep Empty => ParseEmpty<WorkflowStep>();

    #endregion Constructors and parsers

    #region Properties

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


    [DataField("WMS_STEP_EXT_DATA")]
    private JsonObject ExtensionData {
      get; set;
    }


    [DataField("WMS_STEP_STATUS", Default = ActivityStatus.Pending)]
    public ActivityStatus Status {
      get; private set;
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

    protected override void OnSave() {
      if (IsDirty) {
        WorkflowExecutionData.Write(this, ExtensionData.ToString());
      }
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
