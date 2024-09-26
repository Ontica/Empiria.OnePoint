/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Data Layer                              *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Data Services                           *
*  Type     : RequestData                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write services for Request instances.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.Workflow.Requests.Data {

  /// <summary>Provides data read and write services for Request instances.</summary>
  static internal class RequestData {

    static internal string GetNextInternalControlNo(int year) {
      Assertion.Require(year > 0, nameof(year));

      if (year >= 2000) {
        year = year - 2000;
      }

      string sql = "SELECT MAX(WMS_REQ_INTERNAL_CONTROL_NO) " +
                   "FROM WMS_REQUESTS " +
                  $"WHERE WMS_REQ_INTERNAL_CONTROL_NO LIKE '{year}-%'";

      string lastControlNo = DataReader.GetScalar(DataOperation.Parse(sql), String.Empty);

      if (lastControlNo != null && lastControlNo.Length != 0) {

        int consecutive = int.Parse(lastControlNo.Split('-')[1]) + 1;

        return $"{year}-{consecutive:000000}";

      } else {
        return $"{year}-000001";
      }
    }


    static internal string GetNextRequestNo(string prefix, int year) {
      Assertion.Require(prefix, nameof(prefix));
      Assertion.Require(year > 0, nameof(year));

      string sql = "SELECT MAX(WMS_REQ_REQUEST_NO) " +
                   "FROM WMS_REQUESTS " +
                   $"WHERE WMS_REQ_REQUEST_NO LIKE '{prefix}-{year}-%'";

      string lastUniqueID = DataReader.GetScalar(DataOperation.Parse(sql), String.Empty);

      if (lastUniqueID != null && lastUniqueID.Length != 0) {

        int consecutive = int.Parse(lastUniqueID.Split('-')[2]) + 1;

        return $"{prefix}-{year}-{consecutive:00000}";

      } else {
        return $"{prefix}-{year}-00001";
      }
    }


    static internal void Write(Request o, string extensionData) {
      var op = DataOperation.Parse("write_WMS_Request", o.Id, o.UID,
            o.RequestType.Id, o.RequestDef.Id, o.RequestNo, o.InternalControlNo,
            o.Description, o.RequestedBy.Id, o.RequestedByOrgUnit.Id, o.ResponsibleOrgUnit.Id,
            (char) o.Priority, o.DueTime, o.StartedBy.Id, o.StartTime,
            o.EndTime, extensionData, o.Keywords, (char) o.Status);

      DataWriter.Execute(op);
    }

  }  // class RequestData

}  // namespace Empiria.Workflow.Requests.Data
