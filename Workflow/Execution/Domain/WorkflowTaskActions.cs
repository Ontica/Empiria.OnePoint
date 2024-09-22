/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Service provider                        *
*  Type     : WorkflowTaskActions                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Determines the actions that can be performed for a workflow instance task.                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.StateEnums;

namespace Empiria.Workflow.Execution {

  /// <summary>Determines the actions that can be performed for a workflow instance task.</summary>
  public class WorkflowTaskActions {

    private readonly WorkflowStep _step;

    internal WorkflowTaskActions(WorkflowStep step) {
      _step = step;
    }


    public bool CanActivate() {
      return _step.IsProcessActive &&
             _step.Status == ActivityStatus.Suspended;
    }


    public bool CanCancel() {
      return _step.IsProcessActive &&
              (_step.Status == ActivityStatus.Pending ||
              _step.Status == ActivityStatus.Active ||
              _step.Status == ActivityStatus.Suspended) &&
              _step.IsOptional;
    }


    public bool CanClose() {
      return _step.IsProcessActive &&
              _step.Status == ActivityStatus.Active;
    }


    public bool CanDelete() {
      return _step.IsProcessActive &&
             _step.Status == ActivityStatus.Pending &&
             _step.IsOptional;
    }


    public bool CanStart() {
      return _step.IsProcessActive &&
             _step.Status == ActivityStatus.Pending;
    }


    public bool CanSuspend() {
      return _step.IsProcessActive &&
             _step.Status == ActivityStatus.Active;
    }


    public bool CanUpdate() {
      return _step.IsProcessActive &&
              _step.Status != ActivityStatus.Canceled &&
              _step.Status != ActivityStatus.Deleted &&
              _step.Status != ActivityStatus.Completed;
    }

  } // class WorkflowTaskActions

}  // namespace Empiria.Workflow.Execution
