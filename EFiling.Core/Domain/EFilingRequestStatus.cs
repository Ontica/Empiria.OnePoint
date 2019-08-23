/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Enumeration Type                        *
*  Type     : EFilingRequestStatus                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Indicates the status of an electronic filing request.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Indicates the status of an electronic filing request.</summary>
  public enum EFilingRequestStatus {

    All = '?',

    Pending = 'P',

    Signed = 'S',

    OnPayment = 'Y',

    Submitted = 'R',

    Finished = 'C',

    Rejected = 'T',

    Deleted = 'X'

  }

} // namespace Empiria.OnePoint.EFiling
