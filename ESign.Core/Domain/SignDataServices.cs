﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Domain                                  *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Data services                           *
*  Type     : SignDataServices                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write methods for involved entities in electronic-sign services.        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Data;

namespace Empiria.OnePoint.ESign {

  /// <summary>Provides data read and write methods for involved entities
  /// in electronic-sign services.</summary>
  static internal class SignDataServices {

    #region Query methods


    static internal FixedList<SignRequest> GetESignRequests(IContact requestedTo,
                                                            string keywords = "") {
      string filter = GetSignRequestKeywordsFilter(keywords);

      var op = DataOperation.Parse("@qryEOPSignRequestsForContact",
                                    requestedTo.Id, filter);

      return DataReader.GetFixedList<SignRequest>(op);
    }


    static internal FixedList<SignRequest> GetESignRequests(IContact requestedTo, SignStatus status,
                                                            string keywords = "") {
      string filter = GetSignRequestKeywordsFilter(keywords);

      var op = DataOperation.Parse("@qryEOPSignRequestsForContactInStatus",
                                   requestedTo.Id, (char) status, filter);

      return DataReader.GetFixedList<SignRequest>(op);
    }


    static internal FixedList<SignEvent> GetLastESignEvents(IContact requestedTo,
                                                            string keywords = "") {
      string filter = GetSignRequestKeywordsFilter(keywords);

      var op = DataOperation.Parse("@qryEOPSignEventsForSigner",
                                    requestedTo.Id, filter);

      return DataReader.GetFixedList<SignEvent>(op);
    }


    static internal SignRequest GetESignRequestByDocumentNo(string documentNo) {
      var op = DataOperation.Parse("@getEOPSignRequestByDocumentNo", documentNo);

      return DataReader.GetObject<SignRequest>(op);
    }


    #endregion Query methods


    #region Command methods


    static internal void AppendSignEvent(SignEvent o) {
      var op = DataOperation.Parse("apdEOPSignEvent", o.Id, o.UID,
                                   o.SignRequest.Id, (char) o.EventType,
                                   o.DigitalSign, o.Timestamp,
                                   o.Integrity.GetUpdatedHashCode());

      DataWriter.Execute(op);
    }


    static internal void WriteDocument(SignableDocument o) {
      var op = DataOperation.Parse("writeEOPSignableDocument", o.Id, o.UID,
                    o.DocumentType, o.TransactionNo, o.DocumentNo,
                    o.Description, o.RequestedBy, o.RequestedTime,
                    o.SignInputData, o.ExtensionData.ToString(),
                    o.Keywords, o.PostingTime, o.PostedBy.Id, (char) o.Status,
                    o.Integrity.GetUpdatedHashCode());

      DataWriter.Execute(op);
    }


    static internal void WriteSignRequest(SignRequest o) {
      var op = DataOperation.Parse("writeEOPSignRequest", o.Id, o.UID,
                    o.RequestedBy.Id, o.RequestedTime, o.RequestedTo.Id,
                    o.Document.Id, o.SignatureKind, o.ExtensionData.ToString(),
                    (char) o.SignStatus, o.SignTime, o.DigitalSign,
                    o.Integrity.GetUpdatedHashCode());

      DataWriter.Execute(op);
    }

    #endregion Command methods


    #region Utility methods


    static private string GetSignRequestKeywordsFilter(string keywords) {
      string filter = SearchExpression.AllRecordsFilter;

      if (!String.IsNullOrWhiteSpace(keywords)) {
        filter = SearchExpression.ParseAndLike("Keywords", EmpiriaString.BuildKeywords(keywords));
      }
      return filter;
    }


    #endregion Utility methods

  } // class SignDataServices

} // namespace Empiria.OnePoint.ESign
