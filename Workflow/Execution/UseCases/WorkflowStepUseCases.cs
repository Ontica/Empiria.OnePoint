/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Use case interactor class               *
*  Type     : WorkflowStepUseCases                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for workflow steps.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services;

using Empiria.Workflow.Execution.Adapters;

namespace Empiria.Workflow.Execution.UseCases {

  /// <summary>Use cases for workflow steps.</summary>
  public class WorkflowStepUseCases : UseCase {

    #region Constructors and parsers

    protected WorkflowStepUseCases() {
      // no-op
    }

    static public WorkflowStepUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<WorkflowStepUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public TaskDto UpdateWorkflowStep(string workflowStepUID, WorkflowStepFields fields) {
      Assertion.Require(workflowStepUID, nameof(workflowStepUID));
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      var workflowStep = WorkflowStep.Parse(workflowStepUID);

      Assertion.Require(workflowStep.Actions.CanUpdate(),
                        $"Esta tarea está en un estado que no permite actualizarse.");

      workflowStep.Update(fields);

      workflowStep.Save();

      return WorkflowTaskMapper.Map(new WorkflowTask(workflowStep));
    }

    #endregion Use cases

  }  // class WorkflowStepUseCases

}  // namespace Empiria.Workflow.Execution.UseCases
