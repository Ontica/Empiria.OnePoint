/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Response methods                        *
*  Type     : SignRequestModels                            License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Response models for SignRequest entities.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Empiria.OnePoint.ESign.WebApi {

  /// <summary>Response models for SignRequest entities.</summary>
  static internal class SignRequestModels {

    #region Response models

    static internal ICollection ToResponse(this IList<SignRequest> list) {
      ArrayList array = new ArrayList(list.Count);

      foreach (var request in list) {
        var item = request.ToResponse();

        array.Add(item);
      }
      return array;
    }

    static internal object ToResponse(this SignRequest request) {
      SignableDocument document = request.Document;
      return new {
        uid = request.UID,
        requestedBy = request.RequestedBy.Alias,
        requestedTime = request.RequestedTime,
        signStatus = request.SignStatus,
        signatureKind = request.SignatureKind,
        digitalSignature = request.DigitalSign,
        document = document.ToResponse(),
        filing = new {
          filingNo = document.TransactionNo,
          filingTime = document.RequestedTime,
          filedBy = document.RequestedBy,
          postedBy = document.PostedBy.Alias,
        },
      };
    }

    #endregion Response models

  }  // class SignRequestModels

}  // namespace Empiria.OnePoint.WebApi

