/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.Requests.dll              Pattern   : Use case interactor class               *
*  Type     : RequestsCataloguesUseCases                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that returns catalogues information for requests.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Parties;
using Empiria.Services;
using Empiria.StateEnums;

using Empiria.OnePoint.Requests.Adapters;

namespace Empiria.OnePoint.Requests.UseCases {

  /// <summary>Use cases taht returns catalogues information for requests.</summary>
  public class CataloguesUseCases : UseCase {

    #region Constructors and parsers

    protected CataloguesUseCases() {
      // no-op
    }

    static public CataloguesUseCases UseCaseInteractor() {
      return CreateInstance<CataloguesUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public FixedList<NamedEntityDto> OrganizationalUnits(string requestsList) {
      Assertion.Require(requestsList, nameof(requestsList));

      FixedList<OrganizationalUnit> list = Party.GetList<OrganizationalUnit>(DateTime.Today);

      list.Sort((x, y) => x.Code.CompareTo(y.Code));

      list = base.RestrictUserDataAccessTo(list);

      return list.MapToNamedEntityList();
    }


    public FixedList<RequestTypeDto> RequestTypes(string requestsList,
                                                  string requesterOrgUnitUID) {

      Assertion.Require(requestsList, nameof(requestsList));
      Assertion.Require(requesterOrgUnitUID, nameof(requesterOrgUnitUID));

      FixedList<RequestType> requestTypes = RequestType.GetList(requestsList);

      return RequestMapper.Map(requestTypes);
    }


    public FixedList<NamedEntityDto> ResponsibleList(string workitemTypeUID) {
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

  }  // class CataloguesUseCases

}  // namespace Empiria.OnePoint.Requests.UseCases
