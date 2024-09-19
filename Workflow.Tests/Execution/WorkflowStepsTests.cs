/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Test cases                              *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll        Pattern   : Unit tests                              *
*  Type     : WorkflowStepsTests                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for workflow steps.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using Empiria.Workflow.Execution;

namespace Empiria.Tests.Workflow.Execution {

  /// <summary>Test cases for workflow steps.</summary>
  public class WorkflowStepsTests {

    #region Facts

    [Fact]
    public void Should_Parse_Empty_Workflow_Step() {
      var sut = WorkflowStep.Empty;

      Assert.NotNull(sut);
    }


    [Fact]
    public void Should_Parse_All_Workflow_Steps() {
      var sut = WorkflowStep.GetList<WorkflowStep>();

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }

    #endregion Facts

  }  // class WorkflowStepsTests

}  // namespace Empiria.Tests.Workflow.Execution
