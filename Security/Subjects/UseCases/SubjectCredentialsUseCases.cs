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

using Empiria.Contacts;
using Empiria.Messaging;

using Empiria.Security;
using Empiria.Services;

using Empiria.OnePoint.Security.Data;

using Empiria.OnePoint.Security.Providers;
using Empiria.OnePoint.Security.Subjects.Adapters;

namespace Empiria.OnePoint.Security.Subjects.UseCases {

  /// <summary>Use cases for update user's credentials.</summary>
  public class SubjectCredentialsUseCases : UseCase {

    #region Constructors and parsers

    static public SubjectCredentialsUseCases UseCaseInteractor() {
      return CreateInstance<SubjectCredentialsUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public void ResetCredentials(string contactUID) {
      Assertion.Require(contactUID, nameof(contactUID));

      var contact = Contact.Parse(contactUID);

      SubjectData subject = SubjectsDataService.GetSubject(contact);

      string newPassword = GeneratePassword();

      var editor = new SubjectSecurityItemsEditor(contact);

      editor.UpdateSubjectCredentials(EncryptPassword(subject.UserID, newPassword));

      if (SecurityParameters.SendPasswordsUsingEmail) {
        EmailServices.SendPasswordChangedWarningEMail(contact, subject.UserID, newPassword);

      } else {

        throw new ServiceException("Servidor de correo electrónico no configurado",
            $"El password asignado al usuario no pudo enviarse " +
            $"por correo electrónico. Sin embargo este es el password: {newPassword}");
      }
    }


    public void UpdateCredentials(UpdateCredentialsFields fields) {
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      var contact = ExecutionServer.CurrentContact;

      SubjectData subject = SubjectsDataService.GetSubject(contact);

      Assertion.Require(fields.UserID.Equals(subject.UserID),
                        "Unrecognized UserID value");

      // ToDo: ensure valid current password

      string newPassword = EncryptPassword(subject.UserID, fields.NewPassword);

      var editor = new SubjectSecurityItemsEditor(contact);

      editor.UpdateSubjectCredentials(newPassword);

      if (SecurityParameters.SendPasswordsUsingEmail) {
        EmailServices.SendPasswordChangedWarningEMail();

      } else {

        throw new ServiceException("Servidor de correo electrónico no configurado",
            $"El password asignado al usuario no pudo enviarse " +
            $"por correo electrónico. Sin embargo este es el password: {newPassword}");
      }
    }

    #endregion Use cases

    #region Former use cases

    public void FormerCreateUserPassword(UserCredentialsDto credentials, string email) {

      Assertion.Require(credentials, nameof(credentials));
      Assertion.Require(email, nameof(email));

      FormerImplementsCreateUserPassword(credentials, email);

      var eventPayload = new {
        credentials.UserID
      };

      EventNotifier.Notify(MessagingEvents.UserPasswordCreated, eventPayload);
    }


    public void FormerChangeUserPassword(string currentPassword, string newPassword) {

      Assertion.Require(currentPassword, nameof(currentPassword));
      Assertion.Require(newPassword, nameof(newPassword));

      var apiKey = SecurityParameters.ChangePasswordKey;

      var userID = ExecutionServer.CurrentIdentity.Name;
      var userEmail = ExecutionServer.CurrentContact.EMail;

      var credentials = new UserCredentialsDto {
        AppKey = apiKey,
        UserID = userID,
        Password = newPassword,
      };

      FormerImplementsCreateUserPassword(credentials, userEmail);

      var eventPayload = new {
        credentials.UserID
      };

      EventNotifier.Notify(MessagingEvents.UserPasswordChanged, eventPayload);

      EmailServices.SendPasswordChangedWarningEMail();
    }

    #endregion Former use cases

    #region Helpers

    private string EncryptPassword(string userID, string password) {

      return Cryptographer.Encrypt(EncryptionMode.EntropyKey,
                                   Cryptographer.GetSHA256(password), userID);
    }


    public void FormerImplementsCreateUserPassword(UserCredentialsDto credentials, string email) {

      if (credentials.AppKey != SecurityParameters.ChangePasswordKey) {
        throw new SecurityException(SecurityException.Msg.InvalidClientAppKey, credentials.AppKey);
      }

      var service = new Services.AuthenticationService();

      EmpiriaUser user = service.GetUserWithUserNameAndEMail(credentials.UserID, email);

      var helper = new PasswordStrength(user, credentials.Password);

      helper.VerifyStrength();

      SubjectsDataService.ChangePassword(credentials.UserID, credentials.Password);
    }


    private string GeneratePassword() {
      var list = SecurityParameters.PasswordRandomNames;

      int position = EmpiriaMath.GetRandom(0, list.Count - 1);

      return $"{list[position]}@{EmpiriaMath.GetRandom(1000, 99999)}";
    }

    #endregion Helpers

  }  // class SubjectCredentialsUseCases

}  // namespace Empiria.OnePoint.Security.Subjects.UseCases