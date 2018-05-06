/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Empiria BaseObject Type                 *
*  Type     : SignableDocument                             License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a signable document.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Security;

namespace Empiria.OnePoint.ESign {

  /// <summary>Represents a signable document.</summary>
  public class SignableDocument : BaseObject, ISignable, IProtected {

    #region Constructors and parsers

    private SignableDocument() {
      // Required by Empiria Framework.
    }

    static public SignableDocument Parse(int id) {
      return BaseObject.ParseId<SignableDocument>(id);
    }

    static public SignableDocument Parse(string uid) {
      return BaseObject.ParseKey<SignableDocument>(uid);
    }

    static public SignableDocument Empty {
      get {
        return BaseObject.ParseEmpty<SignableDocument>();
      }
    }

    #endregion Constructors and parsers

    #region Public properties


    [DataField("DocumentType")]
    public string DocumentType {
      get;
      private set;
    }


    [DataField("DocumentNo")]
    public string DocumentNo {
      get;
      private set;
    }


    [DataField("TransactionNo")]
    public string TransactionNo {
      get;
      private set;
    }


    [DataField("RequestedBy")]
    public string RequestedBy {
      get;
      private set;
    }


    [DataField("RequestedTime")]
    public DateTime RequestedTime {
      get;
      private set;
    }


    [DataField("Description")]
    public string Description {
      get;
      private set;
    }

    public string Uri {
      get {
        if (this.Status == SignStatus.Signed) {
          return "https://registropublico.tlaxcala.gob.mx/intranet/emitted.certificates/signed.pdf";
        } else {
          return "https://registropublico.tlaxcala.gob.mx/intranet/emitted.certificates/unsigned.pdf";
        }
      }
    }

    [DataField("SignInputData")]
    public string SignInputData {
      get;
      private set;
    }


    [DataField("ExtData")]
    internal JsonObject ExtensionData {
      get;
      private set;
    }


    internal string Keywords {
      get {
        return EmpiriaString.BuildKeywords(this.UID, this.DocumentType, this.RequestedBy, this.Description);
      }
    }


    [DataField("PostingTime")]
    public DateTime PostingTime {
      get;
      private set;
    } = DateTime.Now;


    [DataField("PostedById")]
    public Contact PostedBy {
      get;
      private set;
    }


    [DataField("SignStatus", Default = SignStatus.Pending)]
    public SignStatus Status {
      get;
      private set;
    }


    int IProtected.CurrentDataIntegrityVersion {
      get {
        return 1;
      }
    }

    object[] IProtected.GetDataIntegrityFieldValues(int version) {
      if (version == 1) {
        return new object[] {
          1, "Id", this.Id, "DocumentUID", this.UID, "DocumentType", this.DocumentType,
          "RequestedBy", this.RequestedBy, "RequestedTime", this.RequestedTime,
          "Description", this.Description, "SignInputData", this.SignInputData,
          "ExtData", this.ExtensionData.ToString(), "PostingTime", this.PostingTime,
          "PostedById", this.PostedBy.Id, "Status", this.Status
        };
      }
      throw new SecurityException(SecurityException.Msg.WrongDIFVersionRequested, version);
    }

    private IntegrityValidator _validator = null;
    public IntegrityValidator Integrity {
      get {
        if (_validator == null) {
          _validator = new IntegrityValidator(this);
        }
        return _validator;
      }
    }

    #endregion Public properties

    #region Public methods

    protected override void OnSave() {
      if (base.IsNew) {
        this.PostedBy = Contact.Parse(ExecutionServer.CurrentUserId);
        this.PostingTime = DateTime.Now;
      }
      SignServicesRepository.WriteDocument(this);
    }

    #endregion Public methods

  } // class SignableDocument

} // namespace Empiria.OnePoint.ESign
