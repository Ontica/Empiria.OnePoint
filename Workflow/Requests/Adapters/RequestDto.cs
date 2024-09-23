/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Output DTO                              *
*  Type     : RequestDto                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO for Request instances.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Empiria.DataObjects;
using Empiria.Storage;

using Empiria.Workflow.Execution.Adapters;

namespace Empiria.Workflow.Requests.Adapters {

  /// <summary>Output DTO that holds a request with its actions, tasks, files and workflow history.</summary>
  public class RequestHolderDto {

    public RequestDto Request {
      get; internal set;
    }

    public FixedList<WorkflowInstanceDto> WorkflowInstances {
      get; internal set;
    }

    public WorkflowActionsDto Actions {
      get; internal set;
    }

    public FixedList<TaskDto> Tasks {
      get; internal set;
    }

    public FixedList<FileDto> Files {
      get; internal set;
    }

    public FixedList<WorkflowHistoryItemDto> WorkflowHistory {
      get; internal set;
    }

  }  // RequestHolderDto


  /// <summary>Output DTO that holds full information about a request.</summary>
  public class RequestDto {

    public string UID {
      get; internal set;
    }

    public RequestTypeDto RequestType {
      get; internal set;
    }

    public string UniqueID {
      get; internal set;
    }

    public string ControlID {
      get; internal set;
    }

    public string RequesterName {
      get; internal set;
    }

    public string Description {
      get; internal set;
    }

    public FixedList<FieldValue> RequestTypeFields {
      get; internal set;
    }

    public NamedEntityDto RequesterOrgUnit {
      get; internal set;
    }

    public NamedEntityDto ResponsibleOrgUnit {
      get; internal set;
    }

    [Newtonsoft.Json.JsonPropertyAttribute(PropertyName = "FiledBy")]
    public NamedEntityDto StartedBy {
      get; internal set;
    }

    [Newtonsoft.Json.JsonPropertyAttribute(PropertyName = "FilingTime")]
    public DateTime StartTime {
      get; internal set;
    }

    [Newtonsoft.Json.JsonPropertyAttribute(PropertyName = "ClosedBy")]
    public NamedEntityDto EndedBy {
      get; internal set;
    }

    [Newtonsoft.Json.JsonPropertyAttribute(PropertyName = "ClosingTime")]
    public DateTime EndTime {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }

  }  // class RequestDto



  /// <summary>Output DTO for Request instances for use in lists.</summary>
  public class RequestDescriptorDto {

    public string UID {
      get; internal set;
    }

    public string RequestTypeName {
      get; internal set;
    }

    public string UniqueID {
      get; internal set;
    }

    public string ControlID {
      get; internal set;
    }

    public string RequesterName {
      get; internal set;
    }

    public string Description {
      get; internal set;
    }

    public string RequesterOrgUnitName {
      get; internal set;
    }

    public string ResponsibleOrgUnitName {
      get; internal set;
    }

    [Newtonsoft.Json.JsonPropertyAttribute(PropertyName = "FiledByName")]
    public string StartedByName {
      get; internal set;
    }

    [Newtonsoft.Json.JsonPropertyAttribute(PropertyName = "FilingTime")]
    public DateTime StartTime {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }

  }  // class RequestDescriptorDto

}  // namespace Empiria.Workflow.Requests.Adapters
