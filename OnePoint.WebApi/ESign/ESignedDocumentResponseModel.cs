/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Response methods                        *
*  Type     : ESignedDocumentResponseModel                 License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Response models for E-signed documents.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Empiria.OnePoint.ESign.WebApi {

  /// <summary>Response models for E-signed documents.</summary>
  static internal class ESignedDocumentResponseModel {

    static internal ICollection ToResponse(this IList<SignableDocument> list) {
      ArrayList array = new ArrayList(list.Count);

      foreach (var document in list) {
        var item = document.ToResponse();

        array.Add(item);
      }
      return array;
    }

    static internal object ToResponse(this SignableDocument document) {
      return new {
        uid = document.UID,
        type = document.DocumentType,
        description = document.Description,
        requestedBy = document.RequestedBy,
        requestedTime = document.RequestedTime,
        postedBy = document.PostedBy.Alias,
      };
    }

  }  // class ESignedDocumentResponseModel

}  // namespace Empiria.OnePoint.WebApi

