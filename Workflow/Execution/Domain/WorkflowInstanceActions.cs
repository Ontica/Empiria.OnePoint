/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Service provider                        *
*  Type     : WorkflowInstanceActions                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Determines the actions that can be performed in a workflow instance.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.StateEnums;

namespace Empiria.Workflow.Execution {

  /// <summary>Determines the actions that can be performed in a workflow instance.</summary>
  public class WorkflowInstanceActions : IWorkflowActions {

    private readonly WorkflowInstance _workflowInstance;

    internal WorkflowInstanceActions(WorkflowInstance workflowInstance) {
      _workflowInstance = workflowInstance;
    }

    public bool CanActivate() {
      return _workflowInstance.IsRequestActive &&
             _workflowInstance.Status == ActivityStatus.Suspended;
    }


    public bool CanCancel() {
      return _workflowInstance.IsRequestActive &&
              (_workflowInstance.Status == ActivityStatus.Pending ||
              _workflowInstance.Status == ActivityStatus.Active ||
              _workflowInstance.Status == ActivityStatus.Suspended) &&
              _workflowInstance.IsOptional;
    }


    public bool CanComplete() {
      return _workflowInstance.IsRequestActive &&
             _workflowInstance.Status == ActivityStatus.Active;
    }


    public bool CanDelete() {
      return _workflowInstance.IsRequestActive &&
             _workflowInstance.Status == ActivityStatus.Pending &&
             _workflowInstance.IsOptional;
    }


    public bool CanStart() {
      return _workflowInstance.IsRequestActive &&
             _workflowInstance.Status == ActivityStatus.Pending;
    }


    public bool CanSuspend() {
      return _workflowInstance.IsRequestActive &&
             _workflowInstance.Status == ActivityStatus.Active;
    }


    public bool CanUpdate() {
      return _workflowInstance.IsRequestActive &&
              _workflowInstance.Status != ActivityStatus.Canceled &&
              _workflowInstance.Status != ActivityStatus.Deleted &&
              _workflowInstance.Status != ActivityStatus.Completed;
    }

  }  // class WorkflowInstanceActions

}  // namespace Empiria.Workflow.Execution
