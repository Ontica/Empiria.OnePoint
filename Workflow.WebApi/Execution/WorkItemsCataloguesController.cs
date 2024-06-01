/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                           Component : Web Api                               *
*  Assembly : Empiria.OnePoint.Workflow.WebApi.dll         Pattern   : Web api Controller                    *
*  Type     : WorkItemsCataloguesController                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API used to retrive workflow items catalogues.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.Workflow.Execution.UseCases;

namespace Empiria.Workflow.Execution.WebApi {

  /// <summary>Web API used to retrive workflow items catalogues.</summary>
  public class WorkItemsCataloguesController : WebApiController {

    #region Web Apis

    [HttpGet]
    [Route("v4/workflow/catalogues/work-items/status-list")]
    public CollectionModel GetWorkItemsStatusList() {

      using (var usecases = WorkItemsCataloguesUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> orgUnits = usecases.StatusList();

        return new CollectionModel(base.Request, orgUnits);
      }
    }


    [HttpGet]
    [Route("v4/workflow/catalogues/work-items/responsible-list")]
    public CollectionModel GetWorkItemsResponsiblesList() {

      using (var usecases = WorkItemsCataloguesUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> orgUnits = usecases.ResponsiblesList(string.Empty);

        return new CollectionModel(base.Request, orgUnits);
      }
    }

    #endregion Web Apis

  }  // class WorkItemsCataloguesController

}  // namespace Empiria.Workflow.Execution.WebApi
