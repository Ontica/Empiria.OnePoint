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

using Empiria.Contacts;

using Empiria.Security;
using Empiria.Security.Providers;

using Empiria.OnePoint.Security.Data;
using Empiria.OnePoint.Security.Subjects;

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
        throw new SecurityException(SecurityException.Msg.ClientAppKeyIsSuspended, clientAppKey);
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

      if (claim.Status == EntityStatus.Suspended) {
        throw new SecurityException(SecurityException.Msg.UserAccountIsSuspended, userID);
      }

      var storedPassword = claim.GetAttribute<string>(ClaimAttributeNames.Password);

      string decryptedPassword = DecryptPassword(userID, storedPassword, entropy);

      if (decryptedPassword == password) {

        AuthenticationAttemptsRegister.Remove(userID);

        return claim;
      }

      //Invalid password

      AuthenticationAttemptsRegister.Add(userID);

      if (AuthenticationAttemptsRegister.MaxAttemptsReached(userID)) {
        throw SuspendUserAccount(claim);
      } else {
        throw new SecurityException(SecurityException.Msg.InvalidUserCredentials);
      }
    }

    #endregion Services

    #region Helpers

    private void CloseActiveUserSessions(IEmpiriaUser user) {
      if (!ConfigurationData.Get("OneActiveSessionPerUser", false)) {
        return;
      }
      EmpiriaPrincipal.CloseAllSessions(user.Contact);
    }


    private string DecryptPassword(string userID, string storedPassword, string entropy) {

      string decrypted = Cryptographer.Decrypt(storedPassword, userID);

      decrypted = Cryptographer.GetSHA256(decrypted + entropy);

      return decrypted;
    }


    private SecurityException SuspendUserAccount(Claim claim) {
      var contact = Contact.Parse(claim.SubjectId);

      var editor = new SubjectSecurityItemsEditor(contact);

      if (editor.GetCredentialsStatus() == EntityStatus.Active) {
        editor.SuspendSubjectCredentials();
      }

      EmpiriaLog.UserManagementLog(contact, "La cuenta de la persona usuaria fue suspendida por intentos de acceso fallidos");

      return new SecurityException(SecurityException.Msg.UserAccountHasBeenBlocked);
    }

    #endregion Helpers

  }  // class AuthenticationService

}  // namespace Empiria.OnePoint.Security.Services
