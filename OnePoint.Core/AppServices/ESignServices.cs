/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Application Service                     *
*  Type     : ESignServices                                License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic signature services.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.OnePoint.ESign;

namespace Empiria.OnePoint.AppServices {

  /// <summary>Use cases that implements electronic signature services.</summary>
  static public class ESignServices {

    #region Public methods

    static public FixedList<SignEvent> Sign(SignTask eSignTask) {
      EnsureValidEventType(eSignTask, SignEventType.Signed);

      FixedList<SignEvent> signEvents = ExecuteSignTask(eSignTask);

      //NotifierService.NotifySigned(signEvents);

      //EMailService.NotifySignEvents(signEvents);

      var a = PaymentOrderData.Empty;

      return signEvents;
    }


    static public FixedList<SignEvent> RefuseSign(SignTask eSignTask) {
      EnsureValidEventType(eSignTask, SignEventType.Refused);

      FixedList<SignEvent> refuseSignEvents = ExecuteSignTask(eSignTask);

      return refuseSignEvents;
    }


    static public FixedList<SignEvent> RevokeSign(SignTask eSignTask) {
      EnsureValidEventType(eSignTask, SignEventType.Revoked);

      FixedList<SignEvent> revokeSignEvents = ExecuteSignTask(eSignTask);

      return revokeSignEvents;
    }


    static public FixedList<SignEvent> UnrefuseSign(SignTask eSignTask) {
      EnsureValidEventType(eSignTask, SignEventType.Unrefused);

      FixedList<SignEvent> unrefuseSignEvents = ExecuteSignTask(eSignTask);

      return unrefuseSignEvents;
    }

    #endregion Public methods

    #region Utility methods

    static private void EnsureValidEventType(SignTask eSignTask,
                                             SignEventType eventType) {
      Assertion.Assert(eSignTask.EventType == eventType,
                       $"EventType type value must be '{eventType}'.");
    }


    static private FixedList<SignEvent> ExecuteSignTask(SignTask task) {
      var currentUserESignCredentials = SignCredentials.ForCurrentUser();

      var signer = new Signer(currentUserESignCredentials);

      FixedList<SignEvent> signEvents = signer.Execute(task);

      return signEvents;
    }

    #endregion Utility methods

  }  // class Empiria.OnePoint.ESign

}  // namespace Empiria.OnePoint.AppServices
