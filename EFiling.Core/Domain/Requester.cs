/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Information Holder                      *
*  Type     : EFilingPaymentOrder                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains data about a electronic filing requester.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.Json;

namespace Empiria.OnePoint.EFiling {

  public class Requester {

    [DataField("RequestedBy")]
    public string Name {
      get; set;
    }


    public string Email {
      get; set;
    } = String.Empty;


    public string Phone {
      get; set;
    } = String.Empty;


    public string Rfc {
      get; set;
    } = String.Empty;


    #region Methods

    internal void Load(JsonObject json) {
      this.Email = json.Get("email", String.Empty);
      this.Phone = json.Get("phone", String.Empty);
      this.Rfc = json.Get("rfc", String.Empty);
    }


    internal JsonObject ToJson() {
      var json = new JsonObject();

      json.AddIfValue("email", this.Email);
      json.AddIfValue("phone", this.Phone);
      json.AddIfValue("rfc", this.Rfc);

      return json;
    }


    #endregion Methods

  }  // class Requester

}  // namespace Empiria.OnePoint.EFiling
