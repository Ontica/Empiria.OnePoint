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
    public void Should_Update_Workflow_Step() {
      var sut = WorkflowStep.Parse(2);

      var fields = new WorkflowStepFields {
        Description = "  Esta es  la  nueva   descripción  ",
        DueTime = DateTime.Today.AddDays(4).Date,
        Priority = StateEnums.Priority.High,
        AssignedToOrgUnitUID = "Empty",
        AssignedToUID = "Empty",
        RequestedByOrgUnitUID = "Empty",
        RequestedByUID = "Empty"
      };

      sut.Update(fields);

      Assert.Equal(EmpiriaString.TrimAll(fields.Description), sut.Description);
      Assert.Equal(fields.DueTime, sut.DueTime);
      Assert.Equal(fields.Priority, sut.Priority);
      Assert.Equal(fields.AssignedToOrgUnitUID, sut.AssignedToOrgUnit.UID);
      Assert.Equal(fields.AssignedToUID, sut.AssignedTo.UID);
      Assert.Equal(fields.RequestedByOrgUnitUID, sut.RequestedByOrgUnit.UID);
      Assert.Equal(fields.RequestedByUID, sut.RequestedBy.UID);
    }

    #endregion Facts

  }  // class WorkflowStepsTests

}  // namespace Empiria.Tests.Workflow.Execution
