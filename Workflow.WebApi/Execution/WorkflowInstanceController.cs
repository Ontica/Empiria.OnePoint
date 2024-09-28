/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                           Component : Web Api                               *
*  Assembly : Empiria.Workflow.WebApi.dll                  Pattern   : Web api Controller                    *
*  Type     : WorkflowInstanceController                   License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API used to create, update and manage workflow instances.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.WebApi;

using Empiria.Workflow.Execution.Adapters;
using Empiria.Workflow.Execution.UseCases;

namespace Empiria.Workflow.Execution.WebApi {

  /// <summary>Web API used to create, update and manage workflow instances.</summary>
  public class WorkflowInstanceController : WebApiController {

    #region Query web apis

    [HttpGet]
    [Route("v4/workflow/execution/workflow-instances/{workflowInstanceUID:guid}/optional-model-items")]
    public CollectionModel GetOptionalWorkflowModelItems([FromUri] string workflowInstanceUID) {

      using (var usecases = WorkflowInstanceUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> steps = usecases.GetOptionalWorkflowModelItems(workflowInstanceUID);

        return new CollectionModel(base.Request, steps);
      }
    }

    #endregion Query web apis

    #region Command web apis

    [HttpPost]
    [Route("v4/workflow/execution/workflow-instances/{workflowInstanceUID:guid}/steps")]
    public SingleObjectModel InsertWorkflowStep([FromUri] string workflowInstanceUID,
                                                [FromBody] WorkflowStepFields fields) {

      using (var usecases = WorkflowInstanceUseCases.UseCaseInteractor()) {
        WorkflowStepDto step = usecases.InsertWorkflowStep(workflowInstanceUID, fields);

        return new SingleObjectModel(base.Request, step);
      }
    }


    [HttpPatch, HttpPut]
    [Route("v4/workflow/execution/workflow-instances/{workflowInstanceUID:guid}/steps/{workflowStepUID:guid}")]
    public SingleObjectModel UpdateWorkflowStep([FromUri] string workflowInstanceUID,
                                                [FromUri] string workflowStepUID,
                                                [FromBody] WorkflowStepFields fields) {

      base.RequireBody(fields);

      using (var usecases = WorkflowInstanceUseCases.UseCaseInteractor()) {
        WorkflowStepDto step = usecases.UpdateWorkflowStep(workflowInstanceUID, workflowStepUID, fields);

        return new SingleObjectModel(base.Request, step);
      }
    }


    [HttpDelete]
    [Route("v4/workflow/execution/workflow-instances/{workflowInstanceUID:guid}/steps/{workflowStepUID:guid}")]
    public NoDataModel RemoveWorkflowStep([FromUri] string workflowInstanceUID,
                                          [FromUri] string workflowStepUID) {

      using (var usecases = WorkflowInstanceUseCases.UseCaseInteractor()) {
        usecases.RemoveWorkflowStep(workflowInstanceUID, workflowStepUID);

        return new NoDataModel(base.Request);
      }
    }

    #endregion Command web apis

  }  // class WorkflowInstanceController

}  // namespace Empiria.Workflow.Execution.WebApi
