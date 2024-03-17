/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security                                     Component : Authentication Services               *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Information holder                    *
*  Type     : ClaimAttributeNames                          License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Static class that provides a set of claim attributes constant names.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.Security {

  /// <summary>Static class that provides a set of claim attributes constant names.</summary>
  static internal class ClaimAttributeNames {

    static internal string ContactName => "contactName";

    static internal string MustChangePassword => "mustChangePassword";

    static internal string Password => "password";

    static internal string PasswordNeverExpires => "passwordNeverExpires";

    static internal string PasswordUpdatedDate => "passwordUpdatedDate";

  }  // class ClaimAttributeNames

}  // namespace Empiria.OnePoint.Security
