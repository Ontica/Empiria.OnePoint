/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : OnePoint Web API                        *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Response methods                        *
*  Type     : FilingResponseModel                          License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Response models for IFiling objects.                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.WebApi {

  /// <summary>Response models for IFiling objects.</summary>
  static internal class FilingResponseModel {

    static internal object ToResponse(this IFiling filing) {
      return new {
        uid = filing.UID
      };
    }

  }  // class FilingResponseModel

}  // namespace Empiria.OnePoint.WebApi
