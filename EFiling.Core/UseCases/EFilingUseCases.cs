/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : EFilingRequestUseCases                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements services for electronic filing requests.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Threading.Tasks;

using Empiria.Json;

namespace Empiria.OnePoint.EFiling.UseCases {

  /// <summary>Use cases that implements services for electronic filing requests.</summary>
  static public class EFilingRequestUseCases {

    #region Use cases

    static public EFilingRequestDto CreateEFilingRequest(CreateEFilingRequestDto requestDTO) {
      Assertion.AssertObject(requestDTO, "requestDTO");

      var procedure = Procedure.Parse(requestDTO.ProcedureType);

      var filingRequest = new EFilingRequest(procedure, requestDTO.RequestedBy);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public void DeleteEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      filingRequest.Delete();

      filingRequest.Save();
    }


    static public EFilingRequestDto GetEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      return EFilingMapper.Map(filingRequest);
    }


    static public FixedList<EFilingRequestDto> GetEFilingRequestListByStatus(RequestStatus status,
                                                                             string keywords, int count) {
      var list = EFilingRequest.GetList(status, keywords, count);

      return EFilingMapper.Map(list);
    }


    static public async Task<EFilingRequestDto> SubmitEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      await filingRequest.Submit();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDto UpdateApplicationForm(string filingRequestUID,
                                                          JsonObject json) {
      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      filingRequest.SetApplicationForm(json);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDto UpdateEFilingRequest(string filingRequestUID,
                                                         RequesterDto requestedBy) {
      Assertion.AssertObject(requestedBy, "requestedBy");

      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      filingRequest.SetRequesterData(requestedBy);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    #endregion Use cases


    #region Synchronization use cases

    static public async Task SynchronizeAllExternalData() {
      var list = EFilingRequest.GetList<EFilingRequest>();

      foreach (var request in list) {
        if (request.HasTransaction && request.Transaction.LastUpdate == ExecutionServer.DateMinValue) {
          await SynchronizeExternalData(request.UID).ConfigureAwait(false);
        }
      }
    }


    static public async Task<EFilingRequestDto> SynchronizeExternalData(string filingRequestUID) {
      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      await filingRequest.Synchronize();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }

    #endregion Synchronization use cases

  }  // class EFilingRequestUseCases

}  // namespace Empiria.OnePoint.EFiling.UseCases
