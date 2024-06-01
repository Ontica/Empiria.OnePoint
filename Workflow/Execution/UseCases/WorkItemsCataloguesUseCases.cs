/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Use case interactor class               *
*  Type     : WorkItemsCataloguesUseCases                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases taht returns catalogues information for work items.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Parties;
using Empiria.Services;
using Empiria.StateEnums;

namespace Empiria.Workflow.Execution.UseCases {

  /// <summary>Use cases taht returns catalogues information for work items.</summary>
  public class WorkItemsCataloguesUseCases : UseCase {

    #region Constructors and parsers

    protected WorkItemsCataloguesUseCases() {
      // no-op
    }

    static public WorkItemsCataloguesUseCases UseCaseInteractor() {
      return CreateInstance<WorkItemsCataloguesUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public FixedList<NamedEntityDto> ResponsiblesList(string workitemTypeUID) {
      var list = new List<NamedEntityDto> {
        new NamedEntityDto(Party.Parse(1001).UID, Party.Parse(1001).Name),
        new NamedEntityDto(Party.Parse(1002).UID, Party.Parse(1002).Name),
        new NamedEntityDto(Party.Parse(1003).UID, Party.Parse(1003).Name),
        new NamedEntityDto(Party.Parse(1004).UID, Party.Parse(1004).Name),
        new NamedEntityDto(Party.Parse(1005).UID, Party.Parse(1005).Name),
      };
      return list.ToFixedList().Sort((x, y) => x.Name.CompareTo(y.Name)).Reverse();
    }


    public FixedList<NamedEntityDto> StatusList() {
      var list = new List<NamedEntityDto> {
        new NamedEntityDto(ActivityStatus.All.ToString(), ActivityStatus.All.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Pending.ToString(), ActivityStatus.Pending.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Review.ToString(), ActivityStatus.Review.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Suspended.ToString(), ActivityStatus.Suspended.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Completed.ToString(), ActivityStatus.Completed.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Canceled.ToString(), ActivityStatus.Canceled.GetPluralName()),
        new NamedEntityDto(ActivityStatus.Deleted.ToString(), ActivityStatus.Deleted.GetPluralName()),
      };

      return list.ToFixedList();
    }

    #endregion Use cases

  }  // class WorkItemsCataloguesUseCases

}  // namespace Empiria.Workflow.Definition.UseCases
