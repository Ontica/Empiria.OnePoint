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

using Empiria.Workflow.Definition.Adapters;

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

    public FixedList<NamedEntityDto> OrganizationalUnits(string processGroupCode) {
      Assertion.Require(processGroupCode, nameof(processGroupCode));

      FixedList<OrganizationalUnit> list = Party.GetList<OrganizationalUnit>(DateTime.Today);

      list.Sort((x, y) => x.Code.CompareTo(y.Code));

      list = base.RestrictUserDataAccessTo(list);

      return list.MapToNamedEntityList();
    }


    public FixedList<ProcessDefDto> OrganizationalUnitProcessTypes(string processGroupCode,
                                                                   string organizationalUnitUID) {
      Assertion.Require(processGroupCode, nameof(processGroupCode));
      Assertion.Require(organizationalUnitUID, nameof(organizationalUnitUID));

      var processGroup = WorkflowObjectsGroup.ParseWithCode(processGroupCode);

      var processes = processGroup.GetItems<ProcessDef>(WorkflowObjectLinkType.ProcessesGroupType);

      return ProcessDefMapper.Map(processes);
    }

    #endregion Use cases

  }  // class ProcessGroupUseCases

}  // namespace Empiria.Workflow.Definition.UseCases
