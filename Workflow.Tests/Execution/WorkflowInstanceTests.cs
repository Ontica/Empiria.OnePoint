/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Test cases                              *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll        Pattern   : Unit tests                              *
*  Type     : WorkflowInstanceTests                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for workflow instances.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Xunit;

using Empiria.Workflow.Execution;
using Empiria.Workflow.Execution.Adapters;

namespace Empiria.Tests.Workflow.Execution {

  /// <summary>Test cases for workflow instances.</summary>
  public class WorkflowInstanceTests {

    #region Facts

    [Fact]
    public void Should_Insert_Workflow_Step() {
      var workflowInstance = WorkflowInstance.Parse(2);

      var fields = new WorkflowStepFields {
        Description = "  Esta es  la  nueva   descripción  ",
        DueTime = DateTime.Today.AddDays(4).Date,
        Priority = StateEnums.Priority.High,
        AssignedToOrgUnitUID = "Empty",
        AssignedToUID = "Empty",
        RequestedByOrgUnitUID = "Empty",
        RequestedByUID = "Empty",
        RequestUID = workflowInstance.Request.UID,
        WorkflowInstanceUID = workflowInstance.UID,
        WorkflowModelItemUID = "2effa02d-8de4-4989-addd-6e2ad87cab6b"
      };

      var countSut = workflowInstance.GetSteps().Count;

      WorkflowInstanceEngine engine = workflowInstance.GetEngine();

      WorkflowStep sut = engine.CreateStep(fields.GetWorkflowModelItem(), fields.GetPreviousStep());

      sut.Update(fields);

      engine.InsertStep(sut);

      Assert.Equal(EmpiriaString.TrimAll(fields.Description), sut.Description);
      Assert.Equal(fields.DueTime, sut.DueTime);
      Assert.Equal(fields.Priority, sut.Priority);
      Assert.Equal(fields.AssignedToOrgUnitUID, sut.AssignedToOrgUnit.UID);
      Assert.Equal(fields.AssignedToUID, sut.AssignedTo.UID);
      Assert.Equal(fields.RequestedByOrgUnitUID, sut.RequestedByOrgUnit.UID);
      Assert.Equal(fields.RequestedByUID, sut.RequestedBy.UID);
      Assert.Equal(fields.RequestUID, sut.Request.UID);
      Assert.Equal(fields.WorkflowInstanceUID, sut.WorkflowInstance.UID);
      Assert.Equal(fields.WorkflowModelItemUID, sut.WorkflowModelItem.UID);

      Assert.Equal(countSut + 1, workflowInstance.GetSteps().Count);
    }


    [Fact]
    public void Should_Parse_All_Workflow_Instances() {
      var sut = BaseObject.GetList<WorkflowInstance>();

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public void Should_Parse_Empty_Workflow_Instance() {
      var sut = WorkflowInstance.Empty;

      Assert.NotNull(sut);
    }


    [Fact]
    public void Should_Parse_All_Workflow_Instances_Steps() {
      var workflowInstances = BaseObject.GetList<WorkflowInstance>();

      foreach (var workflowInstance in workflowInstances) {
        FixedList<WorkflowStep> sut = workflowInstance.GetSteps();

        Assert.NotNull(sut);

        if (workflowInstance.IsStarted) {
          Assert.NotEmpty(sut);
        }
      }
    }

    #endregion Facts

  }  // class WorkflowInstanceTests

}  // namespace Empiria.Tests.Workflow.Execution
