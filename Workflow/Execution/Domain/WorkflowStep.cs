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
      this.No = workflowModelItem.Position.ToString("00");
      this.Description = workflowModelItem.TargetObject.Name;
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

    [DataField("WKF_INSTANCE_ID")]
    public WorkflowInstance WorkflowInstance {
      get; private set;
    }


    [DataField("WKF_MDL_ITEM_ID")]
    public WorkflowModelItem WorkflowModelItem {
      get; private set;
    }


    [DataField("WKF_STEP_NO")]
    public string No {
      get; private set;
    }


    [DataField("WKF_STEP_DESCRIPTION")]
    public string Description {
      get; private set;
    }


    [DataField("WKF_STEP_TAGS")]
    public string Tags {
      get; private set;
    }


    [DataField("WKF_STEP_EXT_OBJECT_ID")]
    public int ExternalObjectId {
      get; private set;
    } = -1;


    [DataField("WKF_STEP_REQUESTED_BY_ID")]
    public Party RequestedBy {
      get; private set;
    }


    [DataField("WKF_STEP_REQUESTED_BY_ORG_UNIT_ID")]
    public OrganizationalUnit RequestedByOrgUnit {
      get; private set;
    }


    [DataField("WKF_STEP_ASSIGNED_TO_ID")]
    public Party AssignedTo {
      get; private set;
    }


    [DataField("WKF_STEP_ASSIGNED_TO_ORG_UNIT_ID")]
    public OrganizationalUnit AssignedToOrgUnit {
      get; private set;
    }


    [DataField("WKF_STEP_DEADLINE")]
    public DateTime Deadline {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WKF_STEP_CHECK_IN_TIME")]
    public DateTime CheckInTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WKF_STEP_END_TIME")]
    public DateTime EndTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WKF_STEP_CHECK_OUT_TIME")]
    public DateTime CheckOutTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WKF_STEP_PREVIOUS_STEP_ID")]
    private int _previousStepId = -1;
    public WorkflowStep PreviousStep {
      get {
        if (this.IsEmptyInstance) {
          return this;
        }
        return WorkflowStep.Parse(_previousStepId);
      }
      private set {
        _previousStepId = value.Id;
      }
    }


    [DataField("WKF_STEP_NEXT_STEP_ID")]
    private int _nextStepId = -1;
    public WorkflowStep NextStep {
      get {
        if (this.IsEmptyInstance) {
          return this;
        }
        return WorkflowStep.Parse(_nextStepId);
      }
      private set {
        _nextStepId = value.Id;
      }
    }

    [DataField("WKF_STEP_EXT_DATA")]
    private JsonObject ExtensionData {
      get; set;
    }

    [DataField("WKF_STEP_STATUS", Default = ActivityStatus.Pending)]
    public ActivityStatus Status {
      get; private set;
    }


    internal protected virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.Description, this.WorkflowModelItem.Keywords,
                                           this.AssignedTo.Name, this.AssignedToOrgUnit.FullName);
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

  }  // class WorkflowStep

}  // namespace Empiria.Workflow.Execution
