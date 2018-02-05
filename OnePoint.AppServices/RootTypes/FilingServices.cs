/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : OnePoint Application Services           *
*  Assembly : Empiria.OnePoint.AppServices.dll             Pattern : Application Service                     *
*  Type     : FilingServices                               License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Filing use cases.                                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Reflection;

namespace Empiria.OnePoint.AppServices {

  /// <summary>Filing use cases.</summary>
  static public class FilingServices {

    #region Services

    static public IFiling GetFiling(string filingUID) {
      Assertion.AssertObject(filingUID, "filingUID");

      Type type = ServiceLocator.GetFilingType();

      IFiling filing = ObjectFactory.InvokeTryParseMethod<IFiling>(type, filingUID);

      if (filing == null) {
        throw new ResourceNotFoundException("Filing.NotFound",
                                            $"No tenemos registrado ningún trámite con número '{filingUID}'.");
      }

      return filing;
    }

    #endregion Services

  }  // class FilingServices

}  // namespace Empiria.OnePoint.AppServices
