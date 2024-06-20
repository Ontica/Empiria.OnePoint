/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Use case interactor class               *
*  Type     : WorkflowInstanceUseCases                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for manage workflow instances.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Services;

using Empiria.Workflow.Requests.Adapters;

namespace Empiria.Workflow.Requests.UseCases {

  /// <summary>Use cases for manage workflow instances.</summary>
  public class WorkflowInstanceUseCases : UseCase {

    #region Constructors and parsers

    protected WorkflowInstanceUseCases() {
      // no-op
    }

    static public WorkflowInstanceUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<WorkflowInstanceUseCases>();
    }


    #endregion Constructors and parsers

    #region Use cases

    public RequestDto CreateRequest(RequestFieldsDto fields) {
      Assertion.Require(fields, nameof(fields));

      var requestType = RequestType.Parse(fields.RequestTypeUID);

      Request request = requestType.CreateRequest(fields);

      request.Save();

      return RequestMapper.Map(request);
    }


    public void DeleteRequest(string requestUID) {
      Assertion.Require(requestUID, nameof(requestUID));

      var request = Request.Parse(requestUID);

      request.Delete();

      request.Save();
    }


    public FixedList<RequestDto> SearchRequests(RequestsQuery query) {
      Assertion.Require(query, nameof(query));

      query.EnsureIsValid();

      string filter = query.MapToFilterString();
      string sort = query.MapToSortString();

      EmpiriaLog.Debug(filter);

      FixedList<Request> requests = Request.GetList(filter, sort, 200);

      return RequestMapper.Map(requests);
    }

    #endregion Use cases

  }  // class WorkflowInstanceUseCases

}  // namespace Empiria.Workflow.Requests.UseCases
