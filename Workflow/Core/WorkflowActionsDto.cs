/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Core                              Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Output DTO                              *
*  Type     : WorkflowActionsDto                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Base output DTO for workflow object actions.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow {

  /// <summary>Base output DTO for workflow object actions.</summary>
  public class WorkflowActionsDto {

    public bool CanActivate {
      get; internal set;
    }

    public bool CanCancel {
      get; internal set;
    }

    public bool CanComplete {
      get; internal set;
    }

    public bool CanDelete {
      get; internal set;
    }

    public bool CanInsertWorkItems {
      get; internal set;
    }

    public bool CanStart {
      get; internal set;
    }

    public bool CanSuspend {
      get; internal set;
    }

    public bool CanUpdate {
      get; internal set;
    }

  }  // class WorkflowActionsDto

}  // namespace Empiria.Workflow
