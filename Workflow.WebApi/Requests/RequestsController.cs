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

    #region Web Apis

    [HttpPost]
    [Route("v4/requests/create")]
    public SingleObjectModel CreateRequest([FromBody] RequestFieldsDto fields) {

      using (var usecases = RequestUseCases.UseCaseInteractor()) {
        RequestDescriptorDto request = usecases.CreateRequest(fields);

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

    #endregion Web Apis

  }  // class RequestsController

}  // namespace Empiria.Workflow.Requests.WebApi
