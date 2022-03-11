/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Data Services                           *
*  Type     : EFilingRepository                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data persistence methods for electronic filing requests.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Provides data persistence methods for electronic filing requests.</summary>
  static public class EFilingRepository {

    #region Internal methods

    static internal FixedList<EFilingRequest> GetList(RequestStatus status,
                                                      string keywords,
                                                      int count) {
      int agencyId = EFilingUserContext.Current().Agency.Id;

      string filter = String.Empty;

      if (status != RequestStatus.All) {
        filter = $"AgencyId = {agencyId} AND RequestStatus = '{(char) status}'";
      } else {
        filter = $"AgencyId = {agencyId} AND RequestStatus <> 'X'";
      }

      string likeKeywords = SearchExpression.ParseAndLikeKeywords("RequestKeywords", keywords);
      if (!String.IsNullOrWhiteSpace(keywords)) {
        filter += " AND " + likeKeywords;
      }

      string sort = "FilingRequestId DESC";

      var list = BaseObject.GetList<EFilingRequest>(filter, sort);

      if (count > 0 && list.Count > count) {
        return BaseObject.GetList<EFilingRequest>(filter, sort).GetRange(0, count).ToFixedList();
      } else {
        return BaseObject.GetList<EFilingRequest>(filter, sort).ToFixedList();
      }
    }


    static internal void WriteFilingRequest(EFilingRequest o) {
      var op = DataOperation.Parse("writeEOPFilingRequest", o.Id, o.UID,
                    o.Procedure.Id, o.RequestedBy.Name, o.Agency.Id, o.Agent.Id,
                    o.ExtensionData.ToString(), o.Keywords, o.LastUpdate,
                    o.Transaction.UID, o.Transaction.Status,
                    o.Transaction.ExtensionData.ToString(), o.Transaction.LastUpdate,
                    o.PostingTime, o.PostedBy.Id, (char) o.Status, o.SecurityData.Integrity.GetUpdatedHashCode());

      DataWriter.Execute(op);
    }


    #endregion Internal methods

  } // class EFilingRepository

} // namespace Empiria.OnePoint.EFiling
