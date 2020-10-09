/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : ElectronicSignUseCases                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic signing of e-filing requests.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Json;

namespace Empiria.OnePoint.EFiling.UseCases {

  /// <summary>Use cases that implements electronic signing of e-filing requests.</summary>
  static public class ElectronicSignUseCases {

    #region Use cases

    static public EFilingRequestDto RevokeSign(string filingRequestUID,
                                               JsonObject revokeSignData) {
      Assertion.AssertObject(revokeSignData, "revokeSignData");

      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      filingRequest.RevokeSign(revokeSignData);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDto SendToSign(string filingRequestUID) {
      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      filingRequest.SendToSign();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDto Sign(string filingRequestUID,
                                         JsonObject signInputData) {
      Assertion.AssertObject(signInputData, "signInputData");

      EFilingRequest filingRequest = EFilingMapper.Map(filingRequestUID);

      filingRequest.Sign(signInputData);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    #endregion Use cases


  }  // class ElectronicSignUseCases

}  // namespace Empiria.OnePoint.EFiling.UseCases
