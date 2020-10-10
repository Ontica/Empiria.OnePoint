/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.EFiling.WebApi.dll        Pattern   : Web Api Controller                      *
*  Type     : EFilingRequestsController                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api controller that provides filing request services.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Threading.Tasks;
using System.Web.Http;

using Empiria.Json;
using Empiria.WebApi;

using Empiria.OnePoint.EFiling.UseCases;

namespace Empiria.OnePoint.EFiling.WebApi {

  /// <summary>Public API to read and write electronic application filing requests,
  /// typically submitted to government agencies.</summary>
  [WebApiAuthorizationFilter(WebApiClaimType.ClientApp_Controller, "Electronic.Filing.Client.Application")]
  public class EFilingRequestsController : WebApiController {

    #region Query services

    [HttpGet]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}")]
    public SingleObjectModel GetEFilingRequest([FromUri] string filingRequestUID) {

      using (var usecases = new EFilingRequestUseCases()) {
        EFilingRequestDto filingRequestDto = usecases.GetEFilingRequest(filingRequestUID);

        return new SingleObjectModel(this.Request, filingRequestDto);
      }
    }


    [HttpGet]
    [Route("v2/electronic-filing/filing-requests")]
    public PagedCollectionModel GetEFilingRequestList([FromUri] RequestStatus status = RequestStatus.Pending,
                                                      [FromUri] string keywords = "") {
      const int PAGE_SIZE = 50;

      using (var usecases = new EFilingRequestUseCases()) {
        FixedList<EFilingRequestDto> list = usecases.GetEFilingRequestListByStatus(status, keywords, PAGE_SIZE);

        return new PagedCollectionModel(this.Request, list);
      }
    }


    #endregion Query services


    #region Command services

    [HttpPost]
    [Route("v2/electronic-filing/filing-requests")]
    public SingleObjectModel CreateEFilingRequest([FromBody] CreateEFilingRequestDto createRequestDTO) {

      using (var usecases = new EFilingRequestUseCases()) {
        EFilingRequestDto filingRequestDto = usecases.CreateEFilingRequest(createRequestDTO);

        return new SingleObjectModel(this.Request, filingRequestDto);
      }
    }


    [HttpDelete]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}")]
    public NoDataModel DeleteEFilingRequest([FromUri] string filingRequestUID) {

      using (var usecases = new EFilingRequestUseCases()) {
        usecases.DeleteEFilingRequest(filingRequestUID);

        return new NoDataModel(this.Request);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/submit")]
    public async Task<SingleObjectModel> SubmitEFilingRequest([FromUri] string filingRequestUID) {

      using (var usecases = new EFilingRequestUseCases()) {
        EFilingRequestDto filingRequestDto = await usecases.SubmitEFilingRequest(filingRequestUID);

        return new SingleObjectModel(this.Request, filingRequestDto);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/update-application-form")]
    public SingleObjectModel UpdateApplicationForm([FromUri] string filingRequestUID,
                                                   [FromBody] object applicationForm) {

      using (var usecases = new EFilingRequestUseCases()) {
        var json = JsonObject.Parse(applicationForm);

        EFilingRequestDto filingRequestDto = usecases.UpdateApplicationForm(filingRequestUID, json);

        return new SingleObjectModel(this.Request, filingRequestDto);
      }
    }


    [HttpPut, HttpPatch]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}")]
    public SingleObjectModel UpdateEFilingRequest([FromUri] string filingRequestUID,
                                                  [FromBody] RequesterDto requestedBy) {

      using (var usecases = new EFilingRequestUseCases()) {
        EFilingRequestDto filingRequestDto = usecases.UpdateEFilingRequest(filingRequestUID, requestedBy);

        return new SingleObjectModel(this.Request, filingRequestDto);
      }
    }


    #endregion Command services


    #region Synchronization services


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/synchronize")]
    public async Task<NoDataModel> SynchronizeAllExternalData() {

      using (var usecases = new EFilingRequestUseCases()) {
        await usecases.SynchronizeAllExternalData();

        return new NoDataModel(this.Request);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/synchronize/{filingRequestUID:guid}")]
    public async Task<SingleObjectModel> SynchronizeRequestExternalData([FromUri] string filingRequestUID) {

      using (var usecases = new EFilingRequestUseCases()) {
        EFilingRequestDto filingRequestDto = await usecases.SynchronizeExternalData(filingRequestUID);

        return new SingleObjectModel(this.Request, filingRequestDto);
      }
    }


    #endregion Synchronization services

  }  // class EFilingRequestsController

}  // namespace Empiria.OnePoint.EFiling.WebApi
