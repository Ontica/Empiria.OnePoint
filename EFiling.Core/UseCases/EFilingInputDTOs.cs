/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases                               *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Input Data Transfer Objects             *
*  Type     : EFiling Input Data Transfer Objects        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains output data transfer objects for electronic sign use cases.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.EFiling {


  public class Requester {

    [DataField("RequestedBy")]
    public string name {
      get; set;
    }


  //  [DataField("RequestExtData/requestedByData/email")]
    public string email {
      get; set;
    } = String.Empty;


//    [DataField("RequestExtData/requestedByData/phone")]
    public string phone {
      get; set;
    } = String.Empty;


  //  [DataField("RequestExtData/requestedByData/rfc")]
    public string rfc {
      get; set;
    } = String.Empty;

  }  // class Requester



  /// <summary>Create electronic filing request data.</summary>
  public class CreateEFilingRequestDTO {

    public string procedureType {
      get; set;
    }

    public Requester requestedBy {
      get; set;
    }

  }  // class CreateEFilingRequestDTO


}  // namespace Empiria.OnePoint.EFiling
