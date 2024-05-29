/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Output DTO                              *
*  Type     : ProcessTypeDto                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output data transfer object for ProcessType instances.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition.Adapters {

  /// <summary>Output data transfer object for ProcessType instances.</summary>
  public class ProcessTypeDto {

    public string UID {
      get; internal set;
    }


    public string Name {
      get; internal set;
    }

    public FixedList<DataField> InputData {
      get; internal set;
    }

  }  // class ProcessTypeDto

}  // namespace Empiria.Workflow.Definition.Adapters
