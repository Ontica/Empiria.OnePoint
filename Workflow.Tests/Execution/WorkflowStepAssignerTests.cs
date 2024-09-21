/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Test cases                              *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll        Pattern   : Unit tests                              *
*  Type     : WorkflowStepAssigner                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for the workflow step assigner.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using Empiria.Workflow.Execution;
using Empiria.Workflow.Definition;

namespace Empiria.Tests.Workflow.Execution {

  /// <summary>Test cases for the workflow step assigner.</summary>
  public class WorkflowStepAssignerTests {

    #region Facts

    [Fact]
    public void Should_Parse_Assignation_Rules_For_All_Workflow_Steps() {
      var steps = BaseObject.GetList<WorkflowStep>();

      foreach (var step in steps) {
        var assigner = new WorkflowStepAssigner(step);

        AssignationRules sut = assigner.AssignationRules;

        Assert.NotNull(assigner);
      }
    }


    [Fact]
    public void Should_Parse_Assignation_Rules_For_Empty_Workflow_Step() {
      var assigner = new WorkflowStepAssigner(WorkflowStep.Empty);

      AssignationRules sut = assigner.AssignationRules;

      Assert.NotNull(sut);
    }

    #endregion Facts

  }  // class WorkflowStepAssigner

}  // namespace Empiria.Tests.Workflow.Execution
