/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : ESign Services                             Component : Interface adapter                       *
*  Assembly : Empiria.Land.ESign.dll                     Pattern   : Data Transfer Object                    *
*  Type     : ESignDTO                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO with OnePoint ESign document data.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign.Adapters {

  /// <summary>DTO with OnePoint ESign document data.</summary>
  public class OPSignDocumentEntryDto {

    
    public int TransactionId {
      get; set;
    }


    public int TrackId {
      get; set;
    }


    public string TransactionUID {
      get; set;
    }


    public string RequestedBy {
      get; set;
    }


    public string NextTransactionStatus {
      get; set;
    }

  } // class SignedDocumentEntryDto

} // namespace Empiria.OnePoint.ESign.Adapters
