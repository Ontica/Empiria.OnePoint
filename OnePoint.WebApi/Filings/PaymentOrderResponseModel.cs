/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : OnePoint Web API                        *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Web Api Controller                      *
*  Type     : PaymentOrderResponseModel                    License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Response models for PaymentOrder objects.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.WebApi {

  /// <summary>Response models for PaymentOrder objects.</summary>
  static internal class PaymentOrderResponseModel {

    static internal object ToResponse(this IPaymentOrderData paymentOrderData) {
      return new {
        controlTag = paymentOrderData.ControlTag
      };
    }

  }  // class PaymentOrderResponseModel

}  //namespace Empiria.OnePoint.WebApi
