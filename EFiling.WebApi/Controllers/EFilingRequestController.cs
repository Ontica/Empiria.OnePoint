/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.EFiling.WebApi.dll        Pattern   : Web Api Controller                      *
*  Type     : EFilingRequestsController                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api controller that provides filing request services.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.Json;
using Empiria.WebApi;

namespace Empiria.OnePoint.EFiling.WebApi {

  /// <summary>Public API to read and write electronic application filing requests,
  /// typically submitted to government agencies.</summary>
  [WebApiAuthorizationFilter(WebApiClaimType.ClientApp_Controller, "Electronic.Filing.Client.Application")]
  public class EFilingRequestsController : WebApiController {

    #region Query API


    [HttpGet]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID}")]
    public SingleObjectModel GetEFilingRequest([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDTO filingRequest = EFilingUseCases.GetEFilingRequest(filingRequestUID);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpGet]
    [Route("v2/electronic-filing/filing-requests")]
    public PagedCollectionModel GetEFilingRequestList([FromUri] EFilingRequestStatus status = EFilingRequestStatus.Pending,
                                                      [FromUri] string keywords = "") {
      try {
        FixedList<EFilingRequestDTO> list = EFilingUseCases.GetEFilingRequestListByStatus(status, keywords);

        return GeneratePagedResponse(list);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    #endregion Query API


    #region Command API

    [HttpPost]
    [Route("v2/electronic-filing/filing-requests")]
    public SingleObjectModel CreateEFilingRequest([FromBody] CreateEFilingRequestDTO createRequestDTO) {
      try {
        EFilingRequestDTO filingRequest = EFilingUseCases.CreateEFilingRequest(createRequestDTO);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID}/generate-payment-order")]
    public SingleObjectModel GeneratePaymentOrderForEFilingRequest([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDTO filingRequest =
                          EFilingUseCases.GeneratePaymentOrderForEFilingRequest(filingRequestUID);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID}/sign")]
    public SingleObjectModel SignEFilingRequest([FromUri] string filingRequestUID,
                                                [FromBody] object body) {
      try {
        var signData = GetBodyAsJson(body);

        EFilingRequestDTO filingRequest = EFilingUseCases.SignEFilingRequest(filingRequestUID, signData);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID}/revoke-sign")]
    public SingleObjectModel RevokeEFilingRequestSign([FromUri] string filingRequestUID,
                                                      [FromBody] object body) {
      try {
        var revokeSignData = GetBodyAsJson(body);

        EFilingRequestDTO filingRequest = EFilingUseCases.RevokeEFilingRequestSign(filingRequestUID, revokeSignData);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID}/send-to-sign")]
    public SingleObjectModel SendToSign([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDTO filingRequest = EFilingUseCases.SendEFilingRequestToSign(filingRequestUID);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID}/set-payment-receipt")]
    public SingleObjectModel SetPaymentReceipt([FromUri] string filingRequestUID,
                                               [FromBody] object paymentData) {
      try {
        var json = JsonObject.Parse(paymentData);

        EFilingRequestDTO filingRequest = EFilingUseCases.SetPaymentReceipt(filingRequestUID, json.Get<string>("receiptNo"));

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID}/submit")]
    public SingleObjectModel SubmitEFilingRequest([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDTO filingRequest = EFilingUseCases.SubmitEFilingRequest(filingRequestUID);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID}/update-application-form")]
    public SingleObjectModel UpdateApplicationForm([FromUri] string filingRequestUID,
                                                   [FromBody] object applicationForm) {
      try {
        var json = JsonObject.Parse(applicationForm);

        EFilingRequestDTO filingRequest = EFilingUseCases.UpdateApplicationForm(filingRequestUID, json);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID}")]
    public SingleObjectModel UpdateEFilingRequest([FromUri] string filingRequestUID,
                                                  [FromBody] Requester requestedBy) {
      try {
        EFilingRequestDTO filingRequest = EFilingUseCases.UpdateEFilingRequest(filingRequestUID, requestedBy);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Command API


    #region Utility methods


    private PagedCollectionModel GeneratePagedResponse(FixedList<EFilingRequestDTO> list) {
      return new PagedCollectionModel(this.Request, list, typeof(EFilingRequestDTO).FullName);
    }


    private SingleObjectModel GenerateResponse(EFilingRequestDTO filingRequest) {
      return new SingleObjectModel(this.Request, filingRequest, typeof(EFilingRequestDTO).FullName);
    }


    private JsonObject GetBodyAsJson(object body) {
      base.RequireBody(body);

      return JsonObject.Parse(body);
    }


    #endregion Utility methods

  }  // class EFilingRequestsController

}  // namespace Empiria.OnePoint.EFiling.WebApi
