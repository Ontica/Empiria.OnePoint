/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowInstanceEngine                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs operations of a workflow process in the context of a workflow instance.               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;

using Empiria.StateEnums;

using Empiria.Workflow.Definition;
using Empiria.Workflow.Execution.Data;

namespace Empiria.Workflow.Execution {

  /// <summary>Performs operations of a workflow process in the context of a workflow instance.</summary>
  internal class WorkflowInstanceEngine {

    #region Fields

    private readonly Lazy<List<WorkflowStep>> _steps;

    private readonly List<WorkflowStep> _removedSteps = new List<WorkflowStep>();

    #endregion Fields

    #region Constructors and parsers

    internal WorkflowInstanceEngine(WorkflowInstance workflowInstance) {
      this.WorkflowInstance = workflowInstance;

      _steps = new Lazy<List<WorkflowStep>>(() => WorkflowExecutionData.GetSteps(workflowInstance));
    }

    #endregion Constructors and parsers

    #region Properties

    private ProcessDef ProcessDefinition {
      get {
        return this.WorkflowInstance.ProcessDefinition;
      }
    }

    public WorkflowInstance WorkflowInstance {
      get;
    }

    #endregion Properties

    #region Methods

    internal WorkflowStep CreateStep(WorkflowModelItem workflowModelItem) {
      Assertion.Require(workflowModelItem, nameof(workflowModelItem));

      return new WorkflowStep(WorkflowInstance, workflowModelItem);
    }


    public FixedList<WorkflowStep> GetSteps() {
      return _steps.Value.ToFixedList();
    }


    internal WorkflowStep InsertStep(WorkflowStep workflowStep) {
      Assertion.Require(workflowStep, nameof(workflowStep));

      Assertion.Require(workflowStep.WorkflowInstance.Equals(this.WorkflowInstance),
                        $"Workflow instance mismatch.");

      _steps.Value.Add(workflowStep);

      UpdateStepsNumbers();

      return workflowStep;
    }


    internal void RemoveStep(WorkflowStep workflowStep) {
      Assertion.Require(workflowStep, nameof(workflowStep));

      Assertion.Require(workflowStep.Actions.CanDelete(), "Can not delete this step.");

      var antecedents = _steps.Value.FindAll(x => x.NextStep.Equals(workflowStep));

      foreach (WorkflowStep antecedent in antecedents) {
        antecedent.SetNextStep(workflowStep.NextStep);
      }

      var subsequents = _steps.Value.FindAll(x => x.PreviousStep.Equals(workflowStep));

      foreach (WorkflowStep subsequent in subsequents) {
        subsequent.SetPreviousStep(workflowStep.PreviousStep);

        if (workflowStep.Status == ActivityStatus.Pending &&
            subsequent.Status == ActivityStatus.Waiting) {
          subsequent.OnPrepare();
        }
      }

      workflowStep.OnRemove();

      _removedSteps.Add(workflowStep);

      _steps.Value.Remove(workflowStep);

      UpdateStepsNumbers();
    }


    internal void Save() {

      this.WorkflowInstance.Save();

      foreach (WorkflowStep step in GetSteps()) {
        step.Save();
      }

      foreach (WorkflowStep removedStep in _removedSteps) {
        removedStep.Save();
      }
    }


    internal void Start() {
      Assertion.Require(!WorkflowInstance.IsStarted,
                        $"Can not start the WorkflowEngine because its " +
                        $"workflow instance {WorkflowInstance.Id} was already started.");

      FixedList<WorkflowModelItem> modelItems = this.ProcessDefinition.GetWorkflowModelItems();

      WorkflowStep previousStep = WorkflowStep.Empty;

      foreach (WorkflowModelItem modelItem in modelItems) {
        WorkflowStep step = CreateStep(modelItem);

        step.SetPreviousStep(previousStep);

        if (!previousStep.IsEmptyInstance) {
          previousStep.SetNextStep(step);
        }

        _steps.Value.Add(step);

        previousStep = step;

      }  // foreach

      WorkflowInstance.OnStart();
    }

    #endregion Methods

    #region Helpers

    private string GetStepNewNo(WorkflowStep step, int position) {
      return position.ToString("D2");
    }


    private void UpdateStepsNumbers() {

      for (int i = 0; i < _steps.Value.Count; i++) {
        var step = _steps.Value[i];

        string newStepNo = GetStepNewNo(step, i + 1);

        if (step.StepNo != newStepNo) {
          step.SetStepNo(newStepNo);
        }
      }
    }

    #endregion Helpers

  }  // class WorkflowInstanceEngine

}  // namespace Empiria.Workflow.Execution
