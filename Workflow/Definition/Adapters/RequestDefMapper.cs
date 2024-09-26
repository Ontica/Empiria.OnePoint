/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Mapper                                  *
*  Type     : RequestDefMapper                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Maps RequestDef instances to their DTOs.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition.Adapters {

  /// <summary>Maps RequestDef instances to their DTOs.</summary>
  static internal class RequestDefMapper {

    static internal FixedList<RequestDefDto> Map(FixedList<RequestDef> list) {
      return list.Select(x => Map(x)).ToFixedList();
    }


    static internal RequestDefDto Map(RequestDef requestDef) {
      return new RequestDefDto {
        UID = requestDef.UID,
        Name = requestDef.Name,
        Description = requestDef.Description,
        ResponsibleOrgUnit = requestDef.ResponsibleOrgUnit.MapToNamedEntity(),
        InputData = requestDef.InputData.Select(x => x.MapToDto())
                                        .ToFixedList(),
      };
    }

  }  // class RequestDefMapper

}  // namespace Empiria.Workflow.Definition.Adapters
