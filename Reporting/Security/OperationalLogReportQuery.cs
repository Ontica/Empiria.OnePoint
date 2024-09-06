/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : OnePoint Security Reporting                  Component : Adapters                              *
*  Assembly : Empiria.OnePoint.Reporting.dll               Pattern   : Query object                          *
*  Type     : OperationalLogReportQuery                    License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Query object used to retrieve information about logs.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.Reporting.Security {

  /// <summary>Query object used to retrieve information about logs.</summary>
  public class OperationalLogReportQuery {

    public LogOperationType OperationLogType {
      get; set;
    }

    public DateTime FromDate {
      get; set;
    }

    public DateTime ToDate {
      get; set;
    }

  }  // class OperationalLogReportQuery

} // namespace Empiria.OnePoint.Reporting.Security
