/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Enumeration Type                        *
*  Type     : ESignStatus                                  License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Electronic-sign status used by both signable documents and e-signs.                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign {

  /// <summary>Electronic-sign status used by both signable documents and e-signs.</summary>
  public enum ESignStatus {

    Pending = 'P',

    Signed = 'S',

    Refused = 'R',

    Deleted = 'X',

    Undefined = 'U',

  }  // enum ESignStatus

} // namespace Empiria.OnePoint.ESign
