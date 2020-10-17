/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : PaymentUseCases                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements payment services for e-filing requests.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Threading.Tasks;

using Empiria.Services;

namespace Empiria.OnePoint.EFiling.UseCases {

  /// <summary>Use cases that implements payment services for e-filing requests.</summary>
  public class PaymentUseCases : UseCase {

    #region Use cases

    public async Task<EFilingRequestDto> GeneratePaymentOrder(string filingRequestUID) {
      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

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


    public EFilingRequestDto SetPaymentReceipt(string filingRequestUID, string receiptNo) {
      Assertion.AssertObject(receiptNo, "receiptNo");

      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      filingRequest.SetPaymentReceipt(receiptNo);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }

    #endregion Use cases

  }  // class PaymentUseCases

}  // namespace Empiria.OnePoint.EFiling.UseCases
