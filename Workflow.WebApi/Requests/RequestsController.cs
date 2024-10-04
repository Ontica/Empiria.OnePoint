/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                          Component : Web Api                               *
*  Assembly : Empiria.Workflow.WebApi.dll                  Pattern   : Web api Controller                    *
*  Type     : RequestsController                           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API used to create, update and manage requests.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.WebApi;

using Empiria.Workflow.Requests.Adapters;
using Empiria.Workflow.Requests.UseCases;

namespace Empiria.Workflow.Requests.WebApi {

  /// <summary>Web API used to create, update and manage requests.</summary>
  public class RequestsController : WebApiController {

    #region Query web apis

    [HttpGet]
    [Route("v4/requests/{requestUID:guid}")]
    public SingleObjectModel GetRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.GetRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpPost]
    [Route("v4/requests/search")]
    public CollectionModel SearchRequests([FromBody] RequestsQuery query) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        FixedList<RequestListItemDto> requests = usecases.SearchRequests(query);

        return new CollectionModel(base.Request, requests);
      }
    }

    #endregion Query web apis

    #region Command web apis

    [HttpPost]
    [Route("v4/requests/{requestUID:guid}/activate")]
    public SingleObjectModel ActivateRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.ActivateRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpPost]
    [Route("v4/requests/{requestUID:guid}/cancel")]
    public SingleObjectModel CancelRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.CancelRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpPost]
    [Route("v4/requests/{requestUID:guid}/close")]   // ToDo: Remove this endpoint
    [Route("v4/requests/{requestUID:guid}/complete")]
    public SingleObjectModel CompleteRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.CompleteRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpPost]
    [Route("v4/requests/create")]
    public SingleObjectModel CreateRequest([FromBody] RequestFields fields) {

      base.RequireBody(fields);

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.CreateRequest(fields);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpDelete]
    [Route("v4/requests/{requestUID:guid}")]
    public NoDataModel DeleteRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        usecases.DeleteRequest(requestUID);

        return new NoDataModel(base.Request);
      }
    }


    [HttpPost]
    [Route("v4/requests/{requestUID:guid}/start")]
    public SingleObjectModel StartRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.StartRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpPost]
    [Route("v4/requests/{requestUID:guid}/suspend")]
    public SingleObjectModel SuspendRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.SuspendRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpPatch, HttpPut]
    [Route("v4/requests/{requestUID:guid}")]
    public SingleObjectModel UpdateRequest([FromUri] string requestUID,
                                           [FromBody] RequestFields fields) {

      base.RequireBody(fields);

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.UpdateRequest(requestUID, fields);

        return new SingleObjectModel(base.Request, request);
      }
    }

    #endregion Command web apis

  }  // class RequestsController

}  // namespace Empiria.Workflow.Requests.WebApi
