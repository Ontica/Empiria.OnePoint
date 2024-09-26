/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Use case interactor class               *
*  Type     : RequestDefUseCases                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for workflow RequestDef instances.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services;

using Empiria.Workflow.Definition.Adapters;

namespace Empiria.Workflow.Definition.UseCases {

  /// <summary>Use cases for workflow RequestDef instances.</summary>
  public class RequestDefUseCases : UseCase {

    #region Constructors and parsers

    protected RequestDefUseCases() {
      // no-op
    }

    static public RequestDefUseCases UseCaseInteractor() {
      return CreateInstance<RequestDefUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public FixedList<RequestDefDto> RequestDefinitions(string requestsDefList) {
      Assertion.Require(requestsDefList, nameof(requestsDefList));

      FixedList<RequestDef> list = RequestDef.GetList(requestsDefList);

      return RequestDefMapper.Map(list);
    }


    public FixedList<RequestDefDto> RequestDefinitions(string requestsDefList,
                                                       string requesterOrgUnitUID) {

      Assertion.Require(requestsDefList, nameof(requestsDefList));
      Assertion.Require(requesterOrgUnitUID, nameof(requesterOrgUnitUID));

      FixedList<RequestDef> list = RequestDef.GetList(requestsDefList);

      return RequestDefMapper.Map(list);
    }

    #endregion Use cases

  }  // class RequestDefUseCases

}  // namespace Empiria.Workflow.Definition.UseCases
