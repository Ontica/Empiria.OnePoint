/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Payment Services                Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EPayments.dll             Pattern   : Use Cases class                         *
*  Type     : EPaymentsUseCases                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic payment services.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

namespace Empiria.OnePoint.EPayments {

  /// <summary>Use cases that implements electronic payment services.</summary>
  static public class EPaymentsUseCases {

    #region Use cases

    static public async Task<PaymentOrderDTO> RefreshPaymentOrder(IPayable payable) {
      PaymentOrderDTO paymentOrderData = payable.TryGetPaymentOrderData();

      Assertion.AssertObject(paymentOrderData,
                             $"Transaction {payable.UID} doesn't have a registered payment order.");

      if (paymentOrderData.IsCompleted) {
        return paymentOrderData;
      }

      IPaymentOrderProvider provider = ExternalProviders.GetPaymentOrderProvider();

      paymentOrderData = await provider.RefreshPaymentOrder(paymentOrderData)
                                       .ConfigureAwait(false);

      if (paymentOrderData.IsCompleted) {
        payable.SetPaymentOrderData(paymentOrderData);
      }

      return paymentOrderData;
    }


    static public async Task<PaymentOrderDTO> RequestPaymentOrderData(IPayable payable) {
      PaymentOrderDTO paymentOrderData = payable.TryGetPaymentOrderData();

      if (paymentOrderData != null) {
        return paymentOrderData;
      }

      IPaymentOrderProvider provider = ExternalProviders.GetPaymentOrderProvider();

      paymentOrderData = await provider.GeneratePaymentOrder(payable)
                                       .ConfigureAwait(false);

      payable.SetPaymentOrderData(paymentOrderData);

      return paymentOrderData;
    }


    #endregion Use cases

  }  // class EPaymentsUseCases

}  // namespace Empiria.OnePoint.EPayments
