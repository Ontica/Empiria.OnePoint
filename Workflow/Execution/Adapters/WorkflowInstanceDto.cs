/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Adpaters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Output DTO                              *
*  Type     : WorkflowInstanceDto                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO for a WorkflowInstance object.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.Workflow.Execution.Adapters {

  /// <summary>Output DTO for a WorkflowInstance object.</summary>
  public class WorkflowInstanceDto {

    public string UID {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Description {
      get; internal set;
    }

    public NamedEntityDto RequestedBy {
      get; internal set;
    }

    public NamedEntityDto RequestedByOrgUnit {
      get; internal set;
    }

    public NamedEntityDto ResponsibleOrgUnit {
      get; internal set;
    }

    public NamedEntityDto Priority {
      get; internal set;
    }

    public DateTime DueTime {
      get; internal set;
    }

    public NamedEntityDto StartedBy {
      get; internal set;
    }

    public DateTime StartTime {
      get; internal set;
    }

    public DateTime EndTime {
      get; internal set;
    }

    public NamedEntityDto Status {
      get; internal set;
    }

    public WorkflowActionsDto Actions {
      get; internal set;
    }

    public NamedEntityDto ProcessDef {
      get; internal set;
    }

  }  // class WorkflowInstanceDto

}  // namespace Empiria.Workflow.Execution.Adapters
