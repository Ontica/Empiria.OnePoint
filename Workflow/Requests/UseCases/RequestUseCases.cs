/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Use case interactor class               *
*  Type     : RequestUseCases                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases for create, update and search requests.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Services;
using Empiria.StateEnums;

using Empiria.Workflow.Definition;
using Empiria.Workflow.Execution;

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

    public RequestHolderDto ActivateRequest(string requestUID) {
      Assertion.Require(requestUID, nameof(requestUID));

      var request = Request.Parse(requestUID);

      request.Activate();

      request.Save();

      return RequestMapper.Map(request);
    }


    public RequestHolderDto CancelRequest(string requestUID) {
      Assertion.Require(requestUID, nameof(requestUID));

      var request = Request.Parse(requestUID);

      request.Cancel();

      request.Save();

      return RequestMapper.Map(request);
    }


    public RequestHolderDto CompleteRequest(string requestUID) {
      Assertion.Require(requestUID, nameof(requestUID));

      var request = Request.Parse(requestUID);

      request.Complete();

      request.Save();

      return RequestMapper.Map(request);
    }


    public RequestHolderDto CreateRequest(RequestFields fields) {
      Assertion.Require(fields, nameof(fields));

      var requestDef = RequestDef.Parse(fields.RequestDefUID);

      Request request = requestDef.RequestType.CreateRequest(fields);

      request.Save();

      return RequestMapper.Map(request);
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


    public FixedList<RequestListItemDto> SearchRequests(RequestsQuery query) {
      Assertion.Require(query, nameof(query));

      query.EnsureIsValid();

      string filter = query.MapToFilterString();
      string sort = query.MapToSortString();

      FixedList<Request> requests = Request.GetList(filter, sort, 200);

      return RequestMapper.MapToListItems(requests);
    }


    public RequestHolderDto StartRequest(string requestUID) {
      Assertion.Require(requestUID, nameof(requestUID));

      var request = Request.Parse(requestUID);

      Assertion.Require(request.Actions.CanStart(), "No se puede iniciar esta solicitud.");

      var requestEngine = request.GetEngine();

      requestEngine.Start();

      requestEngine.Save();

      return RequestMapper.Map(request);
    }


    public RequestHolderDto SuspendRequest(string requestUID) {
      Assertion.Require(requestUID, nameof(requestUID));

      var request = Request.Parse(requestUID);

      request.Suspend();

      request.Save();

      return RequestMapper.Map(request);
    }


    public RequestHolderDto UpdateRequest(string requestUID, RequestFields fields) {
      Assertion.Require(requestUID, nameof(requestUID));
      Assertion.Require(fields, nameof(fields));

      var request = Request.Parse(requestUID);

      request.Update(fields);

      request.Save();

      return RequestMapper.Map(request);
    }

    #endregion Use cases

  }  // class RequestUseCases

}  // namespace Empiria.Workflow.Requests.UseCases
