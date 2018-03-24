﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Information Holder                      *
*  Type     : SignEvent                                    License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a sign event that is the result or outcome of an electronic-sign operation.         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security;

namespace Empiria.OnePoint.ESign {

  /// <summary>Represents a sign event that is the result or outcome of an electronic-sign operation.</summary>
  public class SignEvent : BaseObject, IProtected {

    #region Constructors and parsers

    private SignEvent() {
      // Required by Empiria Framework.
    }


    internal SignEvent(SignEventType eventType,
                       SignRequest signRequest,
                       string digitalSign = "") {

      this.EventType = eventType;
      this.SignRequest = signRequest;
      this.DigitalSign = digitalSign;

      this.EnsureIsValid();
    }


    static public SignEvent Parse(int id) {
      return BaseObject.ParseId<SignEvent>(id);
    }


    static public SignEvent Parse(string uid) {
      return BaseObject.ParseKey<SignEvent>(uid);
    }

    #endregion Constructors and parsers

    #region Properties

    [DataField("UID")]
    public string UID {
      get;
      private set;
    }


    [DataField("SignRequestId")]
    public SignRequest SignRequest {
      get;
      private set;
    }


    [DataField("EventType", Default = SignEventType.Empty)]
    public SignEventType EventType {
      get;
      private set;
    }


    [DataField("DigitalSign")]
    public string DigitalSign {
      get;
      private set;
    }


    [DataField("Timestamp", Default = "System.DateTime.Now")]
    public DateTime Timestamp {
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
          1, "Id", this.Id, "SignEventUID", this.UID,
          "SignRequestId", this.SignRequest.Id, "EventType", (char) this.EventType,
          "DigitalSign", this.DigitalSign, "Timestamp", this.Timestamp
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

    #endregion Properties

    #region Methods

    private void EnsureIsValid() {

    }


    protected override void OnSave() {
      if (this.IsNew) {
        this.UID = Guid.NewGuid().ToString();

        SignServicesRepository.AppendSignEvent(this);
      } else {
        throw new InvalidOperationException("Update SignEvent instances not allowed.");
      }
    }

    #endregion Methods

  }  // class SignEvent

}  // namespace Empiria.OnePoint.ESign