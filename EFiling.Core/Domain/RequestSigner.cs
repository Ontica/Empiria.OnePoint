/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Service provider                        *
*  Type     : RequestSigner                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Performs electronic sign operations for e-filing requests.                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Security;

using Empiria.Json;
using Empiria.Security;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Performs electronic sign operations for e-filing requests.</summary>
  internal class RequestSigner {

    private readonly EFilingRequest _request;

    #region Constructors and parsers

    internal RequestSigner(EFilingRequest request) {
      _request = request;

      this.SecurityData = new SecurityData(request);
    }


    #endregion Constructors and parsers

    #region Public members

    public DateTime AuthorizationTime {
      get {
        return _request.ExtensionData.Get("authorizationTime", ExecutionServer.DateMaxValue);
      }
      private set {
        if (value != ExecutionServer.DateMaxValue) {
          _request.ExtensionData.Set("authorizationTime", value);
        } else {
          _request.ExtensionData.Remove("authorizationTime");
        }
      }
    }


    internal bool IsSigned {
      get {
        return this.SecurityData.ElectronicSign.Length != 0;
      }
    }


    internal SecurityData SecurityData {
      get;
    }


    internal void RevokeSign(JsonObject credentials) {
      Assertion.AssertObject(credentials, "credentials");

      EnsureCanRevokeSign();

      var password = credentials.Get<string>("revokeSignToken");

      SecureString securedPassword = Cryptographer.ConvertToSecureString(password);

      Cryptographer.AssertValidPrivateKeyPassword(securedPassword);

      this.AuthorizationTime = ExecutionServer.DateMaxValue;

      _request.OnSignRevoked();
    }


    internal void Sign(JsonObject credentials) {
      Assertion.AssertObject(credentials, "credentials");

      EnsureCanBeSigned();

      this.AuthorizationTime = DateTime.Now;

      JsonObject signData = this.GetESignData(credentials);

      _request.OnSigned(signData);
    }


    #endregion Public members


    #region Private methods

    private void EnsureCanBeSigned() {
      Assertion.Assert(!this.IsSigned, "This filing was already signed.");

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsSigner, "Current user can't sign this filing.");
    }


    private void EnsureCanRevokeSign() {
      Assertion.Assert(this.IsSigned, "This filing is not signed.");

      var userContext = EFilingUserContext.Current();

      Assertion.Assert(userContext.IsSigner, "Current user can't revoke sign.");

      Assertion.Assert(userContext.User.Equals(_request.Agent),
                      "Current user is not the same as this filing signer.");
    }


    private JsonObject GetESignData(JsonObject credentials) {
      var password = credentials.Get<string>("signToken");

      SecureString securedPassword = Cryptographer.ConvertToSecureString(password);

      var electronicSeal = this.SecurityData.GetElectronicSeal();

      var signature = Cryptographer.SignText(electronicSeal, securedPassword);

      var signData = new JsonObject();

      signData.Add("signature", signature);
      signData.Add("timestamp", DateTime.Now);

      return signData;
    }

    #endregion Private methods

  }  // class RequestSigner

} // namespace Empiria.OnePoint.EFiling
