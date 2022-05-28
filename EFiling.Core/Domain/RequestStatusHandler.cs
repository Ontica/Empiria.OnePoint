/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Information Holder                      *
*  Type     : RequestStatusHandler                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : EFilingRequest collaborator that provides status data and services.                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

namespace Empiria.OnePoint.EFiling {

  /// <summary>EFilingRequest collaborator that provides status data and services.</summary>
  internal class RequestStatusHandler {

    #region Fields

    private readonly EFilingRequest _request;
    private readonly EFilingExternalServicesInteractor _externalServices;

    #endregion Fields

    #region Constructors and parsers

    internal RequestStatusHandler(EFilingRequest request,
                                  EFilingExternalServicesInteractor externalServices) {
      Assertion.Require(request, "request");
      Assertion.Require(externalServices, "externalServices");

      _request = request;
      _externalServices = externalServices;
    }

    #endregion Constructors and parsers


    #region Properties

    public bool IsClosed {
      get {
        return (_request.Status == RequestStatus.Finished ||
                _request.Status == RequestStatus.Rejected);
      }
    }

    #endregion Properties


    #region Methods


    internal void Delete() {
      this.EnsureCanBeEdited();

      _request.OnStatusChanged(RequestStatus.Deleted);
    }


    internal void EnsureCanBeEdited() {
      Assertion.Ensure(!_request.IsSigned, "This filing is already signed, so it can't be edited.");

      Assertion.Ensure(_request.Status == RequestStatus.Pending,
                       "This filing is not in pending status, so it can't be edited.");

      var userContext = EFilingUserContext.Current();

      Assertion.Ensure(userContext.IsRegister, "Current user can't edit this filing.");
    }


    internal void EnsureCanBeSubmitted() {
      Assertion.Ensure(_request.Status == RequestStatus.OnPayment,
                       "Invalid status for submitting. Must be OnPayment");

      Assertion.Ensure(_request.PaymentOrder.ReceiptNo, "No receipt number provided.");

      var userContext = EFilingUserContext.Current();

      Assertion.Ensure(userContext.IsManager, "Current user can't submit this filing.");
    }


    internal void SendToSign() {
      this.EnsureCanBeEdited();

      _request.OnStatusChanged(RequestStatus.OnSign);
    }


    internal void Signed() {
      _request.LastUpdate = DateTime.Now;

      _request.OnStatusChanged(RequestStatus.OnPayment);
    }


    internal void SignRevoked() {
      _request.LastUpdate = ExecutionServer.DateMinValue;

      _request.OnStatusChanged(RequestStatus.Pending);
    }


    internal async Task Submit() {
      this.EnsureCanBeSubmitted();

      await _externalServices.SetPayment();

      await _externalServices.Submit();

      await this.UpdateStatus(RequestStatus.Submitted)
                .ConfigureAwait(false);
    }


    internal async Task UpdateStatus(RequestStatus newStatus) {
      _request.OnStatusChanged(newStatus);

      if (newStatus.RequiresExternalDataSynchronization()) {
        await _request.Synchronize()
                      .ConfigureAwait(false);
      }
    }

    #endregion Methods

  }  // class RequestStatusHandler

}  // namespace Empiria.OnePoint.EFiling
