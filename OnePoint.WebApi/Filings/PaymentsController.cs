/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : OnePoint Web API                        *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Web Api Controller                      *
*  Type     : PaymentsController                           License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Services to manage filing's payments.                                                          *
*                                                                                                            *
********************************* Copyright (c) 2009-2018. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Threading.Tasks;
using System.Web.Http;

using Empiria.WebApi;
using Empiria.WebApi.Models;

using Empiria.OnePoint.AppServices;

namespace Empiria.OnePoint.WebApi {

  /// <summary>Services to manage filing's payments.</summary>
  public class PaymentsController : WebApiController {

    #region POST methods

    [HttpPost]
    [Route("v2/filings/{filingUID}/payment-order")]
    public async Task<SingleObjectModel> GetPaymentOrder([FromUri] string filingUID) {
      try {
        IFiling filing = FilingServices.GetFiling(filingUID);

        IPaymentOrderData paymentOrderData = await PaymentServices.RequestPaymentOrderData(filing);

        return new SingleObjectModel(this.Request, paymentOrderData.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    #endregion POST methods

  }  // class PaymentsController

}  // namespace Empiria.OnePoint.WebApi
