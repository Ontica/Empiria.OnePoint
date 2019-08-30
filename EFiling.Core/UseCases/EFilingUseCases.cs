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


    //static public EFilingRequest CreateApplicationForm(string filingRequestUID, JsonObject formData) {
    //  EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);
    //  Assertion.AssertObject(formData, "formData");

    //  var applicationForm = filingRequest.AddApplicationForm(formData);

    //  applicationForm.Save();

    //  return filingRequest;
    //}


    static public EFilingRequestDTO CreateEFilingRequest(CreateEFilingRequestDTO requestDTO) {
      Assertion.AssertObject(requestDTO, "requestDTO");

      var procedure = Procedure.Parse(requestDTO.procedureType);

      var filingRequest = new EFilingRequest(procedure, requestDTO.requestedBy);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO GeneratePaymentOrderForEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.GeneratePaymentOrder();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO GetEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      return EFilingMapper.Map(filingRequest);
    }


    static public FixedList<EFilingRequestDTO> GetEFilingRequestListByStatus(EFilingRequestStatus status,
                                                                             string keywords) {
      var list = EFilingRequest.GetList(status, keywords);

      return EFilingMapper.Map(list);
    }


    static public EFilingRequestDTO RevokeEFilingRequestSign(string filingRequestUID,
                                                             JsonObject revokeSignData) {
      Assertion.AssertObject(revokeSignData, "revokeSignData");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.RevokeSign(revokeSignData);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO SetPaymentReceipt(string filingRequestUID,
                                                      string receiptNo) {
      Assertion.AssertObject(receiptNo, "receiptNo");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.SetPaymentReceipt(receiptNo);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO SignEFilingRequest(string filingRequestUID,
                                                       JsonObject signData) {
      Assertion.AssertObject(signData, "signData");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);
      filingRequest.Sign(signData);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO SubmitEFilingRequest(string filingRequestUID) {
      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.Submit();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO UpdateApplicationForm(string filingRequestUID, JsonObject json) {
      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.SetApplicationForm(json);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO UpdateEFilingRequest(string filingRequestUID,
                                                         Requester requestedBy) {
      Assertion.AssertObject(requestedBy, "requestedBy");

      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.Update(requestedBy);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
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
