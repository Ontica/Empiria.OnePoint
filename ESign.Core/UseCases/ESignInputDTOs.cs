/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Use cases                               *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Input Data Transfer Objects             *
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



  public class DocumentPostDTO {

    public string uid {
      get; set;
    } = String.Empty;


    public string documentType {
      get; set;
    } = String.Empty;


    public string documentNo {
      get; set;
    } = String.Empty;


    public string transactionNo {
      get; set;
    } = String.Empty;


    public string requestedBy {
      get; set;
    } = String.Empty;


    public DateTime requestedTime {
      get; set;
    }


    public string description {
      get; set;
    } = String.Empty;


    public string signInputData {
      get; set;
    } = String.Empty;


    public int postedById {
      get; set;
    } = -1;


    public DateTime postingTime {
      get; set;
    }


    public SignRequestPostDTO[] signRequests {
      get; set;
    } = new SignRequestPostDTO[0];


  }



  public class SignRequestPostDTO {


    public int requestedById {
      get; set;
    }


    public int signerId {
      get; set;
    }


    public string signatureKind {
      get; set;
    }


  }

}  // namespace Empiria.OnePoint.ESign
