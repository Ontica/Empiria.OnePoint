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

using Empiria.Workflow.Definition;

namespace Empiria.Workflow.Requests.Adapters {

  /// <summary>Extension methods for RequestsQuery interface adapter.</summary>
  static internal class RequestsQueryExtensions {

    #region Extension methods

    static internal void EnsureIsValid(this RequestsQuery query) {
      // no-op
    }

    static internal string MapToFilterString(this RequestsQuery query) {
      string requestDefFilter = BuildRequestDefinitionFilter(query);
      string requesterOrgUnitFilter = BuildRequestedByOrgUnitFilter(query);
      string requestsListFilter = BuildRequestsListFilter(query);
      string requestStatusFilter = BuildRequestStatusFilter(query);
      string dateRangeFilter = BuildDateRangeFilter(query);

      var filter = new Filter(requestDefFilter);
      filter.AppendAnd(requesterOrgUnitFilter);
      filter.AppendAnd(requestsListFilter);
      filter.AppendAnd(requestStatusFilter);
      filter.AppendAnd(dateRangeFilter);

      return filter.ToString();
    }


    static internal string MapToSortString(this RequestsQuery query) {
      if (query.OrderBy.Length != 0) {
        return query.OrderBy;
      } else {
        return "WMS_REQ_INTERNAL_CONTROL_NO, WMS_REQ_REQUEST_NO";
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

      if (query.DateSearchField == DateSearchField.DueTime) {
        return filter.Replace("@DATE_FIELD@", "WMS_REQ_DUE_TIME");

      } else if (query.DateSearchField == DateSearchField.StartTime) {
        return filter.Replace("@DATE_FIELD@", "WMS_REQ_START_TIME");

      } else if (query.DateSearchField == DateSearchField.EndTime) {
        return filter.Replace("@DATE_FIELD@", "WMS_REQ_END_TIME");

      } else {
        throw Assertion.EnsureNoReachThisCode();
      }
    }

    static private string BuildRequestedByOrgUnitFilter(RequestsQuery query) {
      if (query.RequesterOrgUnitUID.Length == 0) {
        return string.Empty;
      }

      var requesterOrgUnit = OrganizationalUnit.Parse(query.RequesterOrgUnitUID);

      return $"WMS_REQ_REQUESTED_BY_ORG_UNIT_ID = {requesterOrgUnit.Id}";
    }


    static private string BuildRequestsListFilter(RequestsQuery query) {
      if (query.RequestsList.Length == 0) {
        return string.Empty;
      }

      var list = RequestDef.GetList(query.RequestsList);

      string filter = string.Empty;

      foreach (RequestDef requestDef in list) {
        if (filter.Length != 0) {
          filter += ", ";
        }
        filter += requestDef.Id;
      }

      return $"WMS_REQUEST_DEF_ID IN ({filter})";
    }


    static private string BuildRequestDefinitionFilter(RequestsQuery query) {
      if (query.RequestDefUID.Length == 0) {
        return string.Empty;
      }

      var requestType = RequestDef.Parse(query.RequestDefUID);

      return $"WMS_REQUEST_DEF_ID = {requestType.Id}";
    }


    static private string BuildRequestStatusFilter(RequestsQuery query) {
      if (query.RequestStatus == ActivityStatus.All) {
        return $"WMS_REQ_STATUS <> 'X'";
      }

      return $"WMS_REQ_STATUS = '{(char) query.RequestStatus}'";
    }

    #endregion Helpers

  }  // class RequestsQueryExtensions

}  // namespace Empiria.Workflow.Requests.Adapters
