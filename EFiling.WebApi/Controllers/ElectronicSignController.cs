/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.EFiling.WebApi.dll        Pattern   : Web Api Controller                      *
*  Type     : ElectronicSignController                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web services that provides electronic sign services.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

namespace Empiria.OnePoint.EFiling.WebApi {

  /// <summary>Web services that provides electronic sign services.</summary>
  [WebApiAuthorizationFilter(WebApiClaimType.ClientApp_Controller, "Electronic.Filing.Client.Application")]
  public class ElectronicSignController : WebApiController {


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/revoke-sign")]
    public SingleObjectModel RevokeEFilingRequestSign([FromUri] string filingRequestUID,
                                                  [FromBody] object body) {
      try {
        var revokeSignData = base.GetJsonFromBody(body);

        EFilingRequestDto filingRequest = EFilingUseCases.RevokeEFilingRequestSign(filingRequestUID, revokeSignData);

        return new SingleObjectModel(this.Request, filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/send-to-sign")]
    public SingleObjectModel SendToSign([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDto filingRequest = EFilingUseCases.SendEFilingRequestToSign(filingRequestUID);

        return new SingleObjectModel(this.Request, filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/sign")]
    public SingleObjectModel SignEFilingRequest([FromUri] string filingRequestUID,
                                                [FromBody] object body) {
      try {
        var signData = base.GetJsonFromBody(body);

        EFilingRequestDto filingRequest = EFilingUseCases.SignEFilingRequest(filingRequestUID, signData);

        return new SingleObjectModel(this.Request, filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


  }  // class ElectronicSignController

}  // namespace Empiria.OnePoint.EFiling.WebApi
