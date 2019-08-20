/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Payment Services                Component : Use cases                               *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Use cases class                         *
*  Type     : EPaymentUseCases                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic payment services.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Threading.Tasks;

namespace Empiria.OnePoint.EPayments {

  /// <summary>Use cases that implements electronic payment services.</summary>
  static public class EPaymentUseCases {

    #region Use cases

    static public async Task<PaymentOrderDTO> RefreshPaymentOrder(IPayable payableEntity) {
      PaymentOrderDTO paymentOrderData = payableEntity.TryGetPaymentOrderData();

      Assertion.AssertObject(paymentOrderData,
                             $"Payable entity {payableEntity.UID} doesn't have a registered payment order.");

      if (paymentOrderData.IsCompleted) {
        return paymentOrderData;
      }

      IPaymentOrderProvider provider = OnePointExternalProviders.GetPaymentOrderProvider();

      paymentOrderData = await provider.RefreshPaymentOrder(paymentOrderData)
                                       .ConfigureAwait(false);

      if (paymentOrderData.IsCompleted) {
        payableEntity.SetPaymentOrderData(paymentOrderData);
      }

      return paymentOrderData;
    }


    static public async Task<PaymentOrderDTO> RequestPaymentOrderData(IPayable payableEntity) {
      PaymentOrderDTO paymentOrderData = payableEntity.TryGetPaymentOrderData();

      if (paymentOrderData != null) {
        return paymentOrderData;
      }

      IPaymentOrderProvider provider = OnePointExternalProviders.GetPaymentOrderProvider();

      paymentOrderData = await provider.GeneratePaymentOrder(payableEntity)
                                       .ConfigureAwait(false);

      payableEntity.SetPaymentOrderData(paymentOrderData);

      return paymentOrderData;
    }

    #endregion Use cases

  }  // class EPaymentUseCases

}  // namespace Empiria.OnePoint.EPayments
