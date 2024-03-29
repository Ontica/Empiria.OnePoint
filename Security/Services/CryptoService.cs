﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security                                     Component : Authorization services                *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Service provider                      *
*  Type     : CryptoService                                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides subject's authorization services.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security.Providers;

using System.Security;
using System.Security.Cryptography;

using Empiria.OnePoint.Security.Providers;

namespace Empiria.OnePoint.Security.Services {

  /// <summary>Provides subject's authorization services.</summary>
  public class CryptoService : ICryptoServiceProvider {

    public RSACryptoServiceProvider GetRSAProvider(string privateKeyFilePath, SecureString password) {
      Assertion.Require(privateKeyFilePath, nameof(privateKeyFilePath));
      Assertion.Require(password, nameof(password));

      return RSAProvider.GetProvider(privateKeyFilePath, password);
    }


    public RSACryptoServiceProvider GetRSASystemProvider() {
      return RSAProvider.GetSystemProvider();
    }

  }  // class CryptoService

}  // namespace Empiria.OnePoint.Security.Services
