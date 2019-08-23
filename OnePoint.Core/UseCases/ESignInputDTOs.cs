/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Use cases                               *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Input Data Transfer Objects             *
*  Type     : ESign Input Data Transfer Objects          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains output data transfer objects for electronic sign use cases.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign {


  /// <summary>Signature credentials DTO.</summary>
  public class SignCredentialsDTO {

    public string password {
      get; set;
    }

  }  // class SignCredentialsDTO



  /// <summary>Sign task DTO.</summary>
  public class SignTaskDTO {

    internal SignEventType eventType {
      get; set;
    } = SignEventType.Empty;


    public SignCredentialsDTO credentials {
      get; set;
    }
    public FixedList<string> signRequests {
      get; set;
    }

  }  // class SignTaskDTO


}  // namespace Empiria.OnePoint.ESign
