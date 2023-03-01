/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management                 Component : Interface adapters                    *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Data Transfer Object                  *
*  Types    : UpdateCredentialsFields                      License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Fields used for update subject's credentials.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.Security.Subjects.Adapters {

  /// <summary>Fields used for update subject's credentials.</summary>
  public class UpdateCredentialsFields {

    public string UserID {
      get; set;
    } = string.Empty;


    public string CurrentPassword {
      get; set;
    } = string.Empty;


    public string NewPassword {
      get; set;
    } = string.Empty;


    internal void EnsureValid() {
      Assertion.Require(this.UserID, "fields.UserID");
      Assertion.Require(this.CurrentPassword, "fields.CurrentPassword");
      Assertion.Require(this.NewPassword, "fields.NewPassword");
    }

  } // class UpdateCredentialsFields

}  // namespace Empiria.OnePoint.Security.Subjects.Adapters
