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

using Empiria.Workflow.Definition.Adapters;
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

    public FixedList<WorkflowStepDto> Steps {
      get; internal set;
    }

    public FixedList<FileDto> Documents {
      get; internal set;
    }

    public FixedList<WorkflowHistoryItemDto> History {
      get; internal set;
    }

    // ToDo: To be removed

    public FixedList<WorkflowStepDto> Tasks {
      get; internal set;
    }

  }  // RequestHolderDto


  /// <summary>Output DTO that holds full information about a request.</summary>
  public class RequestDto {

    public string UID {
      get; internal set;
    }

    public string RequestNo {
      get; internal set;
    }

    public string InternalControlNo {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Description {
      get; internal set;
    }

    public NamedEntityDto RequestedBy {
      get; internal set;
    }

    public NamedEntityDto RequestedByOrgUnit {
      get; internal set;
    }

    public NamedEntityDto ResponsibleOrgUnit {
      get; internal set;
    }

    public NamedEntityDto Priority {
      get; internal set;
    }

    public DateTime DueTime {
      get; internal set;
    }

    public NamedEntityDto StartedBy {
      get; internal set;
    }

    public DateTime StartTime {
      get; internal set;
    }

    public DateTime EndTime {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }

    public FixedList<FieldValue> Fields {
      get; internal set;
    }

    public WorkflowActionsDto Actions {
      get; internal set;
    }

    public RequestDefDto RequestDef {
      get; internal set;
    }

    // ToDo: To be removed

    public RequestDefDto RequestType {
      get; internal set;
    }

  }  // class RequestDto



  /// <summary>Output DTO for Request instances for use in lists.</summary>
  public class RequestListItemDto {

    public string UID {
      get; internal set;
    }

    public string RequestNo {
      get; internal set;
    }

    public string InternalControlNo {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Description {
      get; internal set;
    }

    public string RequestedBy {
      get; internal set;
    }

    public string RequestedByOrgUnit {
      get; internal set;
    }

    public string ResponsibleOrgUnit {
      get; internal set;
    }

    public string Priority {
      get; internal set;
    }

    public DateTime DueTime {
      get; internal set;
    }

    public string StartedBy {
      get; internal set;
    }

    public DateTime StartTime {
      get; internal set;
    }

    public DateTime EndTime {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }

  }  // class RequestDescriptorDto

}  // namespace Empiria.Workflow.Requests.Adapters
