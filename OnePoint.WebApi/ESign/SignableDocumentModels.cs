/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Response methods                        *
*  Type     : SignableDocumentModels                       License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Response models for SignableDocument entities.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Empiria.OnePoint.ESign.WebApi {

  /// <summary>Response models for SignableDocument entities.</summary>
  static internal class SignableDocumentModels {

    #region Response models

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
        documentNo = document.DocumentNo,
        description = document.Description,
        uri = document.Uri
      };
    }

    #endregion Response models

  }  // class SignableDocumentModels

}  // namespace Empiria.OnePoint.WebApi

