/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Repository                              *
*  Type     : SignableDocumentsRepository                  License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write methods for signable documents.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Data;

namespace Empiria.OnePoint.ESign {

  /// <summary>Provides data read and write methods for signable documents.</summary>
  static public class SignableDocumentsRepository {

    #region Query methods

    static public FixedList<SignableDocument> GetDocumentsToSign(Contact contact,
                                                                 string filter, string sort) {
      var op = DataOperation.Parse("@qryEOPESignDocumentsForContact",
                                   contact.Id, (char) ESignStatus.Pending);

      return DataReader.GetFixedList<SignableDocument>(op);
    }


    static public FixedList<SignableDocument> GetRefusedDocuments(Contact contact,
                                                                  string filter, string sort) {
      var op = DataOperation.Parse("@qryEOPESignDocumentsForContact",
                                   contact.Id, (char) ESignStatus.Refused);

      return DataReader.GetFixedList<SignableDocument>(op);
    }


    static public FixedList<SignableDocument> GetSignedDocuments(Contact contact,
                                                                 string filter, string sort) {
      var op = DataOperation.Parse("@qryEOPESignDocumentsForContact",
                                   contact.Id, (char) ESignStatus.Signed);

      return DataReader.GetFixedList<SignableDocument>(op);
    }

    #endregion Query methods

    #region Command internal methods

    static internal void WriteDocument(SignableDocument o) {
      throw new NotImplementedException();
    }

    #endregion Command internal methods

  } // class SignableDocumentsRepository

} // namespace Empiria.OnePoint.ESign
