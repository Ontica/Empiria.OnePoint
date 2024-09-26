/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Output DTO                              *
*  Type     : RequestDefDto                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output data transfer object for RequestDef instances.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.DataObjects;

namespace Empiria.Workflow.Definition.Adapters {

  /// <summary>Output data transfer object for RequestDef instances.</summary>
  public class RequestDefDto {

    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Description {
      get; internal set;
    }

    public NamedEntityDto ResponsibleOrgUnit {
      get; internal set;
    }

    public FixedList<DataFieldDto> InputData {
      get; internal set;
    }

  }  // class RequestDefDto

}  // namespace Empiria.Workflow.Definition.Adapters
