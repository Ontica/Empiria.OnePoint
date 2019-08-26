/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Plugin factory                          *
*  Type     : ExternalProviders                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Plugin factory methods that provide access to external services.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Reflection;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Plugin factory methods that provide access to external services.</summary>
  static internal class ExternalProviders {


    static public IFilingTransactionProvider GetFilingTransactionProvider(IProcedure procedure) {
      Type type = ObjectFactory.GetType("Empiria.Land.Registration",
                                        "Empiria.Land.Integration.LandFilingTransactionProvider");

      return (IFilingTransactionProvider) ObjectFactory.CreateObject(type);
    }


  }  // class ExternalProviders

}  // namespace Empiria.OnePoint.EFiling
