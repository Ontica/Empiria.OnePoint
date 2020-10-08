/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.EFiling.WebApi.dll        Pattern   : Web Api Controller                      *
*  Type     : PaymentsController                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api provider for e-filing payment related processes.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;
using System.Web.Http;

using Empiria.Json;
using Empiria.WebApi;

namespace Empiria.OnePoint.EFiling.WebApi {

  /// <summary> Web api provider for e-filing payment related processes.</summary>
  [WebApiAuthorizationFilter(WebApiClaimType.ClientApp_Controller, "Electronic.Filing.Client.Application")]
  public class PaymentsController : WebApiController {


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/generate-payment-order")]
    public async Task<SingleObjectModel> GeneratePaymentOrder([FromUri] string filingRequestUID) {
      try {
        EFilingRequestDto filingRequest =
                          await EFilingUseCases.GeneratePaymentOrder(filingRequestUID);

        return new SingleObjectModel(this.Request, filingRequest);

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

        EFilingRequestDto filingRequest =
                      EFilingUseCases.SetPaymentReceipt(filingRequestUID, json.Get<string>("receiptNo"));

        return new SingleObjectModel(this.Request, filingRequest);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


  }  // class PaymentsController

}  // namespace Empiria.OnePoint.EFiling.WebApi
