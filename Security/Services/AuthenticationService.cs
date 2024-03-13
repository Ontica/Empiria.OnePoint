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

using Empiria.StateEnums;

using Empiria.Security;
using Empiria.Security.Providers;

using Empiria.OnePoint.Security.Data;

namespace Empiria.OnePoint.Security.Services {

  /// <summary>Provides user authentication services.</summary>
  public class AuthenticationService: IAuthenticationProvider {

    #region Services

    public IEmpiriaPrincipal Authenticate(string sessionToken, string userHostAddress) {
      Assertion.Require(sessionToken, nameof(sessionToken));
      Assertion.Require(userHostAddress, nameof(userHostAddress));

      EmpiriaPrincipal principal = EmpiriaPrincipal.TryParseWithToken(sessionToken);

      if (principal != null) {
        return principal;
      }

      IEmpiriaSession session = EmpiriaSession.ParseActive(sessionToken);

      if (SecurityParameters.EnsureSimilarUserHostAddress &&
          session.UserHostAddress != userHostAddress) {
        throw new SecurityException(SecurityException.Msg.InvalidUserHostAddress,
                                    userHostAddress);
      }


      var userData = Claim.TryParse(SecurityItemType.SubjectCredentials, session.UserId);

      if (userData == null) {
        throw new SecurityException(SecurityException.Msg.EnsureClaimFailed);
      }

      IEmpiriaUser user = EmpiriaUser.Authenticate(userData);

      var identity = new EmpiriaIdentity(user, AuthenticationMode.Realm);

      var clientApplication = ClientApplication.Parse(session.ClientAppId);

      return new EmpiriaPrincipal(identity, clientApplication, session);
    }


    public IEmpiriaPrincipal Authenticate(IUserCredentials credentials) {
      Assertion.Require(credentials, nameof(credentials));

      Claim userData = GetSubjectAuthenticationClaim(credentials.UserID,
                                                     credentials.Password,
                                                     credentials.Entropy);

      IEmpiriaUser user = EmpiriaUser.Authenticate(userData);

      var identity = new EmpiriaIdentity(user, AuthenticationMode.Basic);

      IClientApplication clientApplication = AuthenticateClientApp(credentials.AppKey);

      CloseActiveUserSessions(user);

      return new EmpiriaPrincipal(identity, clientApplication, credentials);
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


    internal Claim GetSubjectAuthenticationClaim(string userID, string password, string entropy) {
      Assertion.Require(userID, nameof(userID));
      Assertion.Require(password, nameof(password));
      Assertion.Require(entropy, nameof(entropy));

      var claim = Claim.TryParseWithKey(SecurityItemType.SubjectCredentials, userID);

      // User not found
      if (claim == null) {
        throw new SecurityException(SecurityException.Msg.InvalidUserCredentials);
      }

      var storedPassword = claim.GetAttribute<string>("password");

      string decryptedPassword = DecryptPassword(userID, storedPassword, entropy);

      //Invalid password
      if (decryptedPassword != password) {
        throw new SecurityException(SecurityException.Msg.InvalidUserCredentials);
      }

      return claim;
    }


    internal EmpiriaUser GetUserWithUserNameAndEMail(string username, string email) {

      Assertion.Require(username, nameof(username));
      Assertion.Require(email, nameof(email));

      Claim userData = Claim.TryParseWithKey(SecurityItemType.SubjectCredentials,
                                             username);
      if (userData == null) {
        throw new SecurityException(SecurityException.Msg.UserWithEMailNotFound, username, email);
      }

      var user = EmpiriaUser.Parse(userData);

      if (user.EMail.Equals(email)) {
        return user;
      } else {
        throw new SecurityException(SecurityException.Msg.UserWithEMailNotFound, username, email);
      }
    }

    #endregion Services

    #region Helpers


    private void CloseActiveUserSessions(IEmpiriaUser user) {
      if (!ConfigurationData.Get("OneActiveSessionPerUser", false)) {
        return;
      }
      SessionsDataService.CloseAllSessions(user);
      EmpiriaPrincipal.RemoveFromCache(user);
    }


    private string DecryptPassword(string userID, string storedPassword, string entropy) {

      string decrypted = Cryptographer.Decrypt(storedPassword, userID);

      decrypted = Cryptographer.GetSHA256(decrypted + entropy);

      return decrypted;
    }


    #endregion Helpers

  }  // class AuthenticationService

}  // namespace Empiria.OnePoint.Security.Services
