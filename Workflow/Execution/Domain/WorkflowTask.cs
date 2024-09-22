/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowTask                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds information about a workflow instance task.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Empiria.Parties;
using Empiria.StateEnums;

using Empiria.Workflow.Definition;

namespace Empiria.Workflow.Execution {

  /// <summary>Holds information about a workflow instance task.</summary>
  public class WorkflowTask {

    private readonly WorkflowStep _step;

    internal WorkflowTask(WorkflowStep step) {
      _step = step;
    }

    #region Properties

    public int Id {
      get {
        return _step.Id;
      }

    }

    public string UID {
      get {
        return _step.UID;
      }
    }


    public string TaskNo {
      get {
        return _step.No;
      }
    }


    public WorkflowInstance WorkflowInstance {
      get {
        return _step.WorkflowInstance;
      }
    }


    public WorkflowObject WorkflowObject {
      get {
        return _step.WorkflowModelItem.TargetObject;
      }
    }

    public string Name {
      get {
        if (_step.WorkflowModelItem.Name.Length != 0) {
          return _step.WorkflowModelItem.Name;
        }
        return _step.WorkflowModelItem.TargetObject.Name;
      }
    }


    public string Description {
      get {
        return _step.Description;
      }
    }

    public Party RequestedBy {
      get {
        return _step.RequestedBy;
      }
    }


    public OrganizationalUnit RequestedByOrgUnit {
      get {
        return _step.RequestedByOrgUnit;
      }
    }


    public Party AssignedTo {
      get {
        return _step.AssignedTo;
      }
    }


    public OrganizationalUnit AssignedToOrgUnit {
      get {
        return _step.AssignedToOrgUnit;
      }
    }


    public DateTime Deadline {
      get {
        return _step.Deadline;
      }
    }


    public DateTime CheckInTime {
      get {
        return _step.CheckInTime;
      }
    }


    public DateTime EndTime {
      get {
        return _step.EndTime;
      }
    }


    public DateTime CheckOutTime {
      get {
        return _step.CheckOutTime;
      }
    }


    public ActivityStatus Status {
      get {
        return _step.RuntimeStatus;
      }
    }

    #endregion Properties

  }  // class WorkflowTask

} // namespace Empiria.Workflow.Execution
