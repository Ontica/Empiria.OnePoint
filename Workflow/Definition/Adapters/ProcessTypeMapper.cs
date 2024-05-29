/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Mapper                                  *
*  Type     : ProcessTypeMapper                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Mapping methods for ProcessType instances.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Workflow.Definition.Adapters {

  /// <summary>Mapping methods for ProcessType instances.</summary>
  static internal class ProcessTypeMapper {

    static internal FixedList<ProcessTypeDto> Map(FixedList<ProcessType> processes) {
      return processes.Select(x => Map(x)).ToFixedList();
    }

    static private ProcessTypeDto Map(ProcessType processType) {
      return new ProcessTypeDto {
        UID = processType.UID,
        Name = processType.Name,
        InputData = processType.InputData,
      };
    }

  }  // class ProcessTypeMapper

}  // namespace Empiria.Workflow.Definition.Adapters
