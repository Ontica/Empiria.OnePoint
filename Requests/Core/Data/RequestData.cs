/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Data Layer                              *
*  Assembly : Empiria.OnePoint.Requests.dll              Pattern   : Data Service                            *
*  Type     : RequestData                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write services for Request instances.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.OnePoint.Requests.Data {

  /// <summary>Provides data read and write services for Request instances.</summary>
  static internal class RequestData {

    static internal void Write(Request o, string extensionData) {
      var op = DataOperation.Parse("write_EOP_Request", o.Id, o.UID, o.RequestType.Id,
            o.UniqueID, o.ControlID, o.RequesterName, o.Description, o.Notes,
            o.RequesterOrgUnit.Id, o.Requester.Id, o.ResponsibleOrgUnit.Id,
            o.FilingTime, o.FiledBy.Id, o.ClosingTime, o.ClosedBy.Id, extensionData, o.Keywords,
            o.PostingTime, o.PostedBy.Id, (char) o.Status);

      DataWriter.Execute(op);
    }

  }  // class RequestData

}  // namespace Empiria.OnePoint.Requests.Data
