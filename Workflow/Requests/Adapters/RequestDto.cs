/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adpaters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Output DTO                              *
*  Type     : RequestDto                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO for Request instances.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.Workflow.Requests.Adapters {

  /// <summary>Output DTO for Request instances for use in lists.</summary>
  public class RequestDescriptorDto {

    public string UID {
      get; internal set;
    }

    public string RequestTypeName {
      get; internal set;
    }

    public string UniqueID {
      get; internal set;
    }

    public string ControlID {
      get; internal set;
    }

    public string RequesterName {
      get; internal set;
    }

    public string Description {
      get; internal set;
    }

    public string RequesterOrgUnitName {
      get; internal set;
    }

    public string ResponsibleOrgUnitName {
      get; internal set;
    }

    public string FiledByName {
      get; internal set;
    }

    public DateTime FilingTime {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }

  }  // class RequestDescriptorDto

}  // namespace Empiria.Workflow.Requests.Adapters
