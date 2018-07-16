/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Web Api Controller                      *
*  Type     : SignRequestsController                       License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api interface to manage document's e-signature services through SignRequest entities.      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Json;
using Empiria.Security;
using Empiria.WebApi;

using Empiria.OnePoint.AppServices;

namespace Empiria.OnePoint.ESign.WebApi {

  /// <summary>Web api interface to manage document's e-signature services
  /// through SignRequest entities.</summary>
  public class SignRequestsController : WebApiController {

    #region GET methods

    [AllowAnonymous]
    [HttpGet]
    [Route("v2/e-sign/requests/by-document-no/{documentNo}")]
    public SingleObjectModel GetSignRequest(string documentNo) {
      try {
        SignRequest signRequest = SignServicesRepository.GetRequestByDocumentNo(documentNo);

        return new SingleObjectModel(this.Request, signRequest.ToResponse(),
                                     typeof(SignRequest).FullName);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    [HttpGet]
    [Route("v2/e-sign/requests/mine")]
    public CollectionModel GetMyRequests([FromUri] string keywords = "") {
      try {
        var me = EmpiriaUser.Current.AsContact();

        FixedList<SignRequest> signRequests =
                                  SignServicesRepository.GetAllRequests(me, keywords);

        return new CollectionModel(this.Request, signRequests.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet]
    [Route("v2/e-sign/requests/mine/pending")]
    public CollectionModel GetMyPendingRequests([FromUri] string keywords = "") {
      try {
        var me = EmpiriaUser.Current.AsContact();

        FixedList<SignRequest> signRequests =
                                  SignServicesRepository.GetPendingSignRequests(me, keywords);

        return new CollectionModel(this.Request, signRequests.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet]
    [Route("v2/e-sign/requests/mine/refused")]
    public CollectionModel GetMyRefusedRequests([FromUri] string keywords = "") {
      try {
        var me = EmpiriaUser.Current.AsContact();

        FixedList<SignRequest> refusedRequests =
                                    SignServicesRepository.GetRefusedRequests(me, keywords);

        return new CollectionModel(this.Request, refusedRequests.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet]
    [Route("v2/e-sign/requests/mine/revoked")]
    public CollectionModel GetMyRevokedRequests([FromUri] string keywords = "") {
      try {
        var me = EmpiriaUser.Current.AsContact();

        FixedList<SignRequest> revokedRequests =
                                    SignServicesRepository.GetRevokedRequests(me, keywords);

        return new CollectionModel(this.Request, revokedRequests.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    [HttpGet]
    [Route("v2/e-sign/requests/mine/signed")]
    public CollectionModel GetMySignedRequests([FromUri] string keywords = "") {
      try {
        var me = EmpiriaUser.Current.AsContact();

        FixedList<SignRequest> signedRequests =
                                      SignServicesRepository.GetSignedRequests(me, keywords);

        return new CollectionModel(this.Request, signedRequests.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    #endregion GET methods

    #region POST methods

    [HttpPost]
    [Route("v2/e-sign/requests/mine/signed")]
    public CollectionModel SignRequests([FromBody] object body) {
      try {
        SignTask signTask = this.BuildESignTaskFromBody(SignEventType.Signed, body);

        FixedList<SignEvent> signEvents = ESignServices.Sign(signTask);

        return new CollectionModel(this.Request, signEvents.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpPost]
    [Route("v2/e-sign/requests/mine/refused")]
    public CollectionModel RefuseSignRequests([FromBody] object body) {
      try {
        SignTask signTask = this.BuildESignTaskFromBody(SignEventType.Refused, body);

        FixedList<SignEvent> refuseSignEvents = ESignServices.RefuseSign(signTask);

        return new CollectionModel(this.Request, refuseSignEvents.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpPost]
    [Route("v2/e-sign/requests/mine/revoked")]
    public CollectionModel RevokeSignedRequests([FromBody] object body) {
      try {
        SignTask signTask = this.BuildESignTaskFromBody(SignEventType.Revoked, body);

        FixedList<SignEvent> revokeSignEvents = ESignServices.RevokeSign(signTask);

        return new CollectionModel(this.Request, revokeSignEvents.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpPost]
    [Route("v2/e-sign/requests/mine/unrefused")]
    public CollectionModel UnrefuseSignRequests([FromBody] object body) {
      try {
        SignTask signTask = this.BuildESignTaskFromBody(SignEventType.Unrefused, body);

        FixedList<SignEvent> unrefuseSignEvents = ESignServices.UnrefuseSign(signTask);

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

      bodyAsJson.Add("eventType", eventType.ToString());

      SignTask.EnsureIsValid(bodyAsJson);

      return SignTask.Parse(bodyAsJson);
    }

    #endregion Private methods

  }  // class SignRequestsController

}  // namespace Empiria.OnePoint.WebApi
