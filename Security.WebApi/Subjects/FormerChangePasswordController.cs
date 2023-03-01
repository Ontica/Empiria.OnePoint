/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management                 Component : Web Api                               *
*  Assembly : Empiria.OnePoint.Security.WebApi.dll         Pattern   : Controller                            *
*  Type     : SubjectCredentialsController                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods used to change subject credentials.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Json;
using Empiria.WebApi;

using Empiria.OnePoint.Security.Subjects.UseCases;
using Empiria.Security;

namespace Empiria.OnePoint.Security.WebApi {

  /// <summary> Web api methods used to change subject credentials.</summary>
  public class FormerChangePasswordController : WebApiController {

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

      using (var usecases = SubjectCredentialsUseCases.UseCaseInteractor()) {
        usecases.FormerChangeUserPassword(currentPassword, newPassword);

        return new NoDataModel(this.Request);
      }
    }


    [HttpPost]
    [Route("v1/security/change-password/{email}")]
    [Route("v3/security/change-password/{email}")]
    public NoDataModel ChangePassword([FromBody] UserCredentialsDto credentials,
                                      [FromUri] string email) {
      base.RequireBody(credentials);

      using (var usecases = SubjectCredentialsUseCases.UseCaseInteractor()) {
        usecases.FormerCreateUserPassword(credentials, email);

        return new NoDataModel(this.Request);
      }
    }

    #endregion Web Apis

  }  // class SubjectCredentialsController

}  // namespace Empiria.OnePoint.Security.WebApi
