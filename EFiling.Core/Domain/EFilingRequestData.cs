/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Data Services                           *
*  Type     : EFilingRequestData                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Indicates the status of an electronic filing request.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;
using Empiria.Security;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Provides database methods for recordable resources: real estate and associations.</summary>
  static public class EFilingRequestData {

    #region Internal methods

    static internal FixedList<EFilingRequest> GetList(EFilingRequestStatus status, string keywords) {
      int agencyId = EFilingUserContext.Current().Agency.Id;

      string filter = status != EFilingRequestStatus.All ? $"AgencyId = {agencyId} AND RequestStatus = '{(char) status}'"
                                                         : $"AgencyId = {agencyId}";

      string likeKeywords = SearchExpression.ParseAndLikeKeywords("RequestKeywords", keywords);
      if (!String.IsNullOrWhiteSpace(keywords)) {
        filter += " AND " + likeKeywords;
      }

      string sort = "PostingTime DESC";

      return BaseObject.GetList<EFilingRequest>(filter, sort).ToFixedList();
    }

    static internal void WriteFilingRequest(EFilingRequest o) {
      var op = DataOperation.Parse("writeEOPFilingRequest", o.Id, o.UID,
                    o.Procedure.Id, o.RequestedBy.name, o.Agency.Id, o.Agent.Id,
                    o.ExtensionData.ToString(), o.Keywords,
                    o.LastUpdateTime, o.PostingTime, o.PostedBy.Id,
                    (char) o.Status, o.Integrity.GetUpdatedHashCode());

      DataWriter.Execute(op);
    }


    #endregion Internal methods

  } // class EFilingRequestData

} // namespace Empiria.OnePoint.EFiling
