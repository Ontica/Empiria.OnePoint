/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases                               *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases Data Mapper                   *
*  Type     : EFilingMapper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data mapping methods between electronic filing entities and their data transfer objects.       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.OnePoint.EFiling.UseCases {

  /// <summary>Data mapping methods between electronic filing entities and their data transfer objects.</summary>
  static internal class EFilingMapper {


    static internal EFilingRequest Map(string filingRequestUID) {
      Assertion.AssertObject(filingRequestUID, "filingRequestUID");

      var request = EFilingRequest.TryParse(filingRequestUID);

      if (request == null) {
        throw new ResourceNotFoundException("ElectronicFilingRequest.UID.NotFound",
                        $"No tenemos registrada ninguna solicitud con identificador {filingRequestUID}.");
      }

      return request;
    }


    static internal FixedList<EFilingRequestDto> Map(FixedList<EFilingRequest> source) {
      return new FixedList<EFilingRequestDto>(source.Select((x) => Map(x)));
    }


    static internal EFilingRequestDto Map(EFilingRequest request) {
      var r = new EFilingRequestDto(request);

      if (request.ApplicationForm.HasItems) {
        r.Form = new ApplicationFormDto(request);
      }

      if (request.IsSigned) {
        r.Esign = new ESignDataDto(request);
      }

      if (request.HasTransaction) {
        r.Transaction = new TransactionDataDto(request);
      }

      if (request.HasPaymentOrder) {
        r.PaymentOrder = new PaymentDataDto(request);
      }

      if (request.IsClosed) {
        r.OutputDocuments = request.Transaction.OutputDocuments;
      }

      return r;
    }


  }  // class EFilingMapper

}  // namespace Empiria.OnePoint.ESign.UseCases
