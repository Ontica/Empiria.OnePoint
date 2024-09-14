/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Adpaters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Output DTO                              *
*  Type     : TaskDto                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO for a Workflow Task instance.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.Workflow.Execution.Adapters {

  /// <summary>Output DTO for a Workflow Task instance.</summary>
  public class TaskDto {

    public string UID {
      get; internal set;
    }

    public NamedEntity Type {
      get; internal set;
    }

    public string Name {
      get; internal set;
    }

    public string Description {
      get; internal set;
    }

    public string Notes {
      get; internal set;
    }

    public NamedEntity WorkflowInstance {
      get; internal set;
    }

    public DateTime StartDate {
      get; internal set;
    }

    public DateTime EndDate {
      get; internal set;
    }

    public DateTime Deadline {
      get; internal set;
    }

    public DateTime PlannedEndDate {
      get; internal set;
    }

    public NamedEntity AssignedTo {
      get; internal set;
    }

    public NamedEntity AssignedToOrgUnit {
      get; internal set;
    }

    public NamedEntity RequestedBy {
      get; internal set;
    }

    public NamedEntity RequestedByOrgUnit {
      get; internal set;
    }

    public NamedEntity Status {
      get; internal set;
    }

  }  // class TaskDto

}  // namespace Empiria.Workflow.Execution.Adapters
