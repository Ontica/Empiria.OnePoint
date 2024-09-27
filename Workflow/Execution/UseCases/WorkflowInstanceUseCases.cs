/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Use case interactor class               *
*  Type     : WorkflowInstanceUseCases                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for workflow instances.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services;

using Empiria.Workflow.Definition;

namespace Empiria.Workflow.Execution.UseCases {

  /// <summary>Use cases for workflow instances.</summary>
  public class WorkflowInstanceUseCases : UseCase {

    #region Constructors and parsers

    protected WorkflowInstanceUseCases() {
      // no-op
    }

    static public WorkflowInstanceUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<WorkflowInstanceUseCases>();
    }



    #endregion Constructors and parsers

    #region Use cases

    public FixedList<NamedEntityDto> GetOptionalWorkflowModelItems(string workflowInstanceUID) {
      Assertion.Require(workflowInstanceUID, nameof(workflowInstanceUID));

      var workflowInstance = WorkflowInstance.Parse(workflowInstanceUID);

      FixedList<WorkflowModelItem> items = workflowInstance.ProcessDefinition.GetOptionalWorkflowModelItems();

      return items.MapToNamedEntityList();
    }

    #endregion Use cases

  }  // class WorkflowInstanceUseCases

}  // namespace Empiria.Workflow.Execution.UseCases
