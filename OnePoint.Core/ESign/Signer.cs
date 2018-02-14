/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Information Holder                      *
*  Type     : Signer                                       License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs the electronic sign of one or more documents according to a SignTask command.         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Security;

namespace Empiria.OnePoint.ESign {

  /// <summary>Performs the electronic sign of one or more documents according
  /// to a SignTask command.</summary>
  public class Signer {

    #region Fields

    private SignCredentials credentials;

    #endregion Fields

    #region Constructors and parsers

    public Signer(SignCredentials credentials) {
      this.credentials = credentials;
    }

    #endregion Constructors and parsers

    #region Methods

    public FixedList<SignEvent> Execute(SignTask task) {
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
          string digitalSign = this.SignData(signTask.ESignCredentials,
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
      Assertion.Assert(signTask.ESignCredentials.Password == "prueba",
                       "No reconozco la contraseña para ejecutar la firma electrónica.");
    }


    private string SignData(SignCredentials credentials, string inputData) {
      return Cryptographer.CreateHashCode(inputData, credentials.Password);
    }

    #endregion Methods

  }  // class Signer

}  // namespace Empiria.OnePoint.ESign
