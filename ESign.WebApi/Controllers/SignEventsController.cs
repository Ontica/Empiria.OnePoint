/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.ESign.WebApi.dll          Pattern   : Web Api Controller                      *
*  Type     : SignEventsController                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web Api used to get electronic sign events.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

namespace Empiria.OnePoint.ESign.WebApi {

  /// <summary>Web Api used to get electronic sign events.</summary>
  public class SignEventsController : WebApiController {


    [HttpGet]
    [Route("v2/e-sign/events/mine")]
    public PagedCollectionModel GetMyLastESignEvents([FromUri] string keywords = "") {
      try {
        FixedList<SignEventDTO> events = ESignUseCases.GetMyLastESignEvents(keywords);

        return new PagedCollectionModel(this.Request, events, typeof(SignEventDTO).FullName);

      } catch (Exception e) {
        EmpiriaLog.Error(e);

        throw base.CreateHttpException(e);

      }
    }


  }  // class SignEventsController

}  // namespace Empiria.OnePoint.WebApi
