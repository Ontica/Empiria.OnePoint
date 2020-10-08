/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Information Provider                    *
*  Type     : SecurityData                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides electronic sign and integrity validation data for e-filing requests.                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Provides electronic sign and integrity validation data for e-filing requests.</summary>
  public class SecurityData : IProtected {

    #region Constructors and parsers

    private readonly EFilingRequest _request;

    internal SecurityData(EFilingRequest request) {
      _request = request;
    }

    #endregion Constructors and parsers

    #region Electronic sign data

    public string ElectronicSign {
      get {
        return _request.ExtensionData.Get("esign/signature", String.Empty);
      }
    }


    internal string GetSecurityHash() {
      return Cryptographer.CreateHashCode(this.GetElectronicSeal(),
                                          _request.UID)
                          .Substring(0, 8)
                          .ToUpperInvariant();
    }


    internal virtual string GetElectronicSeal() {
      var text = this.GetESignInputData();

      return Cryptographer.CreateHashCode(text, _request.UID);
    }


    internal string GetESignInputData() {
      return EmpiriaString.BuildDigitalString(_request.Id, _request.UID, _request.Procedure.Id, _request.RequestedBy,
                                              _request.Agency.Id, _request.Agent.Id, _request.AuthorizationTime,
                                              _request.ApplicationForm.ToString());
    }


    #endregion Electronic sign data


    #region Integrity validation data

    int IProtected.CurrentDataIntegrityVersion {
      get {
        return 1;
      }
    }


    object[] IProtected.GetDataIntegrityFieldValues(int version) {
      if (version == 1) {
        return new object[] {
          1, "Id", _request.Id, "UID", _request.UID,
          "ProcedureId", _request.Procedure.Id, "RequestedBy", _request.RequestedBy.Name,
          "AgencyId", _request.Agency.Id, "AgentId", _request.Agent.Id,
          "ExtensionData", _request.ExtensionData.ToString(), "LastUpdateTime", _request.LastUpdate,
          "PostingTime", _request.PostingTime, "PostedById", _request.PostedBy.Id, "Status", (char) _request.Status
        };
      }
      throw new SecurityException(SecurityException.Msg.WrongDIFVersionRequested, version);
    }


    private IntegrityValidator _validator;
    public IntegrityValidator Integrity {
      get {
        if (_validator == null) {
          _validator = new IntegrityValidator(this);
        }
        return _validator;
      }
    }

    #endregion Integrity validation data

  }  // class RequestSigner

}  // namespace Empiria.OnePoint.EFiling
