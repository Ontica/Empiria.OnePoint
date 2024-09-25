/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Service provider                        *
*  Type     : WorkflowStepActions                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Determines the actions that can be performed in the context of a WorkflowStep.                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.StateEnums;

namespace Empiria.Workflow.Execution {

  /// <summary>Determines the actions that can be performed in the context of a WorkflowStep.</summary>
  public class WorkflowStepActions : IWorkflowActions {

    private readonly WorkflowStep _step;

    internal WorkflowStepActions(WorkflowStep step) {
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


    public bool CanComplete() {
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

  } // class WorkflowStepActions

}  // namespace Empiria.Workflow.Execution
