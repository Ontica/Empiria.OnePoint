/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Test cases                              *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll        Pattern   : Unit tests                              *
*  Type     : WorkflowObjectTests                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for workflow objects.                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using Empiria.Workflow.Definition;

namespace Empiria.Tests.Workflow.Definition {

  /// <summary>Test cases for workflow model items.</summary>
  public class WorkflowObjectTests {

    #region Facts

    [Fact]
    public void Should_Parse_All_Workflow_Objects() {
      var sut = BaseObject.GetList<WorkflowObject>();

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public void Should_Parse_Empty_Workflow_Object() {
      var sut = WorkflowObject.Empty;

      Assert.NotNull(sut);
    }

    #endregion Facts

  }  // class WorkflowObjectTests

}  // namespace Empiria.Tests.Workflow.Definition
