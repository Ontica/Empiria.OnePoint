/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                          Component : Web Api                               *
*  Assembly : Empiria.Workflow.WebApi.dll                  Pattern   : Web api Controller                    *
*  Type     : RequestDefController                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API used to retrive RequestDef instances.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.WebApi;

using Empiria.Workflow.Definition.UseCases;
using Empiria.Workflow.Definition.Adapters;

namespace Empiria.Workflow.Definition.WebApi {

  /// <summary>Web API used to retrive RequestDef instances.</summary>
  public class RequestDefController : WebApiController {

    #region Web Apis

    [HttpGet]
    [Route("v4/requests/catalogues/requests-types")]  // To be removed
    [Route("v4/workflow/definition/requests-definitions")] // ok
    public CollectionModel GetRequestDefList([FromUri] string requestsList,
                                             [FromUri] string requesterOrgUnitUID) {

      using (var usecases = RequestDefUseCases.UseCaseInteractor()) {
        FixedList<RequestDefDto> list = usecases.RequestDefinitions(requestsList,
                                                                    requesterOrgUnitUID);

        return new CollectionModel(base.Request, list);
      }
    }


    [HttpGet]
    [Route("v4/requests/catalogues/requests-types")]  // ToDo: To be removed
    [Route("v4/workflow/definition/requests-definitions")] // ok
    public CollectionModel GetRequestDefList([FromUri] string requestsList) {

      using (var usecases = RequestDefUseCases.UseCaseInteractor()) {
        FixedList<RequestDefDto> list = usecases.RequestDefinitions(requestsList);

        return new CollectionModel(base.Request, list);
      }
    }

    #endregion Web Apis

  }  // class RequestDefController

}  // namespace Empiria.Workflow.Definition.WebApi
