/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Test cases                              *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll        Pattern   : Unit tests                              *
*  Type     : WorkflowStepsTests                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for workflow steps.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

using Xunit;

using Empiria.Workflow.Execution;
using Empiria.Workflow.Execution.Adapters;

using Empiria.Workflow.Requests;

namespace Empiria.Tests.Workflow.Execution {

  /// <summary>Test cases for workflow steps.</summary>
  public class WorkflowStepsTests {

    #region Facts

    [Fact]
    public void Should_Parse_All_Workflow_Steps() {
      var sut = BaseObject.GetList<WorkflowStep>();

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public void Should_Parse_Empty_Workflow_Step() {
      var sut = WorkflowStep.Empty;

      Assert.NotNull(sut);
    }


    [Fact]
    public void Should_Add_Workflow_Step() {
      var request = Request.Parse(1);

      var fields = new WorkflowStepFields {
        Description = "  Esta es  la  nueva   descripción  ",
        DueTime = DateTime.Today.AddDays(4).Date,
        Priority = StateEnums.Priority.High,
        AssignedToOrgUnitUID = "Empty",
        AssignedToUID = "Empty",
        RequestedByOrgUnitUID = "Empty",
        RequestedByUID = "Empty",
        RequestUID = request.UID,
        WorkflowInstanceUID = request.WorkflowInstances[0].UID,
        WorkflowModelItemUID = "2effa02d-8de4-4989-addd-6e2ad87cab6b"
      };

      WorkflowStep sut = request.AddStep(fields);

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
    }


    [Fact]
    public void Should_Update_Workflow_Step() {
      var sut = WorkflowStep.Parse(2);

      var fields = new WorkflowStepFields {
        Description = "  Esta es  la  nueva   descripción  ",
        DueTime = DateTime.Today.AddDays(4).Date,
        Priority = StateEnums.Priority.High,
        AssignedToOrgUnitUID = "Empty",
        AssignedToUID = "Empty",
        RequestedByOrgUnitUID = "Empty",
        RequestedByUID = "Empty",
        RequestUID = sut.Request.UID,
        WorkflowInstanceUID = sut.WorkflowInstance.UID,
        WorkflowModelItemUID = sut.WorkflowModelItem.UID
      };

      sut.Update(fields);

      Assert.Equal(EmpiriaString.TrimAll(fields.Description), sut.Description);
      Assert.Equal(fields.DueTime, sut.DueTime);
      Assert.Equal(fields.Priority, sut.Priority);
      Assert.Equal(fields.AssignedToOrgUnitUID, sut.AssignedToOrgUnit.UID);
      Assert.Equal(fields.AssignedToUID, sut.AssignedTo.UID);
      Assert.Equal(fields.RequestedByOrgUnitUID, sut.RequestedByOrgUnit.UID);
      Assert.Equal(fields.RequestedByUID, sut.RequestedBy.UID);
      Assert.Equal(fields.RequestUID, sut.Request.UID);
      Assert.Equal(fields.WorkflowInstanceUID, sut.WorkflowInstance.UID);
    }

    #endregion Facts

  }  // class WorkflowStepsTests

}  // namespace Empiria.Tests.Workflow.Execution
