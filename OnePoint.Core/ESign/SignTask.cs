/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Domain                                  *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Information Holder                      *
*  Type     : SignTask                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains information about an electronic sign task over one or more SignRequests.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign {

  /// <summary>Contains information about an electronic sign task over one or more SignRequests.</summary>
  internal class SignTask {

    #region Constructors and parsers

    internal SignTask(SignEventType eventType,
                      FixedList<SignRequest> signRequests,
                      SignCredentials credentials) {
      this.EventType = eventType;
      this.SignRequests = signRequests;
      this.SignCredentials = credentials;

      this.EnsureIsValid();
    }


    #endregion Constructors and parsers


    #region Properties


    public SignEventType EventType {
      get;
      private set;
    } = SignEventType.Empty;


    internal FixedList<SignRequest> SignRequests {
      get;
      private set;
    }


    internal SignCredentials SignCredentials {
      get;
      private set;
    }


    #endregion Properties


    #region Methods


    private void EnsureIsValid() {
      Assertion.Assert(this.EventType != SignEventType.Empty,
                      "EventType can't have the empty value.");

      Assertion.AssertObject(this.SignCredentials,
                             "SignCredentials has a null value.");

      Assertion.AssertObject(this.SignRequests,
                             "SignRequests has a null value.");

      Assertion.Assert(this.SignRequests.Count > 0,
                       "SignRequests can't be an empty list.");

    }


    #endregion Methods

  }  // class SignTask

}  // namespace Empiria.OnePoint.ESign
