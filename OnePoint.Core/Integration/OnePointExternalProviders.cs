/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Empiria OnePoint                           Component : External Services Providers             *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Plugin factory                          *
*  Type     : OnePointExternalProviders                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Plugin factory methods that provide access to OnePoint external services.                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Reflection;

using Empiria.OnePoint.EFiling;
using Empiria.OnePoint.EPayments;

namespace Empiria.OnePoint {

  /// <summary>Plugin factory methods that provide access to OnePoint external services.</summary>
  static internal class OnePointExternalProviders {


    static internal IFilingTransactionProvider GetFilingTransactionProvider(Procedure procedure) {
      Type type = ObjectFactory.GetType("Empiria.Land.Registration",
                                        "Empiria.Land.Integration.LandFilingTransactionProvider");

      return (IFilingTransactionProvider) ObjectFactory.CreateObject(type);
    }


    static internal IPaymentOrderProvider GetPaymentOrderProvider() {
      Type type = ObjectFactory.GetType("Empiria.Land.Connectors",
                                        "Empiria.Land.Integration.TlaxcalaGov.PaymentOrderConnector");

      return (IPaymentOrderProvider) ObjectFactory.CreateObject(type);
    }


  }  // class OnePointExternalProviders

}  // namespace Empiria.OnePoint
