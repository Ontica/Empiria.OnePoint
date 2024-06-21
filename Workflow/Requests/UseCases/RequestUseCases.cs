/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Use case interactor class               *
*  Type     : RequestUseCases                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for create, update and search requests.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Services;

using Empiria.Workflow.Requests.Adapters;

namespace Empiria.Workflow.Requests.UseCases {

  /// <summary>Use cases for create, update and search requests.</summary>
  public class RequestUseCases : UseCase {

    #region Constructors and parsers

    protected RequestUseCases() {
      // no-op
    }

    static public RequestUseCases UseCaseInteractor() {
      return UseCase.CreateInstance<RequestUseCases>();
    }


    #endregion Constructors and parsers

    #region Use cases

    public RequestDescriptorDto CreateRequest(RequestFieldsDto fields) {
      Assertion.Require(fields, nameof(fields));

      var requestType = RequestType.Parse(fields.RequestTypeUID);

      Request request = requestType.CreateRequest(fields);

      request.Save();

      return RequestMapper.MapToDescriptor(request);
    }


    public void DeleteRequest(string requestUID) {
      Assertion.Require(requestUID, nameof(requestUID));

      var request = Request.Parse(requestUID);

      request.Delete();

      request.Save();
    }


    public RequestHolderDto GetRequest(string requestUID) {
      Assertion.Require(requestUID, nameof(requestUID));

      var request = Request.Parse(requestUID);

      return RequestMapper.Map(request);
    }


    public FixedList<RequestDescriptorDto> SearchRequests(RequestsQuery query) {
      Assertion.Require(query, nameof(query));

      query.EnsureIsValid();

      string filter = query.MapToFilterString();
      string sort = query.MapToSortString();

      EmpiriaLog.Debug(filter);

      FixedList<Request> requests = Request.GetList(filter, sort, 200);

      return RequestMapper.MapToDescriptor(requests);
    }

    #endregion Use cases

  }  // class RequestUseCases

}  // namespace Empiria.Workflow.Requests.UseCases
