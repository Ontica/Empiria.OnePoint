﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management               Component : Data Access Layer                       *
*  Assembly : Empiria.OnePoint.Security.dll              Pattern   : Data Services                           *
*  Type     : SecurityParameters                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds security configuration parameters.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Collections.Generic;

using Empiria.Json;

namespace Empiria.OnePoint.Security.Data {

  /// <summary>Holds security configuration parameters.</summary>
  static internal class SecurityParameters {

    static private readonly JsonObject _config;

    static SecurityParameters() {
      _config = ConfigurationData.Get<JsonObject>("Authentication.Data");
    }


    static public string ChangePasswordKey => _config.Get<string>("changePasswordKey");


    static internal bool EnsureSimilarUserHostAddress => _config.Get<bool>("ensureSimilarUserHostAddress", false);


    static internal IList<string> PasswordRandomNames => _config.GetList<string>("passwordRandomNames");


    static internal bool SendPasswordsUsingEmail => _config.Get<bool>("sendPasswordsUsingEmail", true);


    static internal bool UseFormerPasswordEncryption => _config.Get("useFormerPasswordEncryption", false);


    static internal bool UseSecurityModelV3 => _config.Get("useSecurityModelV3", true);

  } // class SecurityParameters

} // namespace Empiria.OnePoint.Security.Data
