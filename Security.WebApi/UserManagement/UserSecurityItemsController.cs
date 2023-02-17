/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Web Api                             Component : User Management                       *
*  Assembly : Empiria.OnePoint.Security.WebApi.dll         Pattern   : Controller                            *
*  Type     : UserSecurityItemsController                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods fo user's assigned security items.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.OnePoint.Security.UserManagement.UseCases;

namespace Empiria.OnePoint.Security.WebApi {

  /// <summary>Web api methods fo user's assigned security items.</summary>
  public class UserSecurityItemsController : WebApiController {

    #region Web Apis

    [HttpGet]
    [Route("v3/onepoint/security/management/users/{userUID:guid}/features")]
    public CollectionModel GetUserFeatures([FromUri] string userUID) {

      using (var usecases = UserSecurityItemsUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> userFeatures = usecases.GetUserFeatures(userUID);

        return new CollectionModel(base.Request, userFeatures);
      }
    }


    [HttpGet]
    [Route("v3/onepoint/security/management/users/{userUID:guid}/roles")]
    public CollectionModel GetUserRoles([FromUri] string userUID) {

      using (var usecases = UserSecurityItemsUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> userRoles = usecases.GetUserRoles(userUID);

        return new CollectionModel(base.Request, userRoles);
      }
    }

    #endregion Web Apis

  }  // class UserSecurityItemsController

}  // namespace Empiria.OnePoint.Security.WebApi
