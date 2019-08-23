/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.ESign.WebApi.dll          Pattern   : Web Api Controller                      *
*  Type     : SignRequestsController                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web Api used to provide electronic sign services through sign tasks.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

namespace Empiria.OnePoint.ESign.WebApi {

  /// <summary>Web Api used to provide electronic sign services through sign tasks.</summary>
  public class SignRequestsController : WebApiController {

    #region Query methods

    [AllowAnonymous]
    [HttpGet]
    [Route("v2/e-sign/requests/by-document-no/{documentNo}")]
    public SingleObjectModel GetESignRequestByDocumentNo(string documentNo) {
      try {
        SignRequestDTO signRequest = ESignUseCases.GetESignRequestByDocumentNo(documentNo);

        return GetResponse(signRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet]
    [Route("v2/e-sign/requests/mine")]
    public CollectionModel GetMyESignRequests([FromUri] string keywords = "") {
      try {
        FixedList<SignRequestDTO> myRequests =
                                  ESignUseCases.GetMyESignRequests(keywords);

        return GetResponse(myRequests);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet]
    [Route("v2/e-sign/requests/mine/{status}")]
    public CollectionModel GetMyESignRequestsInStatus([FromUri] string status,
                                                      [FromUri] string keywords = "") {
      try {
        SignStatus signStatus = GetSignStatusFromRequest(status);

        FixedList<SignRequestDTO> myRequests =
                                  ESignUseCases.GetMyESignRequests(signStatus, keywords);

        return GetResponse(myRequests);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    #endregion Query methods


    #region Command methods

    [HttpPost]
    [Route("v2/e-sign/requests/mine/signed")]
    public CollectionModel Sign([FromBody] SignTaskDTO signTaskDTO) {
      try {
        AssertSignTaskDTOIsValid(signTaskDTO);

        FixedList<SignEventDTO> signEvents = ESignUseCases.Sign(signTaskDTO);

        return GetResponse(signEvents);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpPost]
    [Route("v2/e-sign/requests/mine/refused")]
    public CollectionModel RefuseSign([FromBody] SignTaskDTO signTaskDTO) {
      try {
        AssertSignTaskDTOIsValid(signTaskDTO);

        FixedList<SignEventDTO> refuseSignEvents = ESignUseCases.RefuseSign(signTaskDTO);

        return GetResponse(refuseSignEvents);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpPost]
    [Route("v2/e-sign/requests/mine/revoked")]
    public CollectionModel RevokeSign([FromBody] SignTaskDTO signTaskDTO) {
      try {
        AssertSignTaskDTOIsValid(signTaskDTO);

        FixedList<SignEventDTO> revokeSignEvents = ESignUseCases.RevokeSign(signTaskDTO);

        return GetResponse(revokeSignEvents);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpPost]
    [Route("v2/e-sign/requests/mine/unrefused")]
    public CollectionModel UnrefuseSign([FromBody] SignTaskDTO signTaskDTO) {
      try {
        AssertSignTaskDTOIsValid(signTaskDTO);

        FixedList<SignEventDTO> unrefuseSignEvents = ESignUseCases.UnrefuseSign(signTaskDTO);

        return GetResponse(unrefuseSignEvents);

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    #endregion Command methods


    #region Utility methods

    private void AssertSignTaskDTOIsValid(SignTaskDTO signTaskDTO) {
      base.RequireBody(signTaskDTO);

      ESignUseCases.EnsureValidSignTaskDTO(signTaskDTO);
    }


    private SingleObjectModel GetResponse(SignRequestDTO signRequest) {
      return new SingleObjectModel(this.Request, signRequest, typeof(SignRequestDTO).FullName);
    }


    private CollectionModel GetResponse(FixedList<SignRequestDTO> signRequests) {
      return new CollectionModel(this.Request, signRequests, typeof(SignRequestDTO).FullName);
    }


    private CollectionModel GetResponse(FixedList<SignEventDTO> signEvents) {
      return new CollectionModel(this.Request, signEvents, typeof(SignEventDTO).FullName);
    }


    private SignStatus GetSignStatusFromRequest(string status) {
      Assertion.AssertObject(status, "status");

      switch (status.ToLowerInvariant()) {
        case "pending":
          return SignStatus.Pending;
        case "refused":
          return SignStatus.Refused;
        case "revoked":
          return SignStatus.Revoked;
        case "signed":
          return SignStatus.Signed;
        default:
          throw new WebApiException(WebApiException.Msg.BadRequest,
                                    $"Request has an unrecognized sign status '{status}'.");
      }
    }


    #endregion Utility methods

  }  // class SignRequestsController

}  // namespace Empiria.OnePoint.WebApi
