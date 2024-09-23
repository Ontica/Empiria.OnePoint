/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Core                              Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Interface                               *
*  Type     : IWorkflowActions                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Interface for workflow objects actions.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow {

  /// <summary>Interface for workflow objects actions.</summary>
  public interface IWorkflowActions {

    bool CanActivate();

    bool CanCancel();

    bool CanComplete();

    bool CanDelete();

    bool CanStart();

    bool CanSuspend();

    bool CanUpdate();

  }  // interface IWorkflowActions




  /// <summary>Extension methods por IWorkflowActions interface.</summary>
  static public class IWorkflowActionsExtensions {

    static public WorkflowActionsDto MapToDto(this IWorkflowActions actions) {
      return new WorkflowActionsDto {
        CanActivate = actions.CanActivate(),
        CanCancel = actions.CanCancel(),
        CanComplete = actions.CanComplete(),
        CanDelete = actions.CanDelete(),
        CanStart = actions.CanStart(),
        CanSuspend = actions.CanSuspend(),
        CanUpdate = actions.CanUpdate()
      };
    }

  }  // class IWorkflowActionsExtensions

}  // namespace Empiria.Workflow
