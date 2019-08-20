/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Payment Services                Component : External Services Providers             *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Dependency Inversion Interface          *
*  Type     : IPaymentOrderProvider                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Interface used to connect Empiria OnePoint with payment orders providers.                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

namespace Empiria.OnePoint.EPayments {

  /// <summary>Interface used to connect Empiria OnePoint with payment orders providers.</summary>
  public interface IPaymentOrderProvider {

    Task<PaymentOrderDTO> GeneratePaymentOrder(IPayable payableEntity);

    Task<PaymentOrderDTO> RefreshPaymentOrder(PaymentOrderDTO paymentOrder);

  }  // interface IPaymentOrderProvider

}  // namespace Empiria.OnePoint.EPayments
