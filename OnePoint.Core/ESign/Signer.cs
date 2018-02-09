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

      List<SignEvent> eventsList = new List<SignEvent>(task.SignRequests.Count);

      foreach (var request in task.SignRequests) {
        SignEvent signEvent = this.CreateSignEvent(task, request);

        eventsList.Add(signEvent);
      }

      return eventsList.ToFixedList();
    }

    #endregion Methods

    #region Private methods

    private SignEvent CreateSignEvent(SignTask signTask, SignRequest signRequest) {
      string sign = this.SignInputData(signTask.ESignCredentials,
                                       signRequest.Document.SignInputData);

      var signEvent = new SignEvent(signTask.EventType, signRequest, sign);

      return signEvent;
    }

    private void EnsureValidCredentials(SignTask signTask) {
      Assertion.Assert(signTask.ESignCredentials.Password == "prueba",
                       "No reconozco la contraseña para ejecutar la firma electrónica.");
    }

    private string SignInputData(SignCredentials credentials, string inputData) {
      return Cryptographer.CreateHashCode(inputData, credentials.Password);
    }

    #endregion Methods

  }  // class Signer

}  // namespace Empiria.OnePoint.ESign
