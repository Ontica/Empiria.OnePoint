/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Web Api Controller                      *
*  Type     : SignEventsController                         License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api interface to get SignEvents entities.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Security;
using Empiria.WebApi;

namespace Empiria.OnePoint.ESign.WebApi {

  /// <summary>Web api interface to get SignEvents entities.</summary>
  public class SignEventsController : WebApiController {

    #region GET methods

    [HttpGet]
    [Route("v2/e-sign/events/mine")]
    public PagedCollectionModel GetMyLastSignEvents([FromUri] string keywords = "") {
      try {
        var me = EmpiriaUser.Current.AsContact();

        FixedList<SignEvent> events = SignServicesRepository.GetLastSignEvents(me, keywords);

        return new PagedCollectionModel(this.Request, events.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    #endregion GET methods

  }  // class SignEventsController

}  // namespace Empiria.OnePoint.WebApi
