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

    public string TaskNo {
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

    public NamedEntityDto AssignedTo {
      get; internal set;
    }

    public NamedEntityDto AssignedToOrgUnit {
      get; internal set;
    }

    public DateTime Deadline {
      get; internal set;
    }

    public DateTime CheckInTime {
      get; internal set;
    }

    public DateTime EndTime {
      get; internal set;
    }

    public DateTime CheckOutTime {
      get; internal set;
    }

    public string Status {
      get; internal set;
    }

    public WorkflowActionsDto Actions {
      get; internal set;
    }

    public TaskInvokerDto TaskInvoker {
      get; internal set;
    }

  }  // class TaskDto


  /// <summary>Inner output DTO that describes a task invoker.</summary>
  public class TaskInvokerDto {

    public string UID {
      get; internal set;
    }

    public string WorkflowInstanceUID {
      get; internal set;
    }

  }  // public class TaskInvokerDto

}  // namespace Empiria.Workflow.Execution.Adapters
