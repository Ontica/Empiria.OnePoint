/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Domain                                  *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Command processor                       *
*  Type     : SignTaskProcessor                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs SignTask command execution over one or more documents.                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Security;

using Empiria.Security;

namespace Empiria.OnePoint.ESign {

  /// <summary>Performs SignTask command execution over one or more documents.</summary>
  internal class SignTaskProcessor {

    #region Constructors and parsers


    internal SignTaskProcessor() {

    }


    #endregion Constructors and parsers


    #region Methods


    internal FixedList<SignEvent> Execute(SignTask task) {
      Assertion.AssertObject(task, "task");

      this.EnsureValidCredentials(task);

      var eventsList = new List<SignEvent>(task.SignRequests.Count);

      using (var context = StorageContext.Open()) {

        foreach (var request in task.SignRequests) {
          SignEvent signEvent = this.CreateSignEvent(task, request);

          context.Watch(request);

          signEvent.Save();
          request.Save();

          eventsList.Add(signEvent);
        }

        context.Update();

      }  // using

      return eventsList.ToFixedList();
    }


    #endregion Methods

    #region Private methods


    private SignEvent CreateSignEvent(SignTask signTask, SignRequest signRequest) {
      switch (signTask.EventType) {
        case SignEventType.Signed:
          string digitalSign = this.SignData(signTask.SignCredentials,
                                             signRequest.Document.SignInputData);

          return signRequest.Sign(digitalSign);

        case SignEventType.Refused:
          return signRequest.Refuse();

        case SignEventType.Revoked:
          return signRequest.Revoke();

        case SignEventType.Unrefused:
          return signRequest.Unrefuse();

        default:
          throw Assertion.AssertNoReachThisCode();
      }
    }


    private void EnsureValidCredentials(SignTask signTask) {
      //Assertion.Assert(signTask.SignCredentials.Password == "prueba1",
      //                 "No reconozco la contraseña para ejecutar la firma electrónica.");
    }


    private string SignData(SignCredentials credentials, string inputData) {
      SecureString securedPassword = Cryptographer.ConvertToSecureString(credentials.Password);

      return Cryptographer.SignText(inputData, securedPassword);
    }


    #endregion Methods

  }  // class SignTaskProcessor

}  // namespace Empiria.OnePoint.ESign
