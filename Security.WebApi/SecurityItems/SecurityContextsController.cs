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

    #endregion Web Apis

  }  // class SecurityContextsController

}  // namespace Empiria.OnePoint.Security.WebApi
