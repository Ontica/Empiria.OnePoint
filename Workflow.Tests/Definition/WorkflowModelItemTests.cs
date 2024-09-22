/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Test cases                              *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll        Pattern   : Unit tests                              *
*  Type     : WorkflowModelItemTests                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for workflow model items.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using Empiria.Workflow.Definition;

namespace Empiria.Tests.Workflow.Definition {

  /// <summary>Test cases for workflow model items.</summary>
  public class WorkflowModelItemTests {

    #region Facts

    [Fact]
    public void Should_Parse_All_Workflow_Model_Items() {
      var sut = BaseObject.GetList<WorkflowModelItem>();

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public void Should_Parse_Empty_Workflow_Model_Item() {
      var sut = WorkflowModelItem.Empty;

      Assert.NotNull(sut);
    }


    [Fact]
    public void Should_Parse_Empty_Workflow_Model_Item_Assignation_Rules() {
      WorkflowModelItem modelItem = TestingConstants.WKF_MODEL_ITEM_WITH_ASSIGNATION_RULES;

      AssignationRules sut = modelItem.AssignationRules;

      Assert.Equal(AssignationRule.FixedValue, sut.RequestedBy);
      Assert.Equal(AssignationRule.RequestResponsible, sut.RequestedByOrgUnit);
      Assert.Equal(AssignationRule.CurrentUser, sut.AssignedTo);
      Assert.Equal(AssignationRule.RequestRequester, sut.AssignedToOrgUnit);
    }

    #endregion Facts

  }  // class WorkflowModelItemTests

}  // namespace Empiria.Tests.Workflow.Definition
