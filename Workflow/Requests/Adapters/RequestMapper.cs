/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Mapper                                  *
*  Type     : RequestMapper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Maps Requests instances to their DTOs.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.StateEnums;
using Empiria.Storage;

using Empiria.Workflow.Execution.Adapters;

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
        WorkflowInstances = WorkflowInstanceMapper.Map(request.WorkflowInstances),
        Tasks = WorkflowTaskMapper.Map(request.GetTasks()),
        Documents = MapDocuments(request),
        History = MapHistory(request),

        // ToDo: To be removed

        Actions = request.Actions.MapToDto()
      };
    }

    static internal FixedList<RequestListItemDto> MapToListItems(FixedList<Request> requests) {
      return requests.Select(x => MapToListItem(x)).ToFixedList();
    }

    #region Helpers

    static private FixedList<FileDto> MapDocuments(Request request) {
      return new FixedList<FileDto>();
    }


    static private FixedList<WorkflowHistoryItemDto> MapHistory(Request request) {
      return new FixedList<WorkflowHistoryItemDto>();
    }


    static private RequestDto MapRequest(Request request) {
      return new RequestDto {
        UID = request.UID,
        RequestNo = request.RequestNo,
        InternalControlNo = request.InternalControlNo,
        Name = request.Name,
        Description = request.Description,
        RequestedBy = request.RequestedBy.MapToNamedEntity(),
        RequestedByOrgUnit = request.RequestedByOrgUnit.MapToNamedEntity(),
        ResponsibleOrgUnit = request.ResponsibleOrgUnit.MapToNamedEntity(),
        Priority = request.Priority.MapToDto(),
        DueTime = request.DueTime,
        StartedBy = request.StartedBy.MapToNamedEntity(),
        StartTime = request.StartTime,
        EndTime = request.EndTime,
        Status = request.Status.GetName(),
        Fields = request.RequestTypeFields,
        Actions = request.Actions.MapToDto(),
        RequestType = Map(request.RequestType),

        // ToDo: To be removed

        UniqueID = request.RequestNo,
        ControlID = request.InternalControlNo,
        RequesterOrgUnit = request.RequestedByOrgUnit.MapToNamedEntity(),
        RequesterName = request.RequestedBy.Name,
        FiledBy = request.StartedBy.MapToNamedEntity(),
        FilingTime = request.StartTime,
        ClosingTime = request.EndTime,
        RequestTypeFields = request.RequestTypeFields
      };
    }


    static private RequestListItemDto MapToListItem(Request request) {
      return new RequestListItemDto {
        UID = request.UID,
        RequestNo = request.RequestNo,
        InternalControlNo = request.InternalControlNo,
        Name = request.Name,
        Description = request.Description,
        RequestedBy = request.RequestedBy.Name,
        RequestedByOrgUnit = request.RequestedByOrgUnit.Name,
        ResponsibleOrgUnit = request.ResponsibleOrgUnit.Name,
        Priority = request.Priority.GetName(),
        DueTime = request.DueTime,
        StartedBy = request.StartedBy.Name,
        StartTime = request.StartTime,
        EndTime = request.EndTime,
        Status = request.Status.GetName(),

        // ToDo: To be removed

        UniqueID = request.RequestNo,
        ControlID = request.InternalControlNo,
        RequestTypeName = request.RequestType.DisplayName,
        RequesterName = request.RequestedBy.Name,
        RequesterOrgUnitName = request.RequestedByOrgUnit.Name,
        ResponsibleOrgUnitName = request.ResponsibleOrgUnit.Name,
        FiledByName = request.StartedBy.Name,
        FilingTime = request.EndTime,
      };
    }


    static private RequestTypeDto Map(RequestType requestType) {
      return new RequestTypeDto {
        UID = requestType.UID,
        Name = requestType.DisplayName,
        Description = requestType.Documentation,
        ResponsibleOrgUnit = requestType.ResponsibleOrgUnit.MapToNamedEntity(),
        InputData = requestType.InputData.Select(x => x.MapToDto())
                                         .ToFixedList(),
      };
    }

    #endregion Helpers

  }  // class RequestMapper

}  // namespace Empiria.Workflow.Requests.Adapters
