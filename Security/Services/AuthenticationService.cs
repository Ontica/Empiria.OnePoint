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
using Empiria.Json;
using Empiria.Security;
using Empiria.Security.Providers;
using Empiria.StateEnums;

namespace Empiria.OnePoint.Security.Services {

  /// <summary>Provides user authentication services.</summary>
  public class AuthenticationService: IAuthenticationProvider {

    public IEmpiriaPrincipal Authenticate(string sessionToken) {
      Assertion.Require(sessionToken, nameof(sessionToken));

      EmpiriaPrincipal principal = EmpiriaPrincipal.TryParseWithToken(sessionToken);

      if (principal != null) {
        return principal;
      }

      IEmpiriaSession session = RetrieveActiveSession(sessionToken);

      IEmpiriaUser user = EmpiriaUser.Authenticate(session);

      var identity = new EmpiriaIdentity(user, AuthenticationMode.Realm);

      IClientApplication clientApplication = TEMP_AuthenticateClientApp(session.ClientAppId);

      return new EmpiriaPrincipal(identity, clientApplication, session);
    }


    public IEmpiriaPrincipal Authenticate(IUserCredentials credentials) {
      Assertion.Require(credentials, nameof(credentials));

      IClientApplication clientApplication = AuthenticateClientApp(credentials.ClientAppKey);

      IEmpiriaUser user = EmpiriaUser.Authenticate(clientApplication,
                                                  credentials.Username, credentials.Password,
                                                  credentials.Entropy);
      Assertion.Require(user, "user");

      var identity = new EmpiriaIdentity(user, AuthenticationMode.Basic);

      return new EmpiriaPrincipal(identity, clientApplication, credentials.ContextData);
    }


    public IClientApplication AuthenticateClientApp(string clientAppKey) {
      Assertion.Require(clientAppKey, clientAppKey);

      ClientApplication application = ClientApplication.TryParse(clientAppKey);

      if (application == null) {
        throw new SecurityException(SecurityException.Msg.InvalidClientAppKey, clientAppKey);
      }

      if (application.Status != EntityStatus.Active) {
        throw new SecurityException(SecurityException.Msg.NotActiveClientAppKey, clientAppKey);
      }

      return application;
    }


    internal Claim Authenticate(IClientApplication app, string username,
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


    internal IEmpiriaSession CreateSession(EmpiriaPrincipal principal, JsonObject contextData) {
      Assertion.Require(principal, nameof(principal));


      return EmpiriaSession.Create(principal, contextData);
    }


    internal IEmpiriaSession RetrieveActiveSession(string sessionToken) {
      Assertion.Require(sessionToken, nameof(sessionToken));

      return EmpiriaSession.ParseActive(sessionToken);
    }


    internal IClientApplication TEMP_AuthenticateClientApp(int clientAppId) {
      return ClientApplication.Parse(clientAppId);
    }


    internal Claim TryGetUser(IEmpiriaSession activeSession) {
      var clientApp = ClientApplication.Parse(activeSession.ClientAppId);

      return Claim.TryParse(SecurityItemType.SubjectCredentials, clientApp, activeSession.UserId);
    }


    internal Claim TryGetUserWithUserName(IClientApplication app, string username) {
      Assertion.Require(app, nameof(app));
      Assertion.Require(username, nameof(username));

      return Claim.TryParseWithKey(SecurityItemType.SubjectCredentials, app, username);
    }

  }  // class AuthenticationService

}  // namespace Empiria.OnePoint.Security.Services
