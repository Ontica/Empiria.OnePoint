/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                     Component : Signer                                *
*  Assembly : Empiria.OnePoint.ESign.dll                   Pattern   : Service provider                      *
*  Type     : SignerService                                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides services for messages electronic signing.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Reflection;

using Empiria.OnePoint.ESign.Providers;

namespace Empiria.OnePoint.ESign.Services {

  /// <summary>Provides services for messages electronic signing.</summary>
  public class SignerService {

    public string Sign(string message) {
      Assertion.Require(message, nameof(message));

      IESignClient client = GetESignClient();

      return client.Sign(message);
    }


    private IESignClient GetESignClient() {
      Type type = ObjectFactory.GetType("Empiria.OnePoint.ESign.Providers.SeguriSign",
                                        "Empiria.OnePoint.ESign.Providers.SeguriSignClient");

      return (IESignClient) ObjectFactory.CreateObject(type);
    }

  }  // class SignerService

}  // namespace Empiria.OnePoint.ESign.Services
