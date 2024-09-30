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
using Empiria.Workflow.Execution.Adapters;

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


    public WorkflowStepDto InsertWorkflowStep(string workflowInstanceUID, WorkflowStepFields fields) {
      Assertion.Require(workflowInstanceUID, nameof(workflowInstanceUID));
      Assertion.Require(fields, nameof(fields));

      var workflowInstance = WorkflowInstance.Parse(workflowInstanceUID);

      fields.EnsureValid();

      WorkflowInstanceEngine engine = workflowInstance.GetEngine();

      WorkflowStep step = engine.CreateStep(fields.GetWorkflowModelItem());

      step.Update(fields);

      step = engine.InsertStep(step);

      engine.Save();

      return WorkflowStepMapper.Map(step);
    }


    public void RemoveWorkflowStep(string workflowInstanceUID, string workflowStepUID) {
      Assertion.Require(workflowInstanceUID, nameof(workflowInstanceUID));
      Assertion.Require(workflowStepUID, nameof(workflowStepUID));

      var workflowInstance = WorkflowInstance.Parse(workflowInstanceUID);

      var workflowStep = workflowInstance.GetStep(workflowStepUID);

      WorkflowInstanceEngine engine = workflowInstance.GetEngine();

      engine.RemoveStep(workflowStep);

      engine.Save();
    }


    public WorkflowStepDto UpdateWorkflowStep(string workflowInstanceUID,
                                              string workflowStepUID,
                                              WorkflowStepFields fields) {
      Assertion.Require(workflowInstanceUID, nameof(workflowInstanceUID));
      Assertion.Require(workflowStepUID, nameof(workflowStepUID));


      var workflowInstance = WorkflowInstance.Parse(workflowInstanceUID);

      var workflowStep = workflowInstance.GetStep(workflowStepUID);

      workflowStep.Update(fields);

      workflowStep.Save();

      return WorkflowStepMapper.Map(workflowStep);
    }

    #endregion Use cases

  }  // class WorkflowInstanceUseCases

}  // namespace Empiria.Workflow.Execution.UseCases
