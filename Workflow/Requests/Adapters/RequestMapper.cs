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
        Actions = request.Actions.MapToDto(),
        Files = MapRequestFiles(request),
        WorkflowHistory = MapRequestWorkflowHistory(request),
      };
    }

    static internal FixedList<RequestDescriptorDto> MapToDescriptor(FixedList<Request> requests) {
      return requests.Select(x => MapToDescriptor(x)).ToFixedList();
    }

    #region Helpers

    static private FixedList<WorkflowHistoryItemDto> MapRequestWorkflowHistory(Request request) {
      return new FixedList<WorkflowHistoryItemDto>();
    }


    static private FixedList<FileDto> MapRequestFiles(Request request) {
      return new FixedList<FileDto>();
    }


    static private RequestDto MapRequest(Request request) {
      return new RequestDto {
        UID = request.UID,
        RequestType = Map(request.RequestType),
        UniqueID = request.UniqueID,
        ControlID = request.ControlID,
        RequesterName = request.RequesterName,
        Description = request.Description,
        RequestTypeFields = request.RequestTypeFields,
        RequesterOrgUnit = request.RequesterOrgUnit.MapToNamedEntity(),
        ResponsibleOrgUnit = request.ResponsibleOrgUnit.MapToNamedEntity(),
        StartedBy = request.StartedBy.MapToNamedEntity(),
        StartTime = request.StartTime,
        EndedBy = request.EndedBy.MapToNamedEntity(),
        EndTime = request.EndTime,
        Status = request.Status.GetName()
      };
    }


    static private RequestDescriptorDto MapToDescriptor(Request request) {
      return new RequestDescriptorDto {
        UID = request.UID,
        RequestTypeName = request.RequestType.DisplayName,
        UniqueID = request.UniqueID,
        ControlID = request.ControlID,
        RequesterName = request.RequesterName,
        Description = request.Description,
        RequesterOrgUnitName = request.RequesterOrgUnit.FullName,
        ResponsibleOrgUnitName = request.ResponsibleOrgUnit.FullName,
        StartedByName = request.StartedBy.FullName,
        StartTime = request.StartTime,
        Status = request.Status.GetName()
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
