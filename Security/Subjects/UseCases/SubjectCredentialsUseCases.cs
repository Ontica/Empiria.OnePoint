/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management                 Component : Use cases Layer                       *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Use case interactor                   *
*  Type     : SubjectCredentialsUseCases                   License   : Please read LICENSE.txt file          *
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

namespace Empiria.OnePoint.Security.Subjects.UseCases {

  /// <summary>Use cases for update user's credentials.</summary>
  public class SubjectCredentialsUseCases : UseCase {

    #region Constructors and parsers

    protected SubjectCredentialsUseCases() {
      // no-op
    }

    static public SubjectCredentialsUseCases UseCaseInteractor() {
      return CreateInstance<SubjectCredentialsUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public void CreateUserPassword(UserCredentialsDto credentials, string email) {

      Assertion.Require(credentials, nameof(credentials));
      Assertion.Require(email, nameof(email));

      ImplementsCreateUserPassword(credentials, email);

      var eventPayload = new {
        credentials.UserID
      };

      EventNotifier.Notify(MessagingEvents.UserPasswordCreated, eventPayload);
    }


    public void ChangeUserPassword(string currentPassword, string newPassword) {

      Assertion.Require(currentPassword, nameof(currentPassword));
      Assertion.Require(newPassword, nameof(newPassword));

      var apiKey = ConfigurationData.GetString("Empiria.Security", "ChangePasswordKey");

      var userID = ExecutionServer.CurrentIdentity.Name;
      var userEmail = ExecutionServer.CurrentContact.EMail;

      var credentials = new UserCredentialsDto {
        AppKey = apiKey,
        UserID = userID,
        Password = newPassword,
      };

      ImplementsCreateUserPassword(credentials, userEmail);

      var eventPayload = new {
        credentials.UserID
      };

      EventNotifier.Notify(MessagingEvents.UserPasswordChanged, eventPayload);

      EMailServices.SendPasswordChangedWarningEMail();
    }

    #endregion Use cases

    #region Helpers


    public void ImplementsCreateUserPassword(UserCredentialsDto credentials, string email) {

      if (credentials.AppKey != ConfigurationData.GetString("ChangePasswordKey")) {
        throw new SecurityException(SecurityException.Msg.InvalidClientAppKey, credentials.AppKey);
      }

      var service = new Services.AuthenticationService();

      EmpiriaUser user = service.GetUserWithUserNameAndEMail(credentials.UserID, email);

      var helper = new PasswordStrength(user, credentials.Password);

      helper.VerifyStrength();

      SubjectsData.ChangePassword(credentials.UserID, credentials.Password);
    }

    #endregion Helpers

  }  // class SubjectCredentialsUseCases

}  // namespace Empiria.OnePoint.Security.Subjects.UseCases
