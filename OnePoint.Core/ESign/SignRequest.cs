/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Empiria BaseObject Type                 *
*  Type     : SignRequest                                  License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a document's sign request.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Json;
using Empiria.Security;

namespace Empiria.OnePoint.ESign {

  /// <summary>Represents a document's sign request.</summary>
  public class SignRequest : BaseObject, IProtected {

    #region Constructors and parsers

    private SignRequest() {
      // Required by Empiria Framework.
    }


    static public SignRequest Parse(int id) {
      return BaseObject.ParseId<SignRequest>(id);
    }


    static public SignRequest Parse(string uid) {
      return BaseObject.ParseKey<SignRequest>(uid);
    }


    static public SignRequest Empty {
      get {
        return BaseObject.ParseEmpty<SignRequest>();
      }
    }

    #endregion Constructors and parsers

    #region Public properties

    [DataField("UID")]
    public string UID {
      get;
      private set;
    } = Guid.NewGuid().ToString();


    [DataField("SignableDocumentId")]
    public SignableDocument Document {
      get;
      private set;
    }


    [DataField("RequestedById")]
    public Contact RequestedBy {
      get;
      private set;
    }


    [DataField("RequestedTime")]
    public DateTime RequestedTime {
      get;
      private set;
    } = DateTime.Now;


    [DataField("SignatureKind")]
    public string SignatureKind {
      get;
      private set;
    }


    [DataField("ExtData")]
    internal JsonObject ExtensionData {
      get;
      private set;
    }


    [DataField("RequestedToId")]
    public Contact RequestedTo {
      get;
      private set;
    }


    [DataField("SignStatus", Default = SignStatus.Pending)]
    public SignStatus SignStatus {
      get;
      private set;
    }


    [DataField("SignTime")]
    public DateTime SignTime {
      get;
      private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("DigitalSign")]
    public string DigitalSign {
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
          1, "Id", this.Id, "UID", this.UID, "SignableDocumentId", this.Document.Id,
          "RequestedById", this.RequestedBy.Id, "RequestedTime", this.RequestedTime,
          "RequestedToId", this.RequestedTo.Id, "ExtData", this.ExtensionData.ToString(),
          "SignatureKind", this.SignatureKind, "RequestedToId", this.RequestedTo.Id,
          "SignInputData", this.Document.SignInputData, "DigitalSign", this.DigitalSign,
          "SignTime", this.SignTime
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


    internal SignEvent Sign(string digitalSign) {
      Assertion.Assert(this.SignStatus == SignStatus.Pending,
                       "Sign is allowed only to requests in pending status.");

      // ToDo: Ensure digitalSign is valid for RequestedTo user and Document.SignInputData

      var signEvent = new SignEvent(SignEventType.Signed, this, digitalSign);

      this.UpdateStatus(signEvent);

      return signEvent;
    }


    internal SignEvent Revoke() {
      Assertion.Assert(this.SignStatus == SignStatus.Signed,
                       "Revoke is allowed only to requests with signed status.");

      var signEvent = new SignEvent(SignEventType.Revoked, this);

      this.UpdateStatus(signEvent);

      return signEvent;
    }


    internal SignEvent Refuse() {
      Assertion.Assert(this.SignStatus == SignStatus.Pending,
                       "Sign refuse is allowed only to requests with pending status.");

      var signEvent = new SignEvent(SignEventType.Refused, this);

      this.UpdateStatus(signEvent);

      return signEvent;
    }


    internal SignEvent Unrefuse() {
      Assertion.Assert(this.SignStatus == SignStatus.Refused,
                       "Unrefuse is allowed only to requests with refused status.");

      var signEvent = new SignEvent(SignEventType.Unrefused, this);

      this.UpdateStatus(signEvent);

      return signEvent;
    }


    protected override void OnSave() {
      SignServicesRepository.WriteSignRequest(this);
    }


    private void UpdateStatus(SignEvent signEvent) {
      if (signEvent.EventType == SignEventType.Signed) {
        this.SignStatus = SignStatus.Signed;
        this.DigitalSign = signEvent.DigitalSign;
        this.SignTime = signEvent.Timestamp;

      } else if (signEvent.EventType == SignEventType.Revoked) {
        this.SignStatus = SignStatus.Pending;
        this.DigitalSign = String.Empty;
        this.SignTime = DateTime.MaxValue;

      } else if (signEvent.EventType == SignEventType.Refused) {
        this.SignStatus = SignStatus.Refused;
        this.DigitalSign = String.Empty;
        this.SignTime = signEvent.Timestamp;

      } else if (signEvent.EventType == SignEventType.Unrefused) {
        this.SignStatus = SignStatus.Pending;
        this.DigitalSign = String.Empty;
        this.SignTime = signEvent.Timestamp;

      }
    }

    #endregion Public methods

  } // class SignRequest

} // namespace Empiria.OnePoint.ESign
