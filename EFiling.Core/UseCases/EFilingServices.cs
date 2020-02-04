/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : EFilingServices                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Implements IFilingServices interface.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Implements IFilingServices interface.</summary>
  public class EFilingServices : IFilingServices {

    #region Services

    public void NotifyEvent(string filingRequestUID, string eventName) {
      Assertion.AssertObject(filingRequestUID, "filingRequestUID");
      Assertion.AssertObject(eventName, "eventName");

      switch (eventName) {

        case "TransactionReceived":
          return;

        case "TransactionReadyToDelivery":
          TransactionReadyToDelivery(filingRequestUID, eventName);
          return;

        case "TransactionReturned":
          return;

        case "TransactionArchived":
          return;

        case "TransactionReentered":
          return;

        default:
          throw Assertion.AssertNoReachThisCode($"Unrecognized external event with name {eventName}.");
      }
    }


    #endregion Services


    #region Implementation


    private void TransactionReadyToDelivery(string filingRequestUID, string eventName) {
      var filingRequest = EFilingRequest.TryParse(filingRequestUID);

      Assertion.AssertObject(filingRequest, $"Invalid filing request with UID {filingRequestUID}.");

      var externalProvider = ExternalProviders.GetFilingTransactionProvider(null);

      externalProvider.MarkAsReceived(filingRequest.TransactionUID);

      filingRequest.Finish();
      filingRequest.Save();
    }


    #endregion Implementation

  }  // class EFilingUseCases

}  // namespace Empiria.OnePoint.EFiling
