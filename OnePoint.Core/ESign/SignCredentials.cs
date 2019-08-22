/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Domain                                  *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Information Holder                      *
*  Type     : SignCredentials                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds information about a user's electronic sign credentials.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign {

  /// <summary>Holds information about a user's electronic sign credentials.</summary>
  internal class SignCredentials {

    #region Constructors and parsers


    internal SignCredentials(string password) {
      Assertion.AssertObject(password, "password");

      this.Password = password;
    }


    #endregion Constructors and parsers

    #region Properties

    internal string Password {
      get;
    }

    #endregion Properties


  }  // class SignCredentials

} // namespace Empiria.OnePoint.ESign
