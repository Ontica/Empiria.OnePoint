/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : Core Domain                             *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Dependency Inversion Interface          *
*  Type     : ITreasuryConnector                           License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Interface used to connect Empiria OnePoint with payment and treasury systems.                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

namespace Empiria.OnePoint {

  /// <summary>Interface used to connect Empiria OnePoint with payment and treasury systems.</summary>
  public interface ITreasuryConnector {

    Task<IPaymentOrderData> GeneratePaymentOrder(IFiling filing);

    Task<IPaymentOrderData> RefreshPaymentOrder(IPaymentOrderData paymentOrder);

  }  // interface ITreasuryConnector

}  // namespace Empiria.OnePoint
