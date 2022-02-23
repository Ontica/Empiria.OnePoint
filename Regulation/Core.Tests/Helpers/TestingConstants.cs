/* Empiria OnePoint Regulation *******************************************************************************
*                                                                                                            *
*  Module   : Regulation Core Module Tests               Component : Test Helpers                            *
*  Assembly : Empiria.OnePoint.Regulation.Tests.dll      Pattern   : Testing constants                       *
*  Type     : TestingConstants                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides testing constants.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.Regulation.Tests {

  /// <summary>Provides testing constants.</summary>
  static public class TestingConstants {

    static readonly internal string SESSION_TOKEN = ConfigurationData.GetString("SESSION_TOKEN");

  }  // class TestingConstants

}  // namespace Empiria.OnePoint.Regulation.Tests
