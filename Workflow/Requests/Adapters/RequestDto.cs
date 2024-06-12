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

  /// <summary>Output DTO for Request instances.</summary>
  public class RequestDto {

    public string UID {
      get; internal set;
    }

    public NamedEntityDto RequestType {
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

    public string Notes {
      get; internal set;
    }

    public NamedEntityDto RequesterOrgUnit {
      get; internal set;
    }

    public NamedEntityDto ResponsibleOrgUnit {
      get; internal set;
    }

    public NamedEntityDto FiledBy {
      get; internal set;
    }

    public DateTime FilingTime {
      get; internal set;
    }

    public NamedEntityDto ClosedBy {
      get; internal set;
    }

    public DateTime ClosingTime {
      get; internal set;
    }

    public NamedEntityDto PostedBy {
      get; internal set;
    }

    public DateTime PostingTime {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }

  }  // class RequestDto

}  // namespace Empiria.Workflow.Requests.Adapters
