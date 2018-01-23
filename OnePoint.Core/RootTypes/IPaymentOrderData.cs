/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : Core Domain                             *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Dependency Inversion Interface          *
*  Type     : IPaymentOrderData                            License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds payment order data which could be payed or not.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.OnePoint {

  /// <summary>Describes a payment order which could be payed or not.</summary>
  public interface IPaymentOrderData {

    string RouteNumber {
      get;
    }

    DateTime IssueTime {
      get;
    }

    DateTime DueDate {
      get;
    }

    string ControlTag {
      get;
    }

    bool IsCompleted {
      get;
    }

    DateTime PaymentDate {
      get;
    }

    string PaymentReference {
      get;
    }

    decimal PaymentTotal {
      get;
    }

    void SetPaymentData(DateTime paymentDate, decimal paymentTotal,
                        string paymentReference);

    JsonObject ToJson();

  }  // interface IPaymentOrderData

}  // namespace Empiria.OnePoint
