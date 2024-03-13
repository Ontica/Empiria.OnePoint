/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management                 Component : Web Api                               *
*  Assembly : Empiria.OnePoint.Security.WebApi.dll         Pattern   : Controller                            *
*  Type     : SubjectsController                           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods for subjects management.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.Security;

using Empiria.OnePoint.Security.Subjects.Adapters;
using Empiria.OnePoint.Security.Subjects.UseCases;

namespace Empiria.OnePoint.Security.WebApi {

  /// <summary>Web api methods for subjects management.</summary>
  public class SubjectsController : WebApiController {

    #region Query apis

    [HttpPost, AllowAnonymous]
    [Route("v4/onepoint/security/management/new-credentials-token")]
    public SingleObjectModel GenerateNewCredentialsToken([FromBody] UserCredentialsDto credentials) {

      PrepareAuthenticationFields(credentials);

      using (var usecases = SubjectCredentialsUseCases.UseCaseInteractor()) {
        string token = usecases.GenerateNewCredentialsToken(credentials);

        return new SingleObjectModel(base.Request, token);
      }
    }


    [HttpPost]
    [Route("v4/onepoint/security/management/subjects/search")]
    public CollectionModel GetSubjects([FromBody] SubjectsQuery query) {

      RequireBody(query);

      using (var usecases = SubjectUseCases.UseCaseInteractor()) {
        FixedList<SubjectDto> subjects = usecases.SearchSubjects(query);

        return new CollectionModel(base.Request, subjects);
      }
    }


    [HttpGet]
    [Route("v4/onepoint/security/management/subjects/workareas")]
    public CollectionModel GetWorkareas() {

      using (var usecases = SubjectUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> workareas = usecases.Workareas();

        return new CollectionModel(base.Request, workareas);
      }
    }

    #endregion Query apis

    #region Command apis


    [HttpPost]
    [Route("v4/onepoint/security/management/subjects/{subjectUID:guid}/activate")]
    public SingleObjectModel ActivateSubject([FromUri] string subjectUID) {

      using (var usecases = SubjectUseCases.UseCaseInteractor()) {
        SubjectDto subject = usecases.ActivateSubject(subjectUID);

        return new SingleObjectModel(base.Request, subject);
      }
    }


    [HttpPost]
    [Route("v4/onepoint/security/management/subjects")]
    public SingleObjectModel CreateSubject([FromBody] SubjectFields fields) {

      base.RequireBody(fields);

      using (var usecases = SubjectUseCases.UseCaseInteractor()) {
        SubjectDto subject = usecases.CreateSubject(fields);

        return new SingleObjectModel(base.Request, subject);
      }
    }


    [HttpDelete]
    [Route("v4/onepoint/security/management/subjects/{subjectUID:guid}")]
    public NoDataModel RemoveSubject([FromUri] string subjectUID) {

      using (var usecases = SubjectUseCases.UseCaseInteractor()) {
        usecases.RemoveSubject(subjectUID);

        return new NoDataModel(base.Request);
      }
    }



    [HttpPost]
    [Route("v4/onepoint/security/management/subjects/{subjectUID:guid}/reset-credentials")]
    public NoDataModel ResetCredentials([FromUri] string subjectUID) {

      using (var usecases = SubjectCredentialsUseCases.UseCaseInteractor()) {
        usecases.ResetCredentials(subjectUID);

        return new NoDataModel(base.Request);
      }
    }


    [HttpPost]
    [Route("v4/onepoint/security/management/subjects/{subjectUID:guid}/suspend")]
    public SingleObjectModel SuspendSubject([FromUri] string subjectUID) {

      using (var usecases = SubjectUseCases.UseCaseInteractor()) {
        SubjectDto subject = usecases.SuspendSubject(subjectUID);

        return new SingleObjectModel(base.Request, subject);
      }
    }


    [HttpPost, AllowAnonymous]
    [Route("v4/onepoint/security/management/update-my-credentials")]
    public NoDataModel UpdateCredentials([FromBody] UpdateCredentialsFields fields) {

      base.RequireBody(fields);

      using (var usecases = SubjectCredentialsUseCases.UseCaseInteractor()) {
        usecases.UpdateCredentials(fields);

        return new NoDataModel(base.Request);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v4/onepoint/security/management/subjects/{subjectUID:guid}")]
    public SingleObjectModel UpdateSubject([FromUri] string subjectUID,
                                           [FromBody] SubjectFields fields) {

      base.RequireBody(fields);

      using (var usecases = SubjectUseCases.UseCaseInteractor()) {
        SubjectDto subject = usecases.UpdateSubject(subjectUID, fields);

        return new SingleObjectModel(base.Request, subject);
      }
    }

    #endregion Command apis


    #region Helpers

    private void PrepareAuthenticationFields(UserCredentialsDto credentials) {
      base.RequireHeader("User-Agent");
      base.RequireBody(credentials);

      credentials.AppKey = base.GetRequestHeader<string>("ApplicationKey");
      credentials.UserHostAddress = base.GetClientIpAddress();
    }

    #endregion Helpers

  }  // class SubjectsController

}  // namespace Empiria.OnePoint.Security.WebApi
