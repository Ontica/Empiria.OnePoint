/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Adpaters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Output DTO                              *
*  Type     : WorkflowStepDto                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO for a WorkflowStep instances.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace Empiria.Workflow.Execution.Adapters {

  /// <summary>Output DTO for a WorkflowStep instances.</summary>
  public class WorkflowStepDto {

    public string UID {
      get; internal set;
    }

    public string StepNo {
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

    public NamedEntityDto Priority {
      get; internal set;
    }

    public DateTime DueTime {
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

    public NamedEntityDto WorkflowModelItem {
      get; internal set;
    }

    public NamedEntityDto Request {
      get; internal set;
    }

    public NamedEntityDto WorkflowInstance {
      get; internal set;
    }

    public WorkflowActionsDto Actions {
      get; internal set;
    }

    public WorkflowStepInvokerDto StepInvoker {
      get; internal set;
    }


    // ToDo: To be removed

    public string TaskNo {
      get; internal set;
    }

  }  // class WorkflowStepDto



  /// <summary>Inner output DTO that describes a WorkflowStep invoker.</summary>
  public class WorkflowStepInvokerDto {

    public string UID {
      get; internal set;
    }

    public string WorkflowInstanceUID {
      get; internal set;
    }

  }  // public class WorkflowStepInvokerDto

}  // namespace Empiria.Workflow.Execution.Adapters
