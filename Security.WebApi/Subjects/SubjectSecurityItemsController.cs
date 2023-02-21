/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management                 Component : Web Api                               *
*  Assembly : Empiria.OnePoint.Security.WebApi.dll         Pattern   : Controller                            *
*  Type     : SubjectSecurityItemsController               License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Web api methods for subjects assigned security items in a given execution context.             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using Empiria.OnePoint.Security.Subjects.UseCases;

namespace Empiria.OnePoint.Security.WebApi {

  /// <summary>Web api methods for subjects assigned security items in a given execution context.</summary>
  public class SubjectSecurityItemsController : WebApiController {

    #region Web Apis


    [HttpGet]
    [Route("v4/onepoint/security/management/subjects/{subjectUID:guid}/contexts")]
    public CollectionModel GetSubjectContexts([FromUri] string subjectUID) {

      using (var usecases = SubjectSecurityItemsUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> contexts = usecases.GetSubjectContexts(subjectUID);

        return new CollectionModel(base.Request, contexts);
      }
    }


    [HttpGet]
    [Route("v4/onepoint/security/management/subjects/{subjectUID:guid}/contexts/{contextUID}/features")]
    public CollectionModel GetSubjectFeatures([FromUri] string subjectUID, [FromUri] string contextUID) {

      using (var usecases = SubjectSecurityItemsUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> features = usecases.GetSubjectFeatures(subjectUID, contextUID);

        return new CollectionModel(base.Request, features);
      }
    }


    [HttpGet]
    [Route("v4/onepoint/security/management/subjects/{subjectUID:guid}/contexts/{contextUID}/roles")]
    public CollectionModel GetSubjectRoles([FromUri] string subjectUID, [FromUri] string contextUID) {

      using (var usecases = SubjectSecurityItemsUseCases.UseCaseInteractor()) {
        FixedList<NamedEntityDto> roles = usecases.GetSubjectRoles(subjectUID, contextUID);

        return new CollectionModel(base.Request, roles);
      }
    }

    #endregion Web Apis

  }  // class SubjectSecurityItemsController

}  // namespace Empiria.OnePoint.Security.WebApi
