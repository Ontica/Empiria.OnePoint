/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adpaters Layer                          *
*  Assembly : Empiria.OnePoint.Requests.dll              Pattern   : Mapper                                  *
*  Type     : RequestMapper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Maps Requests instances to their DTOs.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

namespace Empiria.OnePoint.Requests.Adapters {

  static internal class RequestMapper {

    static internal FixedList<RequestTypeDto> Map(FixedList<RequestType> requestsTypes) {
      return requestsTypes.Select(x => Map(x)).ToFixedList();
    }


    static internal RequestDto Map(Request request) {
      return new RequestDto {
        UID = request.UID,
        RequestType = new NamedEntityDto(request.RequestType.Name, request.RequestType.DisplayName),
        UniqueID = request.UniqueID,
        ControlID = request.ControlID,
        RequesterName = request.RequesterName,
        RequesterOrgUnit = request.RequesterOrgUnit.MapToNamedEntity(),
        ResponsibleOrgUnit = request.ResponsibleOrgUnit.MapToNamedEntity(),
        Notes = request.Notes,
        FiledBy = request.FiledBy.MapToNamedEntity(),
        FilingTime = request.FilingTime,
        ClosedBy = request.ClosedBy.MapToNamedEntity(),
        ClosingTime = request.ClosingTime,
        PostedBy = request.PostedBy.MapToNamedEntity(),
        PostingTime = request.PostingTime,
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

}  // namespace Empiria.OnePoint.Requests.Adapters
