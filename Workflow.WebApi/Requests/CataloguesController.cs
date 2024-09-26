/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                          Component : Web Api                               *
*  Assembly : Empiria.Workflow.WebApi.dll                  Pattern   : Web api Controller                    *
*  Type     : CataloguesController                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API used to retrive requests related catalogues.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Web.Http;

using Empiria.WebApi;

using Empiria.Workflow.Requests.UseCases;

namespace Empiria.Workflow.Requests.WebApi {

  /// <summary>Web API used to retrive requests related catalogues.</summary>
  public class CataloguesController : WebApiController {

    #region Web Apis

    [HttpGet]
    [Route("v4/requests/catalogues/organizational-units")]
    public CollectionModel GetOrganizationalUnits([FromUri] string requestsList) {

      using (var usecases = CataloguesUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> list = usecases.OrganizationalUnits(requestsList);

        return new CollectionModel(base.Request, list);
      }
    }


    [HttpGet]
    [Route("v4/requests/catalogues/responsible-list")]
    public CollectionModel GetResponsibleList() {

      using (var usecases = CataloguesUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> list = usecases.ResponsibleList(string.Empty);

        return new CollectionModel(base.Request, list);
      }
    }


    [HttpGet]
    [Route("v4/requests/catalogues/status-list")]
    public CollectionModel GetStatusList() {

      using (var usecases = CataloguesUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> list = usecases.StatusList();

        return new CollectionModel(base.Request, list);
      }
    }

    #endregion Web Apis

  }  // class CataloguesController

}  // namespace Empiria.Workflow.Requests.WebApi
