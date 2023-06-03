/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management                 Component : Web Api                               *
*  Assembly : Empiria.OnePoint.Security.WebApi.dll         Pattern   : Query Controller                      *
*  Type     : SecurityContextsController                   License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api query methods that retrieve information about security contexts.                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.OnePoint.Security.SecurityItems.UseCases;
using Empiria.OnePoint.Security.SecurityItems.Adapters;

namespace Empiria.OnePoint.Security.WebApi {

  /// <summary>Web api query methods that retrieve information about security contexts.</summary>
  public class SecurityContextsController : WebApiController {

    #region Web Apis

    [HttpGet]
    [Route("v4/onepoint/security/management/contexts")]
    public CollectionModel GetSecurityContextsList() {

      using (var usecases = SecurityContextsUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> contexts = usecases.SecurityContexts();

        return new CollectionModel(base.Request, contexts);
      }
    }


    [HttpGet]
    [Route("v4/onepoint/security/management/contexts/{contextUID:guid}/roles")]
    public CollectionModel GetSecurityContextRoles([FromUri] string contextUID) {

      using (var usecases = SecurityContextsUseCases.UseCaseInteractor()) {

        FixedList<NamedEntityDto> roles = usecases.SoftwareSystemRoles(contextUID);

        return new CollectionModel(base.Request, roles);
      }
    }


    [HttpGet]
    [Route("v4/onepoint/security/management/contexts/{contextUID:guid}/features")]
    public CollectionModel GetSecurityContextFeatures([FromUri] string contextUID) {

      using (var usecases = SecurityContextsUseCases.UseCaseInteractor()) {

        FixedList<FeatureDto> features = usecases.SecurityContextFeatures(contextUID);

        return new CollectionModel(base.Request, features);
      }
    }

    #endregion Web Apis

  }  // class SecurityContextsController

}  // namespace Empiria.OnePoint.Security.WebApi
