/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Domain                                  *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Enumerated Type                         *
*  Type     : SignStatus                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Electronic-sign status used by signable documents and sign requests.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign {

  /// <summary>Electronic-sign status used by signable documents and sign requests.</summary>
  public enum SignStatus {

    /// <summary>Signature pending.</summary>
    Pending = 'P',

    /// <summary>The document was signed.</summary>
    Signed = 'S',

    /// <summary>The signer refuse to sign the document.</summary>
    Refused = 'R',

    /// <summary>The signer revoke the document sign.</summary>
    Revoked = 'K',

    /// <summary>The document or sign request were marked as deleted.</summary>
    Deleted = 'X',

    /// <summary>Use to control unassigned values.</summary>
    Empty = 'Y',

  }  // enum SignStatus

} // namespace Empiria.OnePoint.ESign
