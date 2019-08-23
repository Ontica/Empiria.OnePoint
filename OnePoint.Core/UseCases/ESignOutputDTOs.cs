/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Use cases                               *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Output Data Transfer Objects            *
*  Type     : ESign Data Transfer Objects                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains output data transfer objects for electronic sign use cases.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign {

  /// <summary>Signable document DTO.</summary>
  public class SignableDocumentDTO {

    public string uid {
      get;
      internal set;
    }

    public string type {
      get;
      internal set;
    }

    public string documentNo {
      get;
      internal set;
    }

    public string description {
      get;
      internal set;
    }

    public string uri {
      get;
      internal set;
    }

  }  // class SignableDocumentDTO




  /// <summary>Sign event DTO.</summary>
  public class SignEventDTO {

    public string uid {
      get;
      internal set;
    }

    public string eventType {
      get;
      internal set;
    }

    public DateTime timeStamp {
      get;
      internal set;
    }

    public SignRequestDTO signRequest {
      get;
      internal set;
    }

  }   //class SignEventDTO


  /// <summary>Sign request DTO.</summary>
  public class SignRequestDTO {

    public string uid {
      get;
      internal set;
    }

    public string requestedBy {
      get;
      internal set;
    }

    public DateTime requestedTime {
      get;
      internal set;
    }

    public string signStatus {
      get;
      internal set;
    }

    public string signatureKind {
      get;
      internal set;
    }

    public string digitalSignature {
      get;
      internal set;
    }

    public SignableDocumentDTO document {
      get;
      internal set;
    }

    public SignRequestFilingDTO filing {
      get;
      internal set;
    }

  }  // class SignRequestDTO



  /// <summary>Sign request filing DTO.</summary>
  public class SignRequestFilingDTO {

    public string filingNo {
      get;
      internal set;
    }

    public DateTime filingTime {
      get;
      internal set;
    }

    public string filedBy {
      get;
      internal set;
    }

    public string postedBy {
      get;
      internal set;
    }

  }  // class SignRequestFilingDTO


}  // namespace Empiria.OnePoint.ESign
