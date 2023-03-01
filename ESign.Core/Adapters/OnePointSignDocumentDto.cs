/* Empiria OnePoint *******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Interface adapter                       *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Data Transfer Object                    *
*  Type     : OnePointSignDocumentDto                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO with OnePoint ESign document data.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign.Adapters {

  /// <summary>DTO with OnePoint ESign document data.</summary>
  public class OnePointSignDocumentDto {


    public int TransactionId {
      get; set;
    }


    public string TransactionUID {
      get; set;
    }


    public string DocumentType {
      get; set;
    }


    public string TransactionType {
      get; set;
    }


    public string InternalControlNo {
      get; set;
    }


    public string AssignedById {
      get; set;
    }


    public string AssignedBy {
      get; set;
    }


    public string RequestedBy {
      get; set;
    }


    public string TransactionStatus {
      get; set;
    }


    public int RecorderOfficeId {
      get; set;
    }


    public DateTime PresentationTime {
      get; set;
    }

  } // class OnePointSignDocumentDto

} // namespace Empiria.OnePoint.ESign.Adapters
