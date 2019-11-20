/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Service provider                        *
*  Type     : EFilingRequestSigner                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs sign operations for electronic filing requests.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Security;

using Empiria.Json;
using Empiria.Security;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Performs sign operations for electronic filing requests.</summary>
  internal class EFilingRequestSigner {


    #region Constructors and parsers


    internal EFilingRequestSigner(EFilingRequest filingRequest) {
      this.FilingRequest = filingRequest;
    }


    #endregion Constructors and parsers


    #region Properties


    internal EFilingRequest FilingRequest {
      get;
      private set;
    }


    #endregion Properties


    #region Methods


    internal void RevokeSign(JsonObject credentials) {
      var password = credentials.Get<string>("revokeSignToken");

      SecureString securedPassword = Cryptographer.ConvertToSecureString(password);

      Cryptographer.AssertValidPrivateKeyPassword(securedPassword);
    }


    internal JsonObject Sign(JsonObject credentials) {
      var password = credentials.Get<string>("signToken");

      SecureString securedPassword = Cryptographer.ConvertToSecureString(password);

      var electronicSeal = this.FilingRequest.GetElectronicSeal();

      var signature = Cryptographer.SignText(electronicSeal, securedPassword);

      var signData = new JsonObject();

      signData.Add("signature", signature);
      signData.Add("timestamp", DateTime.Now);

      return signData;
    }

    #endregion Methods

  }  // class EFilingRequestSigner

} // namespace Empiria.OnePoint.EFiling
