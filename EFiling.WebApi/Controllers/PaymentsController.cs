/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.EFiling.WebApi.dll        Pattern   : Web Api Controller                      *
*  Type     : PaymentsController                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api provider for e-filing payment related processes.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Threading.Tasks;
using System.Web.Http;

using Empiria.Json;
using Empiria.WebApi;

using Empiria.OnePoint.EFiling.UseCases;

namespace Empiria.OnePoint.EFiling.WebApi {

  /// <summary> Web api provider for e-filing payment related processes.</summary>
  [WebApiAuthorizationFilter(WebApiClaimType.ClientApp_Controller, "Electronic.Filing.Client.Application")]
  public class PaymentsController : WebApiController {


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/generate-payment-order")]
    public async Task<SingleObjectModel> GeneratePaymentOrder([FromUri] string filingRequestUID) {

      using (var usecases = new PaymentUseCases()) {
        EFilingRequestDto filingRequestDto = await usecases.GeneratePaymentOrder(filingRequestUID);

        return new SingleObjectModel(this.Request, filingRequestDto);
      }
    }


    [HttpPost]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/set-payment-receipt")]
    public SingleObjectModel SetPaymentReceipt([FromUri] string filingRequestUID,
                                               [FromBody] object paymentData) {

      using (var usecases = new PaymentUseCases()) {
        var json = JsonObject.Parse(paymentData);

        EFilingRequestDto filingRequestDto = usecases.SetPaymentReceipt(filingRequestUID, json.Get<string>("receiptNo"));

        return new SingleObjectModel(this.Request, filingRequestDto);
      }
    }


  }  // class PaymentsController

}  // namespace Empiria.OnePoint.EFiling.WebApi
