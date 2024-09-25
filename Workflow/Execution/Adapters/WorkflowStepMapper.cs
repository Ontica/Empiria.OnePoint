/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Adpaters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Mapper                                  *
*  Type     : WorkflowStepMapper                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Maps WorkflowStep instances to their DTOs.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.StateEnums;

namespace Empiria.Workflow.Execution.Adapters {

  /// <summary>Maps WorkflowStep instances to their DTOs.</summary>
  static internal class WorkflowStepMapper {

    static internal FixedList<WorkflowStepDto> Map(FixedList<WorkflowStep> steps) {
      return steps.Select(x => Map(x))
                  .ToFixedList();
    }


    static internal WorkflowStepDto Map(WorkflowStep step) {
      return new WorkflowStepDto {
        UID = step.UID,
        StepNo = step.StepNo,
        Name = step.Name,
        Description = step.Description,
        RequestedBy = step.RequestedBy.MapToNamedEntity(),
        RequestedByOrgUnit = step.RequestedByOrgUnit.MapToNamedEntity(),
        AssignedTo = step.AssignedTo.MapToNamedEntity(),
        AssignedToOrgUnit = step.AssignedToOrgUnit.MapToNamedEntity(),
        Priority = step.Priority.MapToDto(),
        DueTime = step.DueTime,
        StartTime = step.StartTime,
        EndTime = step.EndTime,
        Status = step.Status.MapToDto(),
        WorkflowInstance = step.WorkflowInstance.MapToNamedEntity(),
        Actions = step.Actions.MapToDto(),
        StepInvoker = MapStepInvoker(step),

        // ToDo: To be removed

        TaskNo = step.StepNo
      };
    }

    #region Helpers

    static private WorkflowStepInvokerDto MapStepInvoker(WorkflowStep step) {
      return new WorkflowStepInvokerDto {
        UID = step.StepDefinition.Code,
        WorkflowInstanceUID = step.WorkflowInstance.UID
      };
    }

    #endregion Helpers

  }  // class WorkflowStepMapper

}  // namespace Empiria.Workflow.Execution.Adapters
