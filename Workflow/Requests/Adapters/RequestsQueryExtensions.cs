/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Type Extension methods                  *
*  Type     : RequestsQueryExtensions                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Extension methods for RequestsQuery interface adapter.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Data;
using Empiria.Parties;
using Empiria.StateEnums;

namespace Empiria.Workflow.Requests.Adapters {

  /// <summary>Extension methods for RequestsQuery interface adapter.</summary>
  static internal class RequestsQueryExtensions {

    #region Extension methods

    static internal void EnsureIsValid(this RequestsQuery query) {
      // no-op
    }

    static internal string MapToFilterString(this RequestsQuery query) {
      string requestsListFilter = BuildRequestsListFilter(query);
      string requesterOrgUnitFilter = BuildRequesterOrgUnitFilter(query);
      string requestTypeFilter = BuildRequestTypeFilter(query);
      string requestStatusFilter = BuildRequestStatusFilter(query);
      string dateRangeFilter = BuildDateRangeFilter(query);

      var filter = new Filter(requestsListFilter);
      filter.AppendAnd(requesterOrgUnitFilter);
      filter.AppendAnd(requestTypeFilter);
      filter.AppendAnd(requestStatusFilter);
      filter.AppendAnd(dateRangeFilter);

      return filter.ToString();
    }


    static internal string MapToSortString(this RequestsQuery query) {
      if (query.OrderBy.Length != 0) {
        return query.OrderBy;
      } else {
        return "REQ_CONTROL_ID, REQ_UNIQUE_ID";
      }
    }

    #endregion Extension methods

    #region Helpers

    static private string BuildDateRangeFilter(RequestsQuery query) {
      if (query.DateSearchField == DateSearchField.None) {
        return string.Empty;
      }

      string filter = $"{DataCommonMethods.FormatSqlDbDate(query.FromDate)} <= @DATE_FIELD@ AND " +
                      $"@DATE_FIELD@ < {DataCommonMethods.FormatSqlDbDate(query.ToDate.Date.AddDays(1))}";

      if (query.DateSearchField == DateSearchField.FilingDate) {
        return filter.Replace("@DATE_FIELD@", "REQ_FILING_TIME");

      } else if (query.DateSearchField == DateSearchField.ClosingDate) {
        return filter.Replace("@DATE_FIELD@", "REQ_CLOSING_TIME");

      } else if (query.DateSearchField == DateSearchField.PostingDate) {
        return filter.Replace("@DATE_FIELD@", "REQ_POSTING_TIME");

      } else {
        throw Assertion.EnsureNoReachThisCode();
      }
    }

    static private string BuildRequesterOrgUnitFilter(RequestsQuery query) {
      if (query.RequesterOrgUnitUID.Length == 0) {
        return string.Empty;
      }

      var requesterOrgUnit = OrganizationalUnit.Parse(query.RequesterOrgUnitUID);

      return $"REQ_REQUESTER_ORG_UNIT_ID = {requesterOrgUnit.Id}";
    }


    static private string BuildRequestsListFilter(RequestsQuery query) {
      if (query.RequestsList.Length == 0) {
        return string.Empty;
      }

      var list = RequestType.GetList(query.RequestsList);

      string filter = string.Empty;
      foreach (var requestType in list) {
        if (filter.Length != 0) {
          filter += ", ";
        }
        filter += requestType.Id;
      }

      return $"REQ_TYPE_ID IN ({filter})";
    }


    static private string BuildRequestTypeFilter(RequestsQuery query) {
      if (query.RequestTypeUID.Length == 0) {
        return string.Empty;
      }

      var requestType = RequestType.Parse(query.RequestTypeUID);

      return $"REQ_TYPE_ID = {requestType.Id}";
    }


    static private string BuildRequestStatusFilter(RequestsQuery query) {
      if (query.RequestStatus == ActivityStatus.All) {
        return string.Empty;
      }

      return $"REQ_STATUS = '{(char) query.RequestStatus}'";
    }

    #endregion Helpers

  }  // class RequestsQueryExtensions

}  // namespace Empiria.Workflow.Requests.Adapters
