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

  }  // class WorkflowInstanceController

}  // namespace Empiria.Workflow.Execution.WebApi
