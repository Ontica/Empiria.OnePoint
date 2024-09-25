/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Mapper                                  *
*  Type     : WorkflowInstanceMapper                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Maps WorkflowInstance objects to their DTOs.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.StateEnums;

namespace Empiria.Workflow.Execution.Adapters {

  /// <summary>Maps WorkflowInstance objects to their DTOs.</summary>
  static internal class WorkflowInstanceMapper {

    static internal FixedList<WorkflowInstanceDto> Map(FixedList<WorkflowInstance> instances) {
      return instances.Select(x => Map(x))
                      .ToFixedList();
    }


    static internal WorkflowInstanceDto Map(WorkflowInstance instance) {
      return new WorkflowInstanceDto {
        UID = instance.UID,
        Name = instance.Name,
        Description = instance.Description,
        RequestedBy = instance.RequestedBy.MapToNamedEntity(),
        RequestedByOrgUnit = instance.RequestedByOrgUnit.MapToNamedEntity(),
        ResponsibleOrgUnit = instance.ResponsibleOrgUnit.MapToNamedEntity(),
        Priority = instance.Priority.MapToDto(),
        DueTime = instance.DueTime,
        StartTime = instance.StartTime,
        EndTime = instance.EndTime,
        Status = instance.Status.MapToDto(),
        Actions = instance.Actions.MapToDto(),
        ProcessDef = instance.ProcessDefinition.MapToNamedEntity()
      };
    }

  } // class WorkflowInstanceMapper

}  // namespace Empiria.Workflow.Execution.Adapters
