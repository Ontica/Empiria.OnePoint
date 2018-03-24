/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : OnePoint Application Services           *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Application Service                     *
*  Type     : PaymentServices                              License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Payment-related use cases.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

namespace Empiria.OnePoint.AppServices {

  /// <summary>Payment-related use cases.</summary>
  static public class PaymentServices {

    #region Application services

    static public async Task<IPaymentOrderData> RefreshPaymentOrder(IFiling filing) {
      IPaymentOrderData paymentOrderData = filing.TryGetPaymentOrderData();

      Assertion.AssertObject(paymentOrderData,
                             $"Transaction {filing.UID} doesn't have a registered payment order.");

      if (paymentOrderData.IsCompleted) {
        return paymentOrderData;
      }

      ITreasuryConnector connector = ServiceLocator.GetTreasuryConnector();

      paymentOrderData = await connector.RefreshPaymentOrder(paymentOrderData)
                                        .ConfigureAwait(false);

      if (paymentOrderData.IsCompleted) {
        filing.SetPaymentOrderData(paymentOrderData);
      }

      return paymentOrderData;
    }


    static public async Task<IPaymentOrderData> RequestPaymentOrderData(IFiling filing) {
      IPaymentOrderData paymentOrderData = filing.TryGetPaymentOrderData();

      if (paymentOrderData != null && paymentOrderData.RouteNumber != "") {
        return paymentOrderData;
      }

      ITreasuryConnector connector = ServiceLocator.GetTreasuryConnector();

      paymentOrderData = await connector.GeneratePaymentOrder(filing)
                                        .ConfigureAwait(false);

      filing.SetPaymentOrderData(paymentOrderData);

      return paymentOrderData;
    }

    #endregion Application services

  }  // class PaymentServices

}  // namespace Empiria.OnePoint.AppServices
