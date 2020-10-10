/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : EFilingDocumentsUseCases                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implement documentation services for electronic filing requests.                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.UseCases;

namespace Empiria.OnePoint.EFiling.UseCases {

  /// <summary>Use cases that implement documentation services for electronic filing requests.</summary>
  public class EFilingDocumentsUseCases : UseCasesBase {

    #region Use cases

    public FixedList<EFilingDocument> GetOutputDocuments(string filingRequestUID) {
      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      var provider = ExternalProviders.GetFilingTransactionProvider(filingRequest.Procedure);

      return provider.GetOutputDocuments(filingRequest.Transaction.UID);
    }

    #endregion Use cases

  }  // class EFilingDocumentsUseCases

}  // namespace Empiria.OnePoint.EFiling.UseCases
