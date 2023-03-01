﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Use cases                               *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Use Cases class                         *
*  Type     : ESignDocumentUseCases                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic signature services.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using Empiria.OnePoint.ESign.Adapters;

namespace Empiria.OnePoint.ESign {

  /// <summary>namespace Empiria.OnePoint.ESign.UseCases</summary>
  public class ESignDocumentUseCases {


    #region Use cases

      
    public FixedList<OnePointSignDocumentDto> SignedDocuments(int recorderOfficeId) {

      var signTaskProcessor = new SignTaskProcessor();

      FixedList<SignedDocumentEntry> signedDocuments = signTaskProcessor.GetSignedDocuments(recorderOfficeId);

      //TODO MAP ENTRY INTO SIGNDOCUMENTDTO

      return new FixedList<OnePointSignDocumentDto>();
      
    }

    #endregion Use cases


  } // class ESignDocumentUseCases

} // namespace Empiria.OnePoint.ESign.UseCases
