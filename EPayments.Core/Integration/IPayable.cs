/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Payment Services                Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.EPayments.dll             Pattern   : Adapter Interface                       *
*  Type     : IPayable                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Allows set payment order data into external payable entities, like electronic filing           *
*             services or purchase orders.                                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.EPayments {

  /// <summary>Allows set payment order data into external payable entities, like electronic filing
  /// services or purchase orders.</summary>
  public interface IPayable {

    string UID {
      get;
    }

    void SetPaymentOrderData(PaymentOrderDTO paymentOrderData);

    PaymentOrderDTO TryGetPaymentOrderData();


  }  // interface IPayable

}  // namespace Empiria.OnePoint.EPayments
