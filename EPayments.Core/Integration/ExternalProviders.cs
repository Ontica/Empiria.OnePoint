/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Payment Services                Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.EPayments.dll             Pattern   : Plugin factory                          *
*  Type     : ExternalProviders                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Plugin factory methods that provide access to external services.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Reflection;

namespace Empiria.OnePoint.EPayments {


  /// <summary>Plugin factory methods that provide access to external services.</summary>
  static internal class ExternalProviders {


    static public IPaymentOrderProvider GetPaymentOrderProvider() {
      Type type = ObjectFactory.GetType("Empiria.Land.Connectors",
                                        "Empiria.Land.Integration.TlaxcalaGov.PaymentOrderConnector");

      return (IPaymentOrderProvider) ObjectFactory.CreateObject(type);
    }


  }  // class ExternalProviders

}  // namespace Empiria.OnePoint.EPayments
