/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Interface adapter                       *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Mapper                                  *
*  Type     : ESignDocumentMapper                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data mapping methods for electronic sign entities.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign.Adapters {

  /// <summary>Data mapping methods for electronic sign entities.</summary>
  static internal class ESignDocumentMapper {

    #region Public methods

    static internal FixedList<OnePointESignDocumentDto> Map(FixedList<SignedDocumentEntry> signedDocuments) {

      FixedList<OnePointESignDocumentDto> mappedDocuments = MapDocuments(signedDocuments);

      return mappedDocuments;
    }


    #endregion


    #region Private methods


    static private FixedList<OnePointESignDocumentDto> MapDocuments(
                    FixedList<SignedDocumentEntry> signedDocuments) {

      var requests = signedDocuments.Select((x) => MapDocument(x));

      return new FixedList<OnePointESignDocumentDto>(requests);
    }


    static private OnePointESignDocumentDto MapDocument(SignedDocumentEntry x) {
      var dto = new OnePointESignDocumentDto();

      dto.TransactionId = x.TransactionId;
      dto.TransactionUID = x.TransactionUID;
      dto.DocumentType = x.DocumentType;
      dto.TransactionType = x.TransactionType;
      dto.InternalControlNo = x.InternalControlNo;
      dto.AssignedById = x.AssignedById;
      dto.AssignedBy = x.AssignedBy;
      dto.RequestedBy = x.RequestedBy;
      dto.TransactionStatus = x.TransactionStatus;
      dto.RecorderOfficeId = x.RecorderOfficeId;
      dto.PresentationTime = x.PresentationTime;

      return dto;
    }

    #endregion
  } // class ESignDocumentMapper

} // namespace Empiria.OnePoint.ESign.Adapters
