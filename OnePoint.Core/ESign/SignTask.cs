/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Information Holder                      *
*  Type     : SignTask                                     License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains information about an electronic sign task over one or more SignRequest entities.      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.OnePoint.ESign {

  /// <summary>Contains information about an electronic sign task over one
  /// or more SignRequest entities.</summary>
  public class SignTask {

    #region Constructors and parsers

    private SignTask(JsonObject json) {
      this.LoadData(json);

      this.EnsureIsValid();
    }


    static public SignTask Parse(JsonObject json) {
      SignTask.EnsureIsValid(json);

      return new SignTask(json);
    }


    static public void EnsureIsValid(JsonObject json) {
      Assertion.AssertObject(json, "json");

      Assertion.Assert(json.HasValue("eventType"),
                      "Electronic sign task must have a 'eventType' value.");

      Assertion.Assert(json.HasValue("credentials"),
                       "Electronic sign task must have a 'credentials' field with no empty data.");

      Assertion.Assert(json.HasValue("signRequests"),
                        "Electronic sign task must have a no empty 'signRequests' array.");
    }

    #endregion Constructors and parsers

    #region Properties

    public SignEventType EventType {
      get;
      private set;
    } = SignEventType.Empty;


    public FixedList<SignRequest> SignRequests {
      get;
      private set;
    } = new FixedList<SignRequest>();


    internal SignCredentials SignCredentials {
      get;
      private set;
    } = SignCredentials.Empty;

    #endregion Properties

    #region Methods

    private void EnsureIsValid() {
      Assertion.Assert(this.EventType != SignEventType.Empty,
                      "EventType can't have the empty value.");

      Assertion.Assert(this.SignRequests.Count > 0,
                       "SignRequests can't be an empty list.");

      Assertion.Assert(!this.SignCredentials.IsEmptyInstance,
                       "SignCredentials can't be an empty instance.");
    }


    private void LoadData(JsonObject json) {
      this.EventType = json.Get<SignEventType>("eventType");
      this.SignCredentials = SignCredentials.Parse(json.Slice("credentials"));

      this.SignRequests = json.GetList<SignRequest>("signRequests")
                              .ToFixedList();
    }

    #endregion Methods

  }  // class SignTask

}  // namespace Empiria.OnePoint.ESign
