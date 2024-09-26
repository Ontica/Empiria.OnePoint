/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Test cases                              *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll        Pattern   : Unit tests                              *
*  Type     : RequestDefTests                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for request definition objects.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using Empiria.Workflow.Definition;

namespace Empiria.Tests.Workflow.Definition {

  /// <summary>Test cases for request definition objects.</summary>
  public class RequestDefTests {

    #region Facts

    [Fact]
    public void Should_Parse_All_RequestDef() {
      var sut = BaseObject.GetList<RequestDef>();

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public void Should_Parse_Empty_RequestDef() {
      var sut = RequestDef.Empty;

      Assert.NotNull(sut);
      Assert.Equal(TestingConstants.REQ_DEF_TYPE, sut.GetEmpiriaType());
    }


    [Fact]
    public void Should_Parse_RequestDef_List() {
      var sut = RequestDef.GetList(TestingConstants.REQ_DEF_LIST_NAME);

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }

    #endregion Facts

  }  // class RequestDefTests

}  // namespace Empiria.Tests.Workflow.Definition
