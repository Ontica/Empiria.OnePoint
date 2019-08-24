/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : EFilingUseCases                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic filing services.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Use cases that implements electronic filing services.</summary>
  static public class EFilingUseCases {

    #region Use cases


    static public EFilingRequest CreateEFilingRequest(JsonObject creationData) {
      Assertion.AssertObject(creationData, "creationData");

      var filingRequest = new EFilingRequest(creationData);

      filingRequest.Save();

      return filingRequest;
    }


    static public EFilingRequest GeneratePaymentOrderForEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.GeneratePaymentOrder();

      filingRequest.Save();

      return filingRequest;
    }


    static public EFilingRequest GetEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      return filingRequest;
    }


    static public FixedList<EFilingRequest> GetEFilingRequestListByStatus(EFilingRequestStatus status,
                                                                          string keywords) {
      return EFilingRequest.GetList(status, keywords);
    }


    static public EFilingRequest RevokeEFilingRequestSign(string filingRequestUID,
                                                          JsonObject revokeSignData) {
      Assertion.AssertObject(revokeSignData, "revokeSignData");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.RevokeSign(revokeSignData);

      filingRequest.Save();

      return filingRequest;
    }


    static public EFilingRequest SignEFilingRequest(string filingRequestUID,
                                                    JsonObject signData) {
      Assertion.AssertObject(signData, "signData");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);
      filingRequest.Sign(signData);

      filingRequest.Save();

      return filingRequest;
    }


    static public EFilingRequest SubmitEFilingRequest(string filingRequestUID) {
      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.Submit();

      filingRequest.Save();

      return filingRequest;
    }


    static public EFilingRequest UpdateEFilingRequest(string filingRequestUID,
                                                      JsonObject updateData) {
      Assertion.AssertObject(updateData, "updateData");

      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.Update(updateData);

      filingRequest.Save();

      return filingRequest;
    }


    #endregion Use cases


    #region Utility methods


    static private EFilingRequest ParseEFilingRequest(string filingRequestUID) {
      Assertion.AssertObject(filingRequestUID, "filingRequestUID");

      var request = EFilingRequest.TryParse(filingRequestUID);

      if (request == null) {
        throw new ResourceNotFoundException("ElectronicFilingRequest.UID.NotFound",
                                            $"No tenemos registrada ninguna solicitud con identificador {filingRequestUID}.");
      }

      return request;
    }


    #endregion Utility methods


  }  // class EFilingUseCases

}  // namespace Empiria.OnePoint.EFiling
