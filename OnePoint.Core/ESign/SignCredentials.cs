/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Information Holder                      *
*  Type     : SignCredentials                              License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds information about a user's electronic sign credentials.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.OnePoint.ESign {

  /// <summary>Holds information about a user's electronic sign credentials.</summary>
  public class SignCredentials {

    #region Constructors and parsers

    private SignCredentials() {

    }


    private SignCredentials(JsonObject json) {
      this.Password = json.Get<string>("password");
    }


    static public SignCredentials Parse(JsonObject json) {
      SignCredentials.EnsureIsValid(json);

      return new SignCredentials(json);
    }


    static public SignCredentials Empty {
      get {
        return new SignCredentials() {
          IsEmptyInstance = true
        };
      }
    }


    static internal void EnsureIsValid(JsonObject json) {
      Assertion.AssertObject(json, "json");

      Assertion.Assert(json.HasValue("password"),
                       "Credentials object must have a 'password' field with no empty data.");
    }

    #endregion Constructors and parsers

    #region Properties

    public bool IsEmptyInstance {
      get;
      private set;
    } = false;


    public string Password {
      get;
    }


    public static SignCredentials ForCurrentUser() {
      var json = new JsonObject() {
        new JsonItem("password", "prueba"),
      };

      return new SignCredentials(json);
    }

    #endregion Properties

  }  // class SignCredentials

} // namespace Empiria.OnePoint.ESign
