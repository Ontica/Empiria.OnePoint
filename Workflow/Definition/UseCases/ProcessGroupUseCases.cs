/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Use case interactor class               *
*  Type     : ProcessGroupUseCases                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for retrieve information for a process group.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Parties;
using Empiria.Services;

namespace Empiria.Workflow.Definition.UseCases {

  /// <summary>Use cases for retrieve information for a process group.</summary>
  public class ProcessGroupUseCases : UseCase {

    #region Constructors and parsers

    protected ProcessGroupUseCases() {
      // no-op
    }

    static public ProcessGroupUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<ProcessGroupUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public FixedList<NamedEntityDto> OrganizationalUnits(string processGroupUID) {
      Assertion.Require(processGroupUID, nameof(processGroupUID));

      FixedList<OrganizationalUnit> list = Party.GetList<OrganizationalUnit>(DateTime.Today);

      list.Sort((x, y) => x.Code.CompareTo(y.Code));

      list = base.RestrictUserDataAccessTo(list);

      return list.MapToNamedEntityList();
    }


    public FixedList<NamedEntityDto> OrganizationalUnitProcessTypes(string processGroupUID,
                                                                    string organizationalUnitUID) {
      Assertion.Require(processGroupUID, nameof(processGroupUID));
      Assertion.Require(organizationalUnitUID, nameof(organizationalUnitUID));

      var processGroup = ProcessGroup.Parse(processGroupUID);

      return processGroup.Processes.MapToNamedEntityList();
    }

    #endregion Use cases

  }  // class ProcessGroupUseCases

}  // namespace Empiria.Workflow.Definition.UseCases
