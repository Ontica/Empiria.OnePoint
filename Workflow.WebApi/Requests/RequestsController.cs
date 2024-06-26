/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                          Component : Web Api                               *
*  Assembly : Empiria.Workflow.WebApi.dll                  Pattern   : Web api Controller                    *
*  Type     : RequestsController                           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API used to create, update and manage requests.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.Workflow.Requests.Adapters;
using Empiria.Workflow.Requests.UseCases;

namespace Empiria.Workflow.Requests.WebApi {

  /// <summary>Web API used to create, update and manage requests.</summary>
  public class RequestsController : WebApiController {

    #region Query web apis

    [HttpGet]
    [Route("v4/requests/{requestUID}")]
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
        FixedList<RequestDescriptorDto> requests = usecases.SearchRequests(query);

        return new CollectionModel(base.Request, requests);
      }
    }

    #endregion Query web apis

    #region Command web apis

    [HttpPost]
    [Route("v4/requests/{requestUID}/activate")]
    public SingleObjectModel ActivateRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.ActivateRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpPost]
    [Route("v4/requests/{requestUID}/cancel")]
    public SingleObjectModel CancelRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.CancelRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpPost]
    [Route("v4/requests/{requestUID}/close")]
    public SingleObjectModel CloseRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.CloseRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }

    [HttpPost]
    [Route("v4/requests/create")]
    public SingleObjectModel CreateRequest([FromBody] RequestFieldsDto fields) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.CreateRequest(fields);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpDelete]
    [Route("v4/requests/{requestUID}")]
    public NoDataModel DeleteRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        usecases.DeleteRequest(requestUID);

        return new NoDataModel(base.Request);
      }
    }


    [HttpPost]
    [Route("v4/requests/{requestUID}/start")]
    public SingleObjectModel StartRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.StartRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }


    [HttpPost]
    [Route("v4/requests/{requestUID}/suspend")]
    public SingleObjectModel SuspendRequest([FromUri] string requestUID) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestHolderDto request = usecases.SuspendRequest(requestUID);

        return new SingleObjectModel(base.Request, request);
      }
    }

    #endregion Command web apis

  }  // class RequestsController

}  // namespace Empiria.Workflow.Requests.WebApi
