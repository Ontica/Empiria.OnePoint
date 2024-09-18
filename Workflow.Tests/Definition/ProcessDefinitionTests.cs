/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Test cases                              *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll        Pattern   : Unit tests                              *
*  Type     : ProcessDefinitionTests                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Test cases for workflow instances.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Xunit;

using Empiria.Workflow.Definition;

namespace Empiria.Tests.Workflow.Definition {

  /// <summary>Test cases for workflow instances.</summary>
  public class ProcessDefinitionTests {

    #region Facts

    [Fact]
    public void Should_Parse_Empty_Process_Definition() {
      var sut = ProcessDef.Empty;

      Assert.NotNull(sut);
      Assert.NotNull(sut.Model);
      Assert.Empty(sut.Model);
    }


    [Fact]
    public void Should_Parse_Process_Definition() {
      var sut = ProcessDef.Parse(101);

      Assert.NotNull(sut);
      Assert.NotNull(sut.Model);
      Assert.NotEmpty(sut.Model);
    }


    [Fact]
    public void Should_Get_Process_Definition_Initial_Steps() {
      var processDef = ProcessDef.Parse(101);

      FixedList<WorkflowModelItem> sut = processDef.GetInitialSteps();

      Assert.NotNull(sut);
      Assert.NotEmpty(sut);
    }

    #endregion Facts

  }  // class ProcessDefinitionTests

}  // namespace Empiria.Tests.Workflow.Definition
