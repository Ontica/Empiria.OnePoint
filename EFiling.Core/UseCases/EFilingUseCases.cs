/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : EFilingUseCases                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic filing services.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Threading.Tasks;

using Empiria.Json;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Use cases that implements electronic filing services.</summary>
  static public class EFilingUseCases {

    #region Use cases

    static public EFilingRequestDto CreateEFilingRequest(CreateEFilingRequestDTO requestDTO) {
      Assertion.AssertObject(requestDTO, "requestDTO");

      var procedure = Procedure.Parse(requestDTO.procedureType);

      var filingRequest = new EFilingRequest(procedure, requestDTO.requestedBy);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public void DeleteEFilingRequest(string filingRequestUID) {
      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.Delete();

      filingRequest.Save();
    }


    static public async Task SynchronizeExternalData() {
      var list = EFilingRequest.GetList<EFilingRequest>();

      foreach (var request in list) {
        if (request.HasTransaction && request.Transaction.LastUpdate == ExecutionServer.DateMinValue) {
          await SynchronizeExternalData(request.UID).ConfigureAwait(false);
        }
      }
    }


    static public async Task<EFilingRequestDto> SynchronizeExternalData(string filingRequestUID) {
      var filingRequest = ParseEFilingRequest(filingRequestUID);

      await filingRequest.Synchronize();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public async Task<EFilingRequestDto> GeneratePaymentOrderForEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      if (!filingRequest.HasTransaction) {
        await filingRequest.CreateTransaction();
      } else {
        await filingRequest.Synchronize();
      }

      if (!filingRequest.HasPaymentOrder) {
        await filingRequest.CreatePaymentOrder();
      }

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDto GetEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      return EFilingMapper.Map(filingRequest);
    }


    static public FixedList<EFilingRequestDto> GetEFilingRequestListByStatus(RequestStatus status,
                                                                             string keywords, int count) {
      var list = EFilingRequest.GetList(status, keywords, count);

      return EFilingMapper.Map(list);
    }


    static public EFilingRequestDto RevokeEFilingRequestSign(string filingRequestUID,
                                                             JsonObject revokeSignData) {
      Assertion.AssertObject(revokeSignData, "revokeSignData");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.RevokeSign(revokeSignData);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDto SendEFilingRequestToSign(string filingRequestUID) {
      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.SendToSign();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDto SetPaymentReceipt(string filingRequestUID,
                                                      string receiptNo) {
      Assertion.AssertObject(receiptNo, "receiptNo");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.SetPaymentReceipt(receiptNo);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDto SignEFilingRequest(string filingRequestUID,
                                                       JsonObject signInputData) {
      Assertion.AssertObject(signInputData, "signInputData");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.Sign(signInputData);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public async Task<EFilingRequestDto> SubmitEFilingRequest(string filingRequestUID) {
      var filingRequest = ParseEFilingRequest(filingRequestUID);

      await filingRequest.Submit();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDto UpdateApplicationForm(string filingRequestUID,
                                                          JsonObject json) {
      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.SetApplicationForm(json);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDto UpdateEFilingRequest(string filingRequestUID,
                                                         RequesterDto requestedBy) {
      Assertion.AssertObject(requestedBy, "requestedBy");

      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.SetRequesterData(requestedBy);

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
