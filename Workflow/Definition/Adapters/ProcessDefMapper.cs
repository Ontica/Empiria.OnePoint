/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Mapper                                  *
*  Type     : ProcessDefMapper                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Mapping methods for Process definition instances.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition.Adapters {

  /// <summary>Mapping methods for ProcessType instances.</summary>
  static internal class ProcessDefMapper {

    static internal FixedList<ProcessDefDto> Map(FixedList<ProcessDef> processes) {
      return processes.Select(x => Map(x)).ToFixedList();
    }

    static private ProcessDefDto Map(ProcessDef processType) {
      return new ProcessDefDto {
        UID = processType.UID,
        Name = processType.Name,
        InputData = processType.InputData,
      };
    }

  }  // class ProcessDefMapper

}  // namespace Empiria.Workflow.Definition.Adapters
