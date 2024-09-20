/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowEngine                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs operations of a workflow process in the context of a workflow instance.               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Collections.Generic;

using Empiria.Workflow.Definition;

namespace Empiria.Workflow.Execution {

  /// <summary>Performs operations of a workflow process in the context of a workflow instance.</summary>
  internal class WorkflowEngine {

    #region Fields

    private readonly List<WorkflowStep> _steps = new List<WorkflowStep>(16);

    #endregion Fields

    #region Constructors and parsers

    internal WorkflowEngine(WorkflowInstance workflowInstance) {
      this.WorkflowInstance = workflowInstance;
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

    internal void SaveChanges() {
      foreach (var step in _steps) {
        step.Save();
      }
    }

    internal void Start() {
      FixedList<WorkflowModelItem> sequenceFlows = this.ProcessDefinition.GetSequenceFlows();

      foreach (var sequenceFlow in sequenceFlows) {
        var step = new WorkflowStep(WorkflowInstance, sequenceFlow);

        _steps.Add(step);
      }
    }

    #endregion Methods

  }  // class WorkflowEngine

}  // namespace Empiria.Workflow.Execution
