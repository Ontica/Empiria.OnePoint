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
using System.Threading.Tasks;
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
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}")]
    public SingleObjectModel GetEFilingRequest([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDto filingRequest = EFilingUseCases.GetEFilingRequest(filingRequestUID);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpGet]
    [Route("v2/electronic-filing/filing-requests")]
    public PagedCollectionModel GetEFilingRequestList([FromUri] RequestStatus status = RequestStatus.Pending,
                                                      [FromUri] string keywords = "") {
      try {
        FixedList<EFilingRequestDto> list = EFilingUseCases.GetEFilingRequestListByStatus(status, keywords, 50);

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
        EFilingRequestDto filingRequest = EFilingUseCases.CreateEFilingRequest(createRequestDTO);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpDelete]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}")]
    public NoDataModel DeleteEFilingRequest([FromUri] string filingRequestUID) {
      try {

        EFilingUseCases.DeleteEFilingRequest(filingRequestUID);

        return new NoDataModel(this.Request);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/generate-payment-order")]
    public async Task<SingleObjectModel> GeneratePaymentOrderForEFilingRequest([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDto filingRequest =
                          await EFilingUseCases.GeneratePaymentOrderForEFilingRequest(filingRequestUID);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/sign")]
    public SingleObjectModel SignEFilingRequest([FromUri] string filingRequestUID,
                                                [FromBody] object body) {
      try {
        var signData = GetBodyAsJson(body);

        EFilingRequestDto filingRequest = EFilingUseCases.SignEFilingRequest(filingRequestUID, signData);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/revoke-sign")]
    public SingleObjectModel RevokeEFilingRequestSign([FromUri] string filingRequestUID,
                                                      [FromBody] object body) {
      try {
        var revokeSignData = GetBodyAsJson(body);

        EFilingRequestDto filingRequest = EFilingUseCases.RevokeEFilingRequestSign(filingRequestUID, revokeSignData);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/send-to-sign")]
    public SingleObjectModel SendToSign([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDto filingRequest = EFilingUseCases.SendEFilingRequestToSign(filingRequestUID);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/set-payment-receipt")]
    public SingleObjectModel SetPaymentReceipt([FromUri] string filingRequestUID,
                                               [FromBody] object paymentData) {
      try {
        var json = JsonObject.Parse(paymentData);

        EFilingRequestDto filingRequest = EFilingUseCases.SetPaymentReceipt(filingRequestUID, json.Get<string>("receiptNo"));

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/submit")]
    public async Task<SingleObjectModel> SubmitEFilingRequest([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDto filingRequest = await EFilingUseCases.SubmitEFilingRequest(filingRequestUID);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/update-application-form")]
    public SingleObjectModel UpdateApplicationForm([FromUri] string filingRequestUID,
                                                   [FromBody] object applicationForm) {
      try {
        var json = JsonObject.Parse(applicationForm);

        EFilingRequestDto filingRequest = EFilingUseCases.UpdateApplicationForm(filingRequestUID, json);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}")]
    public SingleObjectModel UpdateEFilingRequest([FromUri] string filingRequestUID,
                                                  [FromBody] RequesterDto requestedBy) {
      try {

        EFilingRequestDto filingRequest = EFilingUseCases.UpdateEFilingRequest(filingRequestUID, requestedBy);

        return GenerateResponse(filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    #endregion Command API


    #region Utility methods


    private PagedCollectionModel GeneratePagedResponse(FixedList<EFilingRequestDto> list) {
      return new PagedCollectionModel(this.Request, list, typeof(EFilingRequestDto).FullName);
    }


    private SingleObjectModel GenerateResponse(EFilingRequestDto filingRequest) {
      return new SingleObjectModel(this.Request, filingRequest, typeof(EFilingRequestDto).FullName);
    }


    private JsonObject GetBodyAsJson(object body) {
      base.RequireBody(body);

      return JsonObject.Parse(body);
    }


    #endregion Utility methods

  }  // class EFilingRequestsController

}  // namespace Empiria.OnePoint.EFiling.WebApi
