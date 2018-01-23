/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : Core Domain                             *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Dependency Inversion Interface          *
*  Type     : IFiling                                      License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Describes a document or procedure filing.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint {

  /// <summary>Describes a document or procedure filing.</summary>
  public interface IFiling {

    string UID {
      get;
    }

    void SetPaymentOrderData(IPaymentOrderData paymentOrderData);

    IPaymentOrderData TryGetPaymentOrderData();

  }  // interface IFiling

}  // namespace Empiria.OnePoint
