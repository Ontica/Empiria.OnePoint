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


    public List<WorkflowStep> Steps {
      get {
        return _steps.Value;
      }
    }

    #endregion Properties

    #region Methods

    internal WorkflowStep CreateStep(WorkflowModelItem workflowModelItem, WorkflowStep previousStep) {
      Assertion.Require(workflowModelItem, nameof(workflowModelItem));
      Assertion.Require(previousStep, nameof(previousStep));

      return new WorkflowStep(WorkflowInstance, workflowModelItem, previousStep);
    }


    internal void Save() {

      this.WorkflowInstance.Save();

      foreach (WorkflowStep step in Steps) {
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
        WorkflowStep step = CreateStep(modelItem, previousStep);

        Steps.Add(step);

        previousStep = step;
      }

      WorkflowInstance.OnStart();
    }

    #endregion Methods

  }  // class WorkflowInstanceEngine

}  // namespace Empiria.Workflow.Execution
