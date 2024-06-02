﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requessts Management                         Component : Web Api                               *
*  Assembly : Empiria.OnePoint.Requests.WebApi.dll         Pattern   : Web api Controller                    *
*  Type     : CataloguesController                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API used to retrive requests related catalogues.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.OnePoint.Requests.UseCases;
using Empiria.OnePoint.Requests.Adapters;

namespace Empiria.OnePoint.Requests.WebApi {

  /// <summary>Web API used to retrive requests related catalogues.</summary>
  public class CataloguesController : WebApiController {

    #region Web Apis

    [HttpGet]
    [Route("v4/requests/catalogues/organizational-units")]
    public CollectionModel GetOrganizationalUnits([FromUri] string requestsList) {

      using (var usecases = CataloguesUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> orgUnits = usecases.OrganizationalUnits(requestsList);

        return new CollectionModel(base.Request, orgUnits);
      }
    }


    [HttpGet]
    [Route("v4/requests/catalogues/requests-types")]
    public CollectionModel GetRequestsTypes([FromUri] string requestsList,
                                            [FromUri] string requesterOrgUnitUID) {

      using (var usecases = CataloguesUseCases.UseCaseInteractor()) {
        FixedList<RequestTypeDto> processTypes = usecases.RequestTypes(requestsList,
                                                                       requesterOrgUnitUID);

        return new CollectionModel(base.Request, processTypes);
      }
    }


    [HttpGet]
    [Route("v4/requests/catalogues/responsible-list")]
    public CollectionModel GetResponsibleList() {

      using (var usecases = CataloguesUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> orgUnits = usecases.ResponsibleList(string.Empty);

        return new CollectionModel(base.Request, orgUnits);
      }
    }


    [HttpGet]
    [Route("v4/requests/catalogues/status-list")]
    public CollectionModel GetStatusList() {

      using (var usecases = CataloguesUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> orgUnits = usecases.StatusList();

        return new CollectionModel(base.Request, orgUnits);
      }
    }

    #endregion Web Apis

  }  // class CataloguesController

}  // namespace Empiria.OnePoint.Requests.WebApi
