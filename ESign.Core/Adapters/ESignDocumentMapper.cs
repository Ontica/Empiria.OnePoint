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

    static internal FixedList<object> Map(FixedList<object> signedDocuments) {

      FixedList<object> mappedDocuments = MapDocuments(signedDocuments);

      return mappedDocuments;
    }


    #endregion


    #region Private methods


    static private FixedList<object> MapDocuments(
                    FixedList<object> signedDocuments) {

      var requests = signedDocuments.Select((x) => MapDocument(x));

      return new FixedList<object>(requests);
    }


    static private object MapDocument(object x) {
      var dto = new object();

      return dto;
    }

    #endregion
  } // class ESignDocumentMapper

} // namespace Empiria.OnePoint.ESign.Adapters
