/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : EFilingServices                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Implements IFilingServices interface.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Threading.Tasks;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Implements IFilingServices interface.</summary>
  public class EFilingServices : IFilingServices {

    #region Services


    public EFilingRequest GetEFilingRequest(string filingRequestUID) {
      Assertion.AssertObject(filingRequestUID, "filingRequestUID");

      var filingRequest = EFilingRequest.TryParse(filingRequestUID);

      Assertion.AssertObject(filingRequest, $"Invalid filing request with UID {filingRequestUID}.");

      return filingRequest;
    }


    public async Task NotifyEvent(string filingRequestUID, string eventName) {
      Assertion.AssertObject(filingRequestUID, "filingRequestUID");
      Assertion.AssertObject(eventName, "eventName");

      switch (eventName) {
        case "TransactionReceived":
        case "TransactionReadyToDelivery":
        case "TransactionReturned":
        case "TransactionArchived":
        case "TransactionReentered":
          await ChangeTransactionStatus(filingRequestUID, eventName);
          return;

        default:
          throw Assertion.AssertNoReachThisCode($"Unrecognized external event with name {eventName}.");
      }
    }


    #endregion Services


    #region Implementation


    private async Task ChangeTransactionStatus(string filingRequestUID, string eventName) {
      var filingRequest = EFilingRequest.TryParse(filingRequestUID);

      Assertion.AssertObject(filingRequest, $"Invalid filing request with UID {filingRequestUID}.");

      var interactor = new EFilingExternalServicesInteractor(filingRequest);

      interactor.InformEventProcessed(filingRequest.Transaction.UID, eventName);

      RequestStatus newStatus = GetNewStatusAfterEvent(eventName);

      await filingRequest.UpdateStatus(newStatus);

      filingRequest.Save();
    }


    private RequestStatus GetNewStatusAfterEvent(string eventName) {
      switch (eventName) {
        case "TransactionReceived":
          return RequestStatus.Submitted;

        case "TransactionReadyToDelivery":
          return RequestStatus.Finished;

        case "TransactionReturned":
          return RequestStatus.Rejected;

        case "TransactionArchived":
          return RequestStatus.Finished;

        case "TransactionReentered":
          return RequestStatus.Submitted;

        default:
          throw Assertion.AssertNoReachThisCode($"Unrecognized external event with name '{eventName}'");
      }
    }


    #endregion Implementation

  }  // class EFilingUseCases

}  // namespace Empiria.OnePoint.EFiling
