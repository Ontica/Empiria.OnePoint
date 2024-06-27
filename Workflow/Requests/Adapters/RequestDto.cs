/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adpaters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Output DTO                              *
*  Type     : RequestDto                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO for Request instances.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Empiria.Storage;

namespace Empiria.Workflow.Requests.Adapters {

  /// <summary>Output DTO that holds a request with its actions, tasks, files and workflow history.</summary>
  public class RequestHolderDto {

    public RequestDto Request {
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

    public RequestActionsDto Actions {
      get; internal set;
    }

  }  // RequestHolderDto


  /// <summary>Output DTO that holds full information about a request.</summary>
  public class RequestDto {

    public string UID {
      get; internal set;
    }

    public NamedEntityDto RequestType {
      get; internal set;
    }

    public string UniqueID {
      get; internal set;
    }

    public string ControlID {
      get; internal set;
    }

    public string Requester {
      get; internal set;
    }

    public string Description {
      get; internal set;
    }

    public string Notes {
      get; internal set;
    }

    public NamedEntityDto RequesterOrgUnit {
      get; internal set;
    }

    public NamedEntityDto ResponsibleOrgUnit {
      get; internal set;
    }

    public NamedEntityDto FiledBy {
      get; internal set;
    }

    public DateTime FilingTime {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }

  }  // class RequestDto



  /// <summary>Inner output DTO with a request actions flags.</summary>
  public class RequestActionsDto {

    public bool CanActivate {
      get; internal set;
    }

    public bool CanCancel {
      get; internal set;
    }

    public bool CanClose {
      get; internal set;
    }

    public bool CanDelete {
      get; internal set;
    }

    public bool CanReject {
      get; internal set;
    }

    public bool CanStart {
      get; internal set;
    }

    public bool CanSuspend {
      get; internal set;
    }

    public bool CanUpdate {
      get; internal set;
    }

  }  // class RequestActionsDto



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

    public string FiledByName {
      get; internal set;
    }

    public DateTime FilingTime {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }

  }  // class RequestDescriptorDto

}  // namespace Empiria.Workflow.Requests.Adapters
