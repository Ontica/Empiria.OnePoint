/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Enumeration Type                        *
*  Type     : RequestStatus                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Indicates the status of an electronic filing request.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.OnePoint.EFiling {

  /// <summary>Indicates the status of an electronic filing request.</summary>
  public enum RequestStatus {

    All = '?',

    Pending = 'P',

    OnSign = 'S',

    OnPayment = 'Y',

    Submitted = 'R',

    Finished = 'C',

    Rejected = 'T',

    Deleted = 'X'

  }  // enum RequestStatus



  /// <summary>Extension methods for RequestStatus enumeration.</summary>
  static public class RequestStatusExtensions {

    static internal string GetName(this RequestStatus status) {
      switch (status) {
        case RequestStatus.Pending:
          return "En elaboración";

        case RequestStatus.OnSign:
          return "En firma";

        case RequestStatus.OnPayment:
          return "Por pagar";

        case RequestStatus.Submitted:
          return "Ingresada";

        case RequestStatus.Finished:
          return "Finalizada";

        case RequestStatus.Rejected:
          return "Devuelta";

        case RequestStatus.Deleted:
          return "Eliminada";

        default:
          return $"Unknown status name for: {status}.";
      }
    }


    static internal bool RequiresExternalDataSynchronization(this RequestStatus newStatus) {
      return (newStatus == RequestStatus.Finished ||
              newStatus == RequestStatus.Rejected ||
              newStatus == RequestStatus.Submitted ||
              newStatus == RequestStatus.OnPayment);
    }

  } // class RequestStatusExtensions

} // namespace Empiria.OnePoint.EFiling
