/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : One Point Security Services                  Component : Test cases                            *
*  Assembly : Empiria.OnePoint.Security.Tests.dll          Pattern   : Services tests                        *
*  Type     : SubjectsEditionTests                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Test cases for Empiria OnePoint subjects edition.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.StateEnums;
using Empiria.Tests;

using Empiria.OnePoint.Security.Subjects.UseCases;
using Empiria.OnePoint.Security.Subjects.Adapters;


namespace Empiria.OnePoint.Security.Tests {

  /// <summary>Test cases for Empiria OnePoint subjects edition.</summary>
  public class SubjectsEditionTests {

    private readonly SubjectUseCases _usecases;


    #region Initialization

    public SubjectsEditionTests() {
      TestsCommonMethods.Authenticate();

      _usecases = SubjectUseCases.UseCaseInteractor();

    }

    ~SubjectsEditionTests() {
      _usecases.Dispose();
    }

    #endregion Initialization
    #region Facts

    [Fact]
    public void Should_Active_A_Subject() {

      SubjectDto sut = _usecases.ActivateSubject(TestingConstants.SUBJECT_UID);

      Assert.NotNull(sut);
      Assert.True(sut.Status.UID == EntityStatus.Active.ToString());
    }


    [Fact]
    public void Should_Suspend_A_Subject() {

      SubjectDto sut = _usecases.SuspendSubject(TestingConstants.SUBJECT_UID);

      Assert.NotNull(sut);
      Assert.True(sut.Status.UID == EntityStatus.Suspended.ToString());
    }

    #endregion Facts

  }  // class SubjectsEditionTests

} // namespace Empiria.OnePoint.Security.Tests
