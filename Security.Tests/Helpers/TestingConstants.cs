/* Empiria OnePoint  *****************************************************************************************
*                                                                                                            *
*  Module   : One Point Security Services                  Component : Test cases                            *
*  Assembly : Empiria.OnePoint.Security.Tests.dll          Pattern   : Testing constants                     *
*  Type     : TestingConstants                             License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides testing constants.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.OnePoint.Security.Tests {

  /// <summary>Provides testing constants.</summary>
  static public class TestingConstants {

    static internal string SUBJECT_UID => ConfigurationData.GetString("SUBJECT_UID");

  }  // class TestingConstants

}  // namespace Empiria.OnePoint.Security.Tests
