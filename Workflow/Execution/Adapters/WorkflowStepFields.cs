/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Fields Input DTO                        *
*  Type     : WorkflowStepFields                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Fields input DTO used to update workflow steps.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Empiria.Commands;
using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.Workflow.Definition;
using Empiria.Workflow.Requests;

namespace Empiria.Workflow.Execution.Adapters {

  /// <summary>Fields input DTO used to update workflow steps.</summary>
  public class WorkflowStepFields {

    public string Description {
      get; set;
    } = string.Empty;


    public string RequestedByUID {
      get; set;
    } = string.Empty;


    public string RequestedByOrgUnitUID {
      get; set;
    } = string.Empty;


    public string AssignedToUID {
      get; set;
    } = string.Empty;


    public string AssignedToOrgUnitUID {
      get; set;
    } = string.Empty;


    public Priority Priority {
      get; set;
    } = Priority.Normal;


    public DateTime DueTime {
      get; set;
    } = ExecutionServer.DateMaxValue;


    public PositioningRule PositioningRule {
      get; set;
    } = PositioningRule.AtEnd;


    public string PositioningOffsetStepUID {
      get; set;
    } = string.Empty;


    public string RequestUID {
      get; set;
    } = string.Empty;


    public string WorkflowInstanceUID {
      get; set;
    } = string.Empty;


    public string WorkflowModelItemUID {
      get; set;
    } = string.Empty;

  }  // class WorkflowStepFields



  /// <summary>Extension methods for WorkflowStepFields class.</summary>
  static internal class WorkflowStepFieldsExtensions {

    static internal void EnsureValid(this WorkflowStepFields fields) {
      Assertion.Require(fields.Description, nameof(fields.Description));
      Assertion.Require(fields.RequestUID, nameof(fields.RequestUID));
      Assertion.Require(fields.WorkflowInstanceUID, nameof(fields.WorkflowInstanceUID));
      Assertion.Require(fields.WorkflowModelItemUID, nameof(fields.WorkflowModelItemUID));

      _ = GetAssignedTo(fields);
      _ = GetAssignedToOrgUnit(fields);
      _ = GetRequestedBy(fields);
      _ = GetRequestedByOrgUnit(fields);
      _ = GetRequest(fields);
      _ = GetWorkflowInstance(fields);
      _ = GetWorkflowModelItem(fields);
    }


    static internal Party GetAssignedTo(this WorkflowStepFields fields) {
      if (string.IsNullOrWhiteSpace(fields.AssignedToUID)) {
        return Party.Empty;
      }
      return Party.Parse(fields.AssignedToUID);
    }


    static internal OrganizationalUnit GetAssignedToOrgUnit(this WorkflowStepFields fields) {
      if (string.IsNullOrWhiteSpace(fields.AssignedToOrgUnitUID)) {
        return OrganizationalUnit.Empty;
      }
      return OrganizationalUnit.Parse(fields.AssignedToOrgUnitUID);
    }


    static internal StepInsertionPoint GetInsertionPoint(this WorkflowStepFields fields) {
      Assertion.Require(fields.PositioningRule != PositioningRule.Undefined, nameof(fields.PositioningRule));

      if (fields.PositioningRule == PositioningRule.AtStart ||
          fields.PositioningRule == PositioningRule.AtEnd) {

        return new StepInsertionPoint(fields.PositioningRule, WorkflowStep.Empty);
      }

      Assertion.Require(fields.PositioningOffsetStepUID, nameof(fields.PositioningOffsetStepUID));

      return new StepInsertionPoint(fields.PositioningRule,
                                    WorkflowStep.Parse(fields.PositioningOffsetStepUID));
    }


    static internal Request GetRequest(this WorkflowStepFields fields) {
      return Request.Parse(fields.RequestUID);
    }


    static internal Party GetRequestedBy(this WorkflowStepFields fields) {
      if (string.IsNullOrWhiteSpace(fields.RequestedByUID)) {
        return Party.Empty;
      }
      return Party.Parse(fields.RequestedByUID);
    }


    static internal OrganizationalUnit GetRequestedByOrgUnit(this WorkflowStepFields fields) {
      if (string.IsNullOrWhiteSpace(fields.RequestedByOrgUnitUID)) {
        return OrganizationalUnit.Empty;
      }
      return OrganizationalUnit.Parse(fields.RequestedByOrgUnitUID);
    }


    static internal WorkflowInstance GetWorkflowInstance(this WorkflowStepFields fields) {
      return WorkflowInstance.Parse(fields.WorkflowInstanceUID);
    }


    static internal WorkflowModelItem GetWorkflowModelItem(this WorkflowStepFields fields) {
      return WorkflowModelItem.Parse(fields.WorkflowModelItemUID);
    }

  }  // class WorkflowStepFieldsExtensions

}  // namespace Empiria.Workflow.Execution.Adapters
