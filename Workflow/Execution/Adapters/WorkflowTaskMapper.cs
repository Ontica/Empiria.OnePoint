/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Adpaters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Mapper                                  *
*  Type     : WorkflowTaskMapper                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Maps WorkflowTask instances to their DTOs.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.StateEnums;

namespace Empiria.Workflow.Execution.Adapters {

  /// <summary>Maps WorkflowTask instances to their DTOs.</summary>
  static internal class WorkflowTaskMapper {

    internal static FixedList<TaskDto> Map(WorkflowInstance workflowInstance) {
      FixedList<WorkflowTask> tasks = workflowInstance.GetTasks();

      return tasks.Select(x => Map(x)).ToFixedList();

    }

    static internal TaskDto Map(WorkflowTask task) {
      return new TaskDto {
        UID = task.UID,
        TaskNo = task.TaskNo,
        Name = task.Name,
        Description = task.Description,
        RequestedBy = task.RequestedBy.MapToNamedEntity(),
        RequestedByOrgUnit = task.RequestedByOrgUnit.MapToNamedEntity(),
        AssignedTo = task.AssignedTo.MapToNamedEntity(),
        AssignedToOrgUnit = task.AssignedToOrgUnit.MapToNamedEntity(),
        Deadline = task.Deadline,
        CheckInTime = task.CheckInTime,
        EndTime = task.EndTime,
        CheckOutTime = task.CheckOutTime,
        Status = new NamedEntityDto(task.Status.ToString(), task.Status.GetName()),
        TaskInvoker = MapTaskInvoker(task)
      };
    }

    #region Helpers

    static private TaskInvokerDto MapTaskInvoker(WorkflowTask task) {
      return new TaskInvokerDto {
        UID = task.WorkflowObject.Code,
        WorkflowInstanceUID = task.WorkflowInstance.UID
      };
    }

    #endregion Helpers

  }  // class WorkflowTaskMapper

}  // namespace Empiria.Workflow.Execution.Adapters
