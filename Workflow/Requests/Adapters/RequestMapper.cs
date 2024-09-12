/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adpaters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Mapper                                  *
*  Type     : RequestMapper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Maps Requests instances to their DTOs.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.StateEnums;
using Empiria.Storage;

namespace Empiria.Workflow.Requests.Adapters {

  /// <summary>Maps Requests instances to their DTOs.</summary>
  static internal class RequestMapper {

    static internal FixedList<RequestTypeDto> Map(FixedList<RequestType> requestsTypes) {
      return requestsTypes.Select(x => Map(x)).ToFixedList();
    }


    static internal FixedList<RequestHolderDto> Map(FixedList<Request> requests) {
      return requests.Select(x => Map(x)).ToFixedList();
    }


    static internal RequestHolderDto Map(Request request) {
      return new RequestHolderDto {
        Request = MapRequest(request),
        Tasks = MapRequestTasks(request),
        Actions = MapRequestActions(request),
        Files = MapRequestFiles(request),
        WorkflowHistory = MapRequestWorkflowHistory(request),
      };
    }

    private static FixedList<WorkflowHistoryItemDto> MapRequestWorkflowHistory(Request request) {
      return new FixedList<WorkflowHistoryItemDto>();
    }

    private static FixedList<FileDto> MapRequestFiles(Request request) {
      return new FixedList<FileDto>();
    }

    private static RequestActionsDto MapRequestActions(Request request) {
      return new RequestActionsDto {
        CanActivate = request.CanActivate(),
        CanCancel = request.CanCancel(),
        CanClose = request.CanClose(),
        CanDelete = request.CanDelete(),
        CanStart = request.CanStart(),
        CanSuspend = request.CanSuspend(),
        CanUpdate = request.CanUpdate(),
      };
    }

    private static FixedList<TaskDto> MapRequestTasks(Request request) {
      return new FixedList<TaskDto>();
    }

    private static RequestDto MapRequest(Request request) {
      return new RequestDto {
        UID = request.UID,
        RequestType = Map(request.RequestType),
        UniqueID = request.UniqueID,
        ControlID = request.ControlID,
        RequesterName = request.RequesterName,
        Description = request.Description,
        Notes = request.Notes,
        RequestTypeFields = request.RequestTypeFields,
        RequesterOrgUnit = request.RequesterOrgUnit.MapToNamedEntity(),
        ResponsibleOrgUnit = request.ResponsibleOrgUnit.MapToNamedEntity(),
        FiledBy = request.FiledBy.MapToNamedEntity(),
        FilingTime = request.FilingTime,
        ClosedBy = request.ClosedBy.MapToNamedEntity(),
        ClosingTime = request.ClosingTime,
        PostedBy = request.PostedBy.MapToNamedEntity(),
        PostingTime = request.PostingTime,
        Status = request.Status.GetName(),

      };
    }


    static internal FixedList<RequestDescriptorDto> MapToDescriptor(FixedList<Request> requests) {
      return requests.Select(x => MapToDescriptor(x)).ToFixedList();
    }


    static internal RequestDescriptorDto MapToDescriptor(Request request) {
      return new RequestDescriptorDto {
        UID = request.UID,
        RequestTypeName = request.RequestType.DisplayName,
        UniqueID = request.UniqueID,
        ControlID = request.ControlID,
        RequesterName = request.RequesterName,
        Description = request.Description,
        RequesterOrgUnitName = request.RequesterOrgUnit.FullName,
        ResponsibleOrgUnitName = request.ResponsibleOrgUnit.FullName,
        FiledByName = request.FiledBy.FullName,
        FilingTime = request.FilingTime,
        Status = request.Status.GetName()
      };
    }


    static private RequestTypeDto Map(RequestType requestType) {
      return new RequestTypeDto {
        UID = requestType.UID,
        Name = requestType.DisplayName,
        InputData = requestType.InputData,
      };
    }

  }  // class RequestMapper

}  // namespace Empiria.Workflow.Requests.Adapters
