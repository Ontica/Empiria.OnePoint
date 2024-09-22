/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Test cases                              *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll        Pattern   : Unit tests                              *
*  Type     : RequestsTests                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for workflow requests.                                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Xunit;

using Empiria.Workflow.Requests;

namespace Empiria.Tests.Workflow.Requests {

  /// <summary>Test cases for workflow requests.</summary>
  public class RequestsTests {

    #region Facts

    [Fact]
    public void Should_Parse_All_Request_Instances() {
      var sut = BaseObject.GetList<Request>();

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }


    [Fact]
    public void Should_Parse_Empty_Request_Instance() {
      var sut = Request.Empty;

      Assert.NotNull(sut);
    }

    #endregion Facts

  }  // class RequestsTests

}  // namespace Empiria.Tests.Workflow.Requests
