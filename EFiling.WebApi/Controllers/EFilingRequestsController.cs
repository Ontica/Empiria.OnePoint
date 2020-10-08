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

    #region Query services

    [HttpGet]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}")]
    public SingleObjectModel GetEFilingRequest([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDto filingRequest = EFilingUseCases.GetEFilingRequest(filingRequestUID);

        return new SingleObjectModel(this.Request, filingRequest);

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

        return new PagedCollectionModel(this.Request, list);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }

    #endregion Query services

    #region Command services

    [HttpPost]
    [Route("v2/electronic-filing/filing-requests")]
    public SingleObjectModel CreateEFilingRequest([FromBody] CreateEFilingRequestDTO createRequestDTO) {
      try {
        EFilingRequestDto filingRequest = EFilingUseCases.CreateEFilingRequest(createRequestDTO);

        return new SingleObjectModel(this.Request, filingRequest);

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
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/submit")]
    public async Task<SingleObjectModel> SubmitEFilingRequest([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDto filingRequest = await EFilingUseCases.SubmitEFilingRequest(filingRequestUID);

        return new SingleObjectModel(this.Request, filingRequest);

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

        return new SingleObjectModel(this.Request, filingRequest);

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

        return new SingleObjectModel(this.Request, filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    #endregion Command services

  }  // class EFilingRequestsController

}  // namespace Empiria.OnePoint.EFiling.WebApi
