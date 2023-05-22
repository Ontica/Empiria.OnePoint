/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Management                          Component : Providers                             *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Service provider                      *
*  Types    : EMailServices                                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Sends e-mail messages.                                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.IO;

using Empiria.Contacts;

using Empiria.Messaging.EMailDelivery;

namespace Empiria.OnePoint.Security.Providers {

  /// <summary>Sends e-mail messages.</summary>
  static internal class EMailServices {

    #region Methods

    static internal void SendPasswordChangedWarningEMail() {
      Person person = (Person) ExecutionServer.CurrentContact;

      var body = GetTemplate("YourPasswordWasChanged");

      body = ParseGeneralFields(body, person);

      var content = new EMailContent($"Your password was changed", body, true);

      SendEmail(content, person);
    }

    static internal void SendPasswordChangedWarningEMail(Contact contact,
                                                         string userID,
                                                         string newPassword) {

    var body = GetTemplate("YourPasswordWasChanged");

    body = ParseGeneralFields(body, contact, userID, newPassword);

    var content = new EMailContent($"Your password was changed", body, true);

    SendEmail(content, contact);
  }


    #endregion Methods

    #region Helpers

    static private string GetTemplate(string templateName) {
      string templatesPath = ConfigurationData.GetString("Templates.Path");

      string fileName = Path.Combine(templatesPath, $"template.email.{templateName}.html");

      return File.ReadAllText(fileName);
    }


    static private string ParseGeneralFields(string body, Contact contact) {
      body = body.Replace("{{TO-NAME}}", contact.ShortName);

      return body;
    }

    private static string ParseGeneralFields(string body, Contact contact,
                                             string userID, string newPassword) {
      body = body.Replace("{{TO_NAME}}", contact.ShortName);
      body = body.Replace("{{USER_ID}}", userID);
      body = body.Replace("{{PASSWORD}}", newPassword);

      return body;
    }

    static private void SendEmail(EMailContent content, Contact sendToPerson) {
      var sendTo = new SendTo(sendToPerson.EMail, sendToPerson.ShortName);

      EMail.Send(sendTo, content);
    }


    #endregion Helpers

  }  // class EMailServices

}  // namespace Empiria.OnePoint.Security.Providers
