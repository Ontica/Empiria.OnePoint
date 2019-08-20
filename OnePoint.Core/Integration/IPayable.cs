/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Payment Services                Component : External Services Providers             *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Dependency Inversion Interface          *
*  Type     : IPayable                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Allows set payment order data into external payable entities,                                  *
*             like service or purchase orders.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.EPayments {

  /// <summary>Allows set payment order data into external payable entities,
  /// like service or purchase orders.</summary>
  public interface IPayable {

    string UID {
      get;
    }

    void SetPaymentOrderData(PaymentOrderDTO paymentOrderData);

    PaymentOrderDTO TryGetPaymentOrderData();


  }  // interface IPayable

}  // namespace Empiria.OnePoint.EPayments
