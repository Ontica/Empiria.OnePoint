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

    static internal string GetNextControlNumber(int year) {
      Assertion.Require(year > 0, nameof(year));

      if (year >= 2000) {
        year = year - 2000;
      }

      string sql = "SELECT MAX(REQ_CONTROL_ID) " +
                   "FROM WKF_REQUESTS " +
                  $"WHERE REQ_CONTROL_ID LIKE '{year}-%'";

      string lastControlNo = DataReader.GetScalar(DataOperation.Parse(sql), String.Empty);

      if (lastControlNo != null && lastControlNo.Length != 0) {

        int consecutive = int.Parse(lastControlNo.Split('-')[1]) + 1;

        return $"{year}-{consecutive:000000}";

      } else {
        return $"{year}-000001";
      }
    }

    static internal string GetNextUniqueID(string prefix, int year) {
      Assertion.Require(prefix, nameof(prefix));
      Assertion.Require(year > 0, nameof(year));

      string sql = "SELECT MAX(REQ_UNIQUE_ID) " +
                   "FROM WKF_REQUESTS " +
                   $"WHERE REQ_UNIQUE_ID LIKE '{prefix}-{year}-%'";

      string lastUniqueID = DataReader.GetScalar(DataOperation.Parse(sql), String.Empty);

      if (lastUniqueID != null && lastUniqueID.Length != 0) {

        int consecutive = int.Parse(lastUniqueID.Split('-')[2]) + 1;

        return $"{prefix}-{year}-{consecutive:00000}";

      } else {
        return $"{prefix}-{year}-00001";
      }
    }


    static internal void Write(Request o, string extensionData) {
      var op = DataOperation.Parse("write_WKF_Request", o.Id, o.UID, o.RequestType.Id,
            o.UniqueID, o.ControlID, o.RequesterName, o.Description, string.Empty,
            o.RequesterOrgUnit.Id, o.Requester.Id, o.ResponsibleOrgUnit.Id,
            o.WorkflowInstance.Id, o.FiledBy.Id, o.FilingTime, o.ClosedBy.Id, o.ClosingTime,
            extensionData, o.Keywords, o.PostedBy.Id, o.PostingTime, (char) o.Status);

      DataWriter.Execute(op);
    }

  }  // class RequestData

}  // namespace Empiria.Workflow.Requests.Data
