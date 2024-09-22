/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Service Provider                        *
*  Type     : DefaultWorkflowStepRulesBuilder            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Builds a workflow step default values using configuration rules.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.Workflow.Definition;

namespace Empiria.Workflow.Execution {

  /// <summary>Builds a workflow step default values using configuration rules.</summary>
  internal class DefaultWorkflowStepRulesBuilder {

    #region Fields

    private readonly WorkflowStep _step;

    private readonly WorkflowStepAssigner _workflowStepAssigner;

    #endregion Fields

    #region Constructors and parsers

    internal DefaultWorkflowStepRulesBuilder(WorkflowStep workflowStep) {
      _step = workflowStep;
      _workflowStepAssigner = new WorkflowStepAssigner(workflowStep);
    }

    #endregion Constructors and parsers

    #region Properties

    public string StepNo {
      get {
        return _step.WorkflowModelItem.Position.ToString("D2");
      }
    }


    public string Description {
      get {
        if (_step.WorkflowModelItem.Name.Length != 0) {
          return _step.WorkflowModelItem.Name;
        }
        return _step.WorkflowModelItem.TargetObject.Name;
      }
    }


    public Party RequestedBy {
      get {
        return _workflowStepAssigner.RequestedBy;
      }
    }


    public OrganizationalUnit RequestedByOrgUnit {
      get {
        return _workflowStepAssigner.RequestedByOrgUnit;
      }
    }


    public Party AssignedTo {
      get {
        return _workflowStepAssigner.AssignedTo;
      }
    }


    public OrganizationalUnit AssignedToOrgUnit {
      get {
        return _workflowStepAssigner.AssignedToOrgUnit;
      }
    }


    public DateTime Deadline {
      get {
        return ExecutionServer.DateMaxValue;
      }
    }


    public DateTime CheckInTime {
      get {
        if (_step.WorkflowModelItem.Autoactivate) {
          return EmpiriaDateTime.NowWithCentiseconds;

        } else if (_step.WorkflowModelItem.TargetObject is StepDef stepDef &&
                   stepDef.Autoactivate) {
          return EmpiriaDateTime.NowWithCentiseconds;

        }

        return ExecutionServer.DateMaxValue;
      }
    }


    public ActivityStatus Status {
      get {
        if (!(_step.WorkflowModelItem.SourceObject is EventDef eventDef) ||
            !eventDef.IsStartEvent) {
          return ActivityStatus.Waiting;
        }

        if (_step.WorkflowModelItem.Autoactivate) {
          return ActivityStatus.Active;

        } else if (_step.WorkflowModelItem.TargetObject is StepDef stepDef &&
                   stepDef.Autoactivate) {
          return ActivityStatus.Active;

        }

        return ActivityStatus.Pending;
      }
    }

    #endregion Properties

  }  // class DefaultWorkflowStepRulesBuilder

}  //namespace Empiria.Workflow.Execution
