/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Web Api Controller                      *
*  Type     : SignServicesController                       License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api interface to manage document's e-signature services.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;
using System.Web.Http;

using Empiria.Json;
using Empiria.WebApi;
using Empiria.WebApi.Models;

namespace Empiria.OnePoint.ESign.WebApi {

  /// <summary>Services to manage document's e-signature services.</summary>
  public class SignServicesController : WebApiController {

    #region GET methods

    [HttpGet]
    [Route("v2/e-sign/requests/pending/mine")]
    public async Task<CollectionModel> GetMyPendingRequests([FromUri] string filter = "",
                                                            [FromUri] string sort = "") {
      try {
        FixedList<SignRequest> signRequests =
                                    await ESignServices.GetMyPendingSignRequests(filter, sort);

        return new CollectionModel(this.Request, signRequests.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet]
    [Route("v2/e-sign/requests/refused/mine")]
    public async Task<CollectionModel> GetMyRefusedRequests([FromUri] string filter = "",
                                                            [FromUri] string sort = "") {
      try {
        FixedList<SignRequest> refusedRequests =
                                    await ESignServices.GetMyRefusedToSignRequests(filter, sort);

        return new CollectionModel(this.Request, refusedRequests.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet]
    [Route("v2/e-sign/requests/signed/mine")]
    public async Task<CollectionModel> GetMySignedRequests([FromUri] string filter = "",
                                                           [FromUri] string sort = "") {
      try {
        FixedList<SignRequest> signedRequests =
                                      await ESignServices.GetMySignedRequests(filter, sort);

        return new CollectionModel(this.Request, signedRequests.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    #endregion GET methods

    #region POST methods

    [HttpPost]
    [Route("v2/e-sign/requests/signed/mine")]
    public async Task<CollectionModel> SignRequests([FromBody] object body) {
      try {
        SignTask signTask = this.BuildESignTaskFromBody(SignEventType.Signed, body);

        FixedList<SignEvent> signEvents = await ESignServices.Sign(signTask);

        return new CollectionModel(this.Request, signEvents.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpPost]
    [Route("v2/e-sign/requests/refused/mine")]
    public async Task<CollectionModel> RefuseSignRequests([FromBody] object body) {
      try {
        SignTask signTask = this.BuildESignTaskFromBody(SignEventType.Refused, body);

        FixedList<SignEvent> refuseSignEvents = await ESignServices.RefuseSign(signTask);

        return new CollectionModel(this.Request, refuseSignEvents.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpPost]
    [Route("v2/e-sign/requests/revoked/mine")]
    public async Task<CollectionModel> RevokeSignedRequests([FromBody] object body) {
      try {
        SignTask signTask = this.BuildESignTaskFromBody(SignEventType.Revoked, body);

        FixedList<SignEvent> revokeSignEvents = await ESignServices.RevokeSign(signTask);

        return new CollectionModel(this.Request, revokeSignEvents.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpPost]
    [Route("v2/e-sign/requests/unrefused/mine")]
    public async Task<CollectionModel> UnrefuseSignRequests([FromBody] object body) {
      try {
        SignTask signTask = this.BuildESignTaskFromBody(SignEventType.Unrefused, body);

        FixedList<SignEvent> unrefuseSignEvents = await ESignServices.UnrefuseSign(signTask);

        return new CollectionModel(this.Request, unrefuseSignEvents.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    #endregion POST methods

    #region Private methods

    private SignTask BuildESignTaskFromBody(SignEventType eventType, object body) {
      base.RequireBody(body);

      var bodyAsJson = JsonObject.Parse(body);

      bodyAsJson.Add(new JsonItem("eventType", eventType.ToString()));

      SignTask.EnsureIsValid(bodyAsJson);

      return SignTask.Parse(bodyAsJson);
    }

    #endregion Private methods

  }  // class SignServicesController

}  // namespace Empiria.OnePoint.WebApi
