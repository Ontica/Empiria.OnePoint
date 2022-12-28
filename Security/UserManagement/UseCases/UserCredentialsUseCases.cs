/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : User Management                              Component : Use cases Layer                       *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Use case interactor                   *
*  Type     : UserCredentialsUseCases                      License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Use cases for update user's credentials.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Messaging;
using Empiria.Security;
using Empiria.Services;

using Empiria.OnePoint.Security.Data;

using Empiria.OnePoint.Security.Providers;

namespace Empiria.OnePoint.Security.UserManagement.UseCases {

  /// <summary>Use cases for update user's credentials.</summary>
  public class UserCredentialsUseCases : UseCase {

    #region Constructors and parsers

    protected UserCredentialsUseCases() {
      // no-op
    }

    static public UserCredentialsUseCases UseCaseInteractor() {
      return CreateInstance<UserCredentialsUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public void CreateUserPassword(string apiKey,
                                   string userName, string userEmail,
                                   string newPassword) {
      Assertion.Require(apiKey, nameof(apiKey));
      Assertion.Require(userName, nameof(userName));
      Assertion.Require(userEmail, nameof(userEmail));
      Assertion.Require(newPassword, nameof(newPassword));

      ChangePassword(apiKey, userName, userEmail, newPassword);

      var eventPayload = new {
        userName
      };

      EventNotifier.Notify(MessagingEvents.UserPasswordCreated, eventPayload);
    }


    public void ChangeUserPassword(string currentPassword, string newPassword) {
      Assertion.Require(currentPassword, nameof(currentPassword));
      Assertion.Require(newPassword, nameof(newPassword));

      var apiKey = ConfigurationData.GetString("Empiria.Security", "ChangePasswordKey");

      var userName = EmpiriaPrincipal.Current.Identity.User.UserName;
      var userEmail = EmpiriaPrincipal.Current.Identity.User.EMail;

      ChangePassword(apiKey, userName, userEmail, newPassword);

      var eventPayload = new {
        userName
      };

      EventNotifier.Notify(MessagingEvents.UserPasswordChanged, eventPayload);

      EMailServices.SendPasswordChangedWarningEMail();
    }

    #endregion Use cases

    #region Helpers

    static private void ChangePassword(string apiKey, string username,
                                       string email, string newPassword) {
      if (apiKey != ConfigurationData.GetString("ChangePasswordKey")) {
        throw new SecurityException(SecurityException.Msg.InvalidClientAppKey, apiKey);
      }

      EmpiriaUser user = EmpiriaUser.Parse(username, email);

      var helper = new PasswordStrength(user, newPassword);

      helper.VerifyStrength();

      UserManagementData.ChangePassword(username, newPassword);
    }

    #endregion Helpers

  }  // class UserCredentialsUseCases

}  // namespace Empiria.OnePoint.Security.UserManagement.UseCases
