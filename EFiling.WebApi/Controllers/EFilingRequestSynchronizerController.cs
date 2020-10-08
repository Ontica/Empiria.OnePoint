/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.EFiling.WebApi.dll        Pattern   : Web Api Controller                      *
*  Type     : EFilingRequestSynchronizerController       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web services used to synchronize the external data of filing requests.                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;
using System.Web.Http;

using Empiria.WebApi;

namespace Empiria.OnePoint.EFiling.WebApi {

  /// <summary>Web services used to synchronize the external data of filing requests.</summary>
  [WebApiAuthorizationFilter(WebApiClaimType.ClientApp_Controller, "Electronic.Filing.Client.Application")]
  public class EFilingRequestSynchronizerController : WebApiController {

    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/synchronize")]
    public async Task<NoDataModel> SynchronizeAllExternalData() {
      try {
        await EFilingUseCases.SynchronizeExternalData();

        return new NoDataModel(this.Request);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/synchronize/{filingRequestUID:guid}")]
    public async Task<SingleObjectModel> SynchronizeRequestExternalData([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDto filingRequest = await EFilingUseCases.SynchronizeExternalData(filingRequestUID);

        return new SingleObjectModel(this.Request, filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


  }  // class EFilingRequestSynchronizerController

}  // namespace Empiria.OnePoint.EFiling.WebApi
