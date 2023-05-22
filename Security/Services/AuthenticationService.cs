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

      if (!session.IsStillActive) {
        throw new SecurityException(SecurityException.Msg.ExpiredSessionToken,
                                    session.Token);
      }

      if (SecurityParameters.EnsureSimilarUserHostAddress &&
          !session.UserHostAddress.Equals(userHostAddress)) {
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

      Claim userData = GetSubjectAuthenticationClaim(credentials);

      IEmpiriaUser user = EmpiriaUser.Authenticate(userData);

      var identity = new EmpiriaIdentity(user, AuthenticationMode.Basic);

      IClientApplication clientApplication = AuthenticateClientApp(credentials.AppKey);

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

    private string DecryptPassword(string storedPassword,
                                   IUserCredentials credentials) {

      bool useSecurityModelV3 = SecurityParameters.UseSecurityModelV3;

      string decrypted;

      if (useSecurityModelV3) {

        decrypted = Cryptographer.Decrypt(storedPassword, credentials.UserID);
        decrypted = Cryptographer.GetSHA256(decrypted + credentials.Entropy);

      } else if (!String.IsNullOrWhiteSpace(credentials.Entropy)) {

        decrypted = FormerCryptographer.Decrypt(storedPassword, credentials.UserID);
        decrypted = FormerCryptographer.GetMD5HashCode(decrypted + credentials.Entropy);

      } else {

        decrypted = FormerCryptographer.Decrypt(storedPassword, credentials.UserID);

      }

      return decrypted;
    }


    private Claim GetSubjectAuthenticationClaim(IUserCredentials credentials) {

      var claim = Claim.TryParseWithKey(SecurityItemType.SubjectCredentials,
                                        credentials.UserID);

      // No user found
      if (claim == null) {
        throw new SecurityException(SecurityException.Msg.InvalidUserCredentials);
      }

      var storedPassword = claim.GetAttribute<string>("password");

      string decryptedPassword = DecryptPassword(storedPassword, credentials);

      //Invalid password
      if (decryptedPassword != credentials.Password) {
        throw new SecurityException(SecurityException.Msg.InvalidUserCredentials);
      }

      return claim;
    }

    #endregion Helpers

  }  // class AuthenticationService

}  // namespace Empiria.OnePoint.Security.Services
