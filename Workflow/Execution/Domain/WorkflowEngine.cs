/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowEngine                             License   : Please read LICENSE.txt file            *
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
  internal class WorkflowEngine {

    #region Fields

    private readonly Lazy<List<WorkflowStep>> _steps;

    #endregion Fields

    #region Constructors and parsers

    internal WorkflowEngine(WorkflowInstance workflowInstance) {
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


    internal void SaveChanges() {
      foreach (WorkflowStep step in Steps) {
        step.Save();
      }
    }


    internal void Start() {
      Assertion.Require(!WorkflowInstance.IsStarted,
                        $"Can not start the WorkflowEngine because its " +
                        $"workflow instance ({WorkflowInstance.Id}) was already started.");

      FixedList <WorkflowModelItem> sequenceFlows = this.ProcessDefinition.GetSequenceFlows();

      foreach (var sequenceFlow in sequenceFlows) {
        var step = new WorkflowStep(WorkflowInstance, sequenceFlow);

        Steps.Add(step);
      }
    }

    #endregion Methods

  }  // class WorkflowEngine

}  // namespace Empiria.Workflow.Execution
