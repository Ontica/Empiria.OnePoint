/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                          Component : Web Api                               *
*  Assembly : Empiria.OnePoint.Workflow.WebApi.dll         Pattern   : Web api Controller                    *
*  Type     : ProcessGroupController                       License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web API used to retrive workflow process group related information.                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.Workflow.Definition.Adapters;
using Empiria.Workflow.Definition.UseCases;

namespace Empiria.Workflow.Definition.WebApi {

  /// <summary>Web API used to retrive workflow process group related information.</summary>
  public class ProcessGroupController : WebApiController {

    #region Web Apis

    [HttpGet]
    [Route("v4/workflow/process-groups/{processGroupCode}/organizational-units")]
    public CollectionModel GetOrganizationalUnits([FromUri] string processGroupCode) {

      using (var usecases = ProcessGroupUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> orgUnits = usecases.OrganizationalUnits(processGroupCode);

        return new CollectionModel(base.Request, orgUnits);
      }
    }


    [HttpGet]
    [Route("v4/workflow/process-groups/{processGroupCode}/organizational-units/" +
           "{organizationalUnitUID:guid}/process-types")]
    public CollectionModel GetOrganizationalUnitsProcessTypes([FromUri] string processGroupCode,
                                                              [FromUri] string organizationalUnitUID) {

      using (var usecases = ProcessGroupUseCases.UseCaseInteractor()) {
        FixedList<ProcessDefDto> processTypes = usecases.OrganizationalUnitProcessTypes(processGroupCode,
                                                                                        organizationalUnitUID);

        return new CollectionModel(base.Request, processTypes);
      }
    }

    #endregion Web Apis

  }  // class ProcessGroupController

}  // namespace Empiria.Workflow.Definition.WebApi
