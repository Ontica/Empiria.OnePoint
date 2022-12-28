/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Web Api                             Component : User Management                       *
*  Assembly : Empiria.OnePoint.Security.WebApi.dll         Pattern   : Controller                            *
*  Type     : UserCredentialsController                    License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods used to change user credentials.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Json;
using Empiria.WebApi;

using Empiria.Services.Authentication;

using Empiria.OnePoint.Security.UserManagement.UseCases;

namespace Empiria.OnePoint.Security.WebApi {

  /// <summary> Web api methods used to change user credentials.</summary>
  public class UserCredentialsController : WebApiController {

    #region Web Apis

    [HttpPost]
    [Route("v2/security/change-password")]
    [Route("v3/security/change-password")]
    public NoDataModel ChangePassword([FromBody] object body) {
      base.RequireBody(body);

      var json = JsonObject.Parse(body);

      var formData = JsonObject.Parse(json.Get<string>("payload/formData"));
      var currentPassword = formData.Get<string>("current");
      var newPassword = formData.Get<string>("new");

      using (var usecases = UserCredentialsUseCases.UseCaseInteractor()) {
        usecases.ChangeUserPassword(currentPassword, newPassword);

        return new NoDataModel(this.Request);
      }
    }


    [HttpPost]
    [Route("v1/security/change-password/{userEmail}")]
    [Route("v3/security/change-password/{userEmail}")]
    public NoDataModel ChangePassword([FromBody] AuthenticationFields fields,
                                      [FromUri] string userEmail) {
      base.RequireBody(fields);

      using (var usecases = UserCredentialsUseCases.UseCaseInteractor()) {
        usecases.CreateUserPassword(fields.AppKey, fields.UserID,
                                    userEmail, fields.Password);

        return new NoDataModel(this.Request);
      }
    }

    #endregion Web Apis

  }  // class UserCredentialsController

}  // namespace Empiria.OnePoint.Security.WebApi
