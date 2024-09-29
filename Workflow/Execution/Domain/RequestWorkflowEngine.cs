/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Service provider                        *
*  Type     : RequestWorkflowEngine                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Controls workflow operations in the context of a request instance.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using System.Collections.Generic;

using Empiria.StateEnums;

using Empiria.Workflow.Definition;
using Empiria.Workflow.Requests;

using Empiria.Workflow.Execution.Data;

namespace Empiria.Workflow.Execution {

  /// <summary>Controls workflow operations in the context of a request instance.</summary>
  internal class RequestWorkflowEngine {

    #region Fields

    private readonly Request _request;

    private Lazy<List<WorkflowInstance>> _workflowInstances;

    #endregion Fields

    #region Constructors and parsers

    internal RequestWorkflowEngine(Request request) {
      Assertion.Require(request, nameof(request));

      _request = request;

      _workflowInstances = new Lazy<List<WorkflowInstance>>(() => WorkflowExecutionData.GetWorkflowInstances(_request));
    }

    #endregion Constructors and parsers

    #region Properties

    #endregion Properties

    #region Methods

    internal FixedList<WorkflowStep> GetSteps() {
      var allStepsList = new List<WorkflowStep>(32);

      foreach (WorkflowInstance instance in GetWorkflowInstances()) {
        var steps = instance.GetSteps();

        allStepsList.AddRange(steps);
      }
      return allStepsList.ToFixedList();
    }


    public FixedList<WorkflowInstance> GetWorkflowInstances() {
      return _workflowInstances.Value.ToFixedList();
    }


    internal void Save() {

      _request.Save();

      foreach (var workflowInstance in _workflowInstances.Value) {
        workflowInstance.Engine.Save();
      }
    }


    internal void Start() {

      Assertion.Require(!GetWorkflowInstances().Contains(x => x.IsStarted),
                        $"Can not start the RequestWorkflowEngine because has at " +
                        $"least one workflow instance that is already started.");

      ProcessDef processDefinition = _request.RequestDef.DefaultProcessDefinition;

      Assertion.Require(processDefinition.Status == EntityStatus.Active,
                        "El proceso asignado a esta solicitud no está activo. " +
                        "No es posible efectuar la operación.");

      WorkflowInstance workflowInstance = new WorkflowInstance(processDefinition, _request);

      workflowInstance.Engine.Start();

      _workflowInstances.Value.Add(workflowInstance);

      _request.OnStart();
    }

    #endregion Methods

  }  // class RequestWorkflowEngine

}  // namespace Empiria.Workflow.Execution
