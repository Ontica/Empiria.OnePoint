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

namespace Empiria.Workflow.Requests.Adapters {

  /// <summary>Maps Requests instances to their DTOs.</summary>
  static internal class RequestMapper {

    static internal FixedList<RequestTypeDto> Map(FixedList<RequestType> requestsTypes) {
      return requestsTypes.Select(x => Map(x)).ToFixedList();
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
