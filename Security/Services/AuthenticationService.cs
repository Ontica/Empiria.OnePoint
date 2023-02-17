/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Services                              *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Service provider                      *
*  Type     : AuthenticationService                        License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides user authentication services.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security;
using Empiria.Security.Providers;

namespace Empiria.OnePoint.Security.Services {

  /// <summary>Provides user authentication services.</summary>
  public class AuthenticationService: IAuthenticationProvider {

    public ISubjectClaim Authenticate(ClientApplication app, string username,
                                      string password, string entropy) {
      Assertion.Require(app, nameof(app));
      Assertion.Require(username, nameof(username));
      Assertion.Require(password, nameof(password));
      Assertion.Require(entropy, nameof(entropy));

      var claim = Claim.TryParseWithKey(SecurityItemType.SubjectCredentials, app, username);

      // No user found
      if (claim == null) {
        throw new SecurityException(SecurityException.Msg.InvalidUserCredentials);
      }

      bool useSecurityModelV3 = ConfigurationData.Get("UseSecurityModel.V3", false);

      var storedPassword = claim.GetAttribute<string>("password");

      string p;

      if (useSecurityModelV3) {
        p = Cryptographer.Decrypt(storedPassword, username);
        p = Cryptographer.GetSHA256(p + entropy);

      } else if (!String.IsNullOrWhiteSpace(entropy)) {
        p = FormerCryptographer.Decrypt(storedPassword, username);
        p = FormerCryptographer.GetMD5HashCode(p + entropy);

      } else {
        p = FormerCryptographer.Decrypt(storedPassword, username);
      }

      //Invalid password
      if (p != password) {
        throw new SecurityException(SecurityException.Msg.InvalidUserCredentials);
      }

      return claim;
    }


    public ISubjectClaim TryGetUser(ClientApplication app, int userId) {
      Assertion.Require(app, nameof(app));

      return Claim.TryParse(SecurityItemType.SubjectCredentials, app, userId);
    }


    public ISubjectClaim TryGetUserWithUserName(ClientApplication app, string username) {
      Assertion.Require(app, nameof(app));
      Assertion.Require(username, nameof(username));

      return Claim.TryParseWithKey(SecurityItemType.SubjectCredentials, app, username);
    }

  }  // class AuthenticationService

}  // namespace Empiria.OnePoint.Security.Services
