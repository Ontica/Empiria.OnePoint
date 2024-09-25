/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                           Component : Web Api                               *
*  Assembly : Empiria.Workflow.WebApi.dll                  Pattern   : Web api Controller                    *
*  Type     : WorkflowStepController                       License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API used to create, update and manage workflow steps.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.WebApi;

using Empiria.Workflow.Execution.Adapters;
using Empiria.Workflow.Execution.UseCases;

namespace Empiria.Workflow.Execution.WebApi {

  /// <summary>Web API used to create, update and manage workflow steps.</summary>
  public class WorkflowStepController : WebApiController {

    #region Command web apis

    [HttpPatch, HttpPut]
    [Route("v4/workflow-execution/tasks/{workflowStepUID:guid}")]
    [Route("v4/workflow-execution/steps/{workflowStepUID:guid}")]
    public SingleObjectModel UpdateWorkflowStep([FromUri] string workflowStepUID,
                                                [FromBody] WorkflowStepFields fields) {

      base.RequireBody(fields);

      using (var usecases = WorkflowStepUseCases.UseCaseInteractor()) {
        WorkflowStepDto step = usecases.UpdateWorkflowStep(workflowStepUID, fields);

        return new SingleObjectModel(base.Request, step);
      }
    }

    #endregion Command web apis

  }  // class WorkflowStepController

}  // namespace Empiria.Workflow.Execution.WebApi
