/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Query DTO                               *
*  Type     : RequestsQuery                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO used for search requests.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Empiria.StateEnums;

namespace Empiria.Workflow.Requests.Adapters {

  public enum DateSearchField {

    None,

    DueTime,

    StartTime,

    EndTime,

  }


  /// <summary>Input DTO used for search requests.</summary>
  public class RequestsQuery {

    public string RequestsList {
      get; set;
    } = string.Empty;


    [Newtonsoft.Json.JsonProperty(PropertyName = "RequestTypeUID")]
    public string RequestDefUID {
      get; set;
    } = string.Empty;


    public string RequesterOrgUnitUID {
      get; set;
    } = string.Empty;


    public ActivityStatus RequestStatus {
      get; set;
    } = ActivityStatus.All;


    public DateSearchField DateSearchField {
      get; set;
    } = DateSearchField.None;


    public DateTime FromDate {
      get; set;
    } = ExecutionServer.DateMinValue;


    public DateTime ToDate {
      get; set;
    } = ExecutionServer.DateMaxValue;


    public object[] RequestFields {
      get; set;
    } = new object[0];


    public string OrderBy {
      get; set;
    } = string.Empty;

  }  // class RequestsQuery

}  // namespace Empiria.Workflow.Requests.Adapters
