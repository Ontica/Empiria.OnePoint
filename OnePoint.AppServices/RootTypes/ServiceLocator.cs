/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : OnePoint Application Services           *
*  Assembly : Empiria.OnePoint.AppServices.dll             Pattern : Service Locator                         *
*  Type     : ServiceLocator                               License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Internal static class to get external services.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Reflection;

namespace Empiria.OnePoint.AppServices {

  /// <summary>Internal static class to get external services.</summary>
  static internal class ServiceLocator {

    // ToDo: Temporarily gets the filing type as a Empiria Land transaction.
    // Please remove this call when transactions refactoring is completed.
    static internal Type GetFilingType() {
      return ObjectFactory.GetType("Empiria.Land.Registration",
                                   "Empiria.Land.Registration.Transactions.LRSTransaction");
    }


    // ToDo: Remove hardcoded type's configuration.
    static internal ITreasuryConnector GetTreasuryConnector() {
      Type type = ObjectFactory.GetType("Empiria.Land.Connectors",
                                        "Empiria.Land.Connectors.TreasuryConnector");

      return (ITreasuryConnector) ObjectFactory.CreateObject(type);
    }


  }  // class ServiceLocator

}  // namespace Empiria.OnePoint.AppServices
