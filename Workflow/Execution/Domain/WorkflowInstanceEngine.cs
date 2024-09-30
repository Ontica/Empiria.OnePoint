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

using Empiria.Workflow.Definition;
using Empiria.Workflow.Execution.Data;

namespace Empiria.Workflow.Execution {

  /// <summary>Performs operations of a workflow process in the context of a workflow instance.</summary>
  internal class WorkflowInstanceEngine {

    #region Fields

    private readonly Lazy<List<WorkflowStep>> _steps;

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

      return workflowStep;
    }


    internal void RemoveStep(WorkflowStep workflowStep) {
      Assertion.Require(workflowStep, nameof(workflowStep));

      Assertion.Require(workflowStep.Actions.CanDelete(), "Can not delete this step.");

      workflowStep.OnRemove();

      _steps.Value.Remove(workflowStep);
    }


    internal void Save() {

      this.WorkflowInstance.Save();

      foreach (WorkflowStep step in GetSteps()) {
        step.Save();
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

        _steps.Value.Add(step);

        previousStep = step;

      }

      WorkflowInstance.OnStart();
    }

    #endregion Methods

  }  // class WorkflowInstanceEngine

}  // namespace Empiria.Workflow.Execution
