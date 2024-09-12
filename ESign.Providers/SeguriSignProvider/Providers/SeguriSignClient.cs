/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Electronic sign providers               *
*  Assembly : OnePoint.ESign.Providers.SeguriSign.dll    Pattern   : Service adapter                         *
*  Type     : SeguriSignClient                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides electronic sign services using the SeguriSign provider.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign.Providers {

  /// <summary>Provides electronic sign services using the SeguriSign provider.</summary>
  internal class SeguriSignClient : IESignClient {

    public string Sign(string message) {
      return Empiria.Security.Cryptographer.GetSHA256(message);
    }

  }  // class SeguriSignClient

} // namespace Empiria.OnePoint.ESign.Providers
