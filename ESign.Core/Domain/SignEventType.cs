/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Domain                                  *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Enumerated Type                         *
*  Type     : SignEventType                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Describes the type of an electronic sign event type.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign {

  /// <summary>Describes the type of an electronic sign event type.</summary>
  public enum SignEventType {

    Signed = SignStatus.Signed,

    Refused = SignStatus.Refused,

    Revoked = 'K',

    Unrefused = 'U',

    Empty = 'Y',

  }  // enum SignEventType

} // namespace Empiria.OnePoint.ESign
