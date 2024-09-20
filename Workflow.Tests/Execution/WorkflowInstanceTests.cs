/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Test cases                              *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll        Pattern   : Unit tests                              *
*  Type     : WorkflowInstanceTests                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for workflow instances.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using Empiria.Workflow.Execution;

namespace Empiria.Tests.Workflow.Execution {

  /// <summary>Test cases for workflow instances.</summary>
  public class WorkflowInstanceTests {

    #region Facts

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
    public void Should_Parse_All_Workflow_Instances_Tasks() {
      var workflowInstances = BaseObject.GetList<WorkflowInstance>();

      foreach (var workflowInstance in workflowInstances) {
        FixedList<WorkflowTask> sut = workflowInstance.GetTasks();

        Assert.NotNull(sut);

        if (workflowInstance.IsStarted) {
          Assert.NotEmpty(sut);
        }
      }
    }

    #endregion Facts

  }  // class WorkflowInstanceTests

}  // namespace Empiria.Tests.Workflow.Execution
