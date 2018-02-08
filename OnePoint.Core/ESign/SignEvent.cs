/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Information Holder                      *
*  Type     : SignEvent                                    License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a sign event that is the result or outcome of an electronic-sign operation.         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign {

  /// <summary>Represents a sign event that is the result or outcome of an electronic-sign operation.</summary>
  public class SignEvent {

    #region Constructors and parsers

    internal SignEvent(SignEventType eventType,
                       SignRequest signRequest,
                       string digitalSign) {

      this.EventType = eventType;
      this.SignRequest = signRequest;
      this.DigitalSign = digitalSign;

      this.EnsureIsValid();
    }

    #endregion Constructors and parsers

    #region Properties

    public SignEventType EventType {
      get;
    }

    public string UID {
      get;
    } = Guid.NewGuid().ToString();


    public SignRequest SignRequest {
      get;
    }


    public string DigitalSign {
      get;
    }


    public DateTime Timestamp {
      get;
      private set;
    } = DateTime.Now;

    #endregion Properties

    #region Methods

    private void EnsureIsValid() {

    }

    #endregion Methods

  }  // class SignEvent

}  // namespace Empiria.OnePoint.ESign
