/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Web Api                             Component : User Management                       *
*  Assembly : Empiria.OnePoint.Security.WebApi.dll         Pattern   : Controller                            *
*  Type     : UserManagementController                     License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods for user management.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.OnePoint.Security.UserManagement.Adapters;
using Empiria.OnePoint.Security.UserManagement.UseCases;

namespace Empiria.OnePoint.Security.WebApi {

  /// <summary>Web api methods for user management.</summary>
  public class UserManagementController : WebApiController {

    #region Web Apis

    [HttpGet]
    [Route("v3/onepoint/security/management/users")]
    public CollectionModel GetUsers([FromUri] string keywords = "") {

      keywords = keywords ?? string.Empty;

      using (var usecases = UserManagementUseCases.UseCaseInteractor()) {
        FixedList<UserDto> users = usecases.SearchUsers(keywords);

        return new CollectionModel(base.Request, users);
      }
    }


    [HttpPost]
    [Route("v3/onepoint/security/management/users")]
    public NoDataModel CreateUser([FromBody] UserFields userFields) {

      base.RequireBody(userFields);

      using (var usecases = UserManagementUseCases.UseCaseInteractor()) {
        usecases.CreateUser(userFields);

        return new NoDataModel(base.Request);
      }
    }


    [HttpDelete]
    [Route("v3/onepoint/security/management/users/{userUID:guid}")]
    public NoDataModel RemoveUser([FromUri] string userUID) {

      using (var usecases = UserManagementUseCases.UseCaseInteractor()) {
        usecases.RemoveUser(userUID);

        return new NoDataModel(base.Request);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v3/onepoint/security/management/users/{userUID:guid}")]
    public SingleObjectModel UpdateUser([FromUri] string userUID, [FromBody] UserFields userFields) {

      base.RequireBody(userFields);

      using (var usecases = UserManagementUseCases.UseCaseInteractor()) {
        UserDto user = usecases.UpdateUser(userUID, userFields);

        return new SingleObjectModel(base.Request, user);
      }
    }

    #endregion Web Apis

  }  // class UserManagementController

}  // namespace Empiria.OnePoint.Security.WebApi
