/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowEngine                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs operations of a workflow process in the context of a workflow instance.               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Workflow.Definition;

namespace Empiria.Workflow.Execution {

  /// <summary>Performs operations of a workflow process in the context of a workflow instance.</summary>
  internal class WorkflowEngine {

    #region Constructors and parsers

    internal WorkflowEngine(WorkflowInstance workflowInstance) {
      this.WorkflowInstance = workflowInstance;
    }

    #endregion Constructors and parsers

    #region Properties

    public WorkflowInstance WorkflowInstance {
      get;
    }

    private ProcessDef ProcessDefinition {
      get {
        return this.WorkflowInstance.ProcessDefinition;
      }
    }

    #endregion Properties

    #region Methods

    internal void SaveChanges() {

    }

    internal void Start() {
      FixedList<WorkflowModelItem> sequenceFlows = this.ProcessDefinition.GetSequenceFlows();

    }

    #endregion Methods

  }  // class WorkflowEngine

}  // namespace Empiria.Workflow.Execution
