/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Response methods                        *
*  Type     : SignEventModels                              License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Response models for SignEvent entities.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Empiria.OnePoint.ESign.WebApi {

  /// <summary>Response models for SignEvent entities.</summary>
  static internal class SignEventModels {

    #region Response models

    static internal ICollection ToResponse(this IList<SignEvent> list) {
      ArrayList array = new ArrayList(list.Count);

      foreach (var signEvent in list) {
        var item = signEvent.ToResponse();

        array.Add(item);
      }
      return array;
    }

    static internal object ToResponse(this SignEvent signEvent) {
      return new {
        uid = signEvent.UID,
        eventType = signEvent.EventType,
        timeStamp = signEvent.Timestamp,
        signRequest = signEvent.SignRequest.ToResponse()
      };
    }

    #endregion Response models

  }  // class SignEventModels

}  // namespace Empiria.OnePoint.WebApi

