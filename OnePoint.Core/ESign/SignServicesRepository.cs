/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Repository                              *
*  Type     : SignServicesRepository                       License : Please read LICENSE.txt file            *
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
  static public class SignServicesRepository {

    #region SignRequest Query methods

    static public FixedList<SignRequest> GetPendingSignRequests(Contact contact,
                                                                string filter, string sort) {
      if (String.IsNullOrWhiteSpace(filter)) {
        filter = GeneralDataOperations.AllRecordsFilter;
      }
      var op = DataOperation.Parse("@qryEOPSignRequestsForContactInStatus",
                                   contact.Id, (char) SignStatus.Pending, filter);

      return DataReader.GetFixedList<SignRequest>(op);
    }


    static public FixedList<SignRequest> GetRefusedRequests(Contact contact,
                                                             string filter, string sort) {
      if (String.IsNullOrWhiteSpace(filter)) {
        filter = GeneralDataOperations.AllRecordsFilter;
      }
      var op = DataOperation.Parse("@qryEOPSignRequestsForContactInStatus",
                                   contact.Id, (char) SignStatus.Refused, filter);

      return DataReader.GetFixedList<SignRequest>(op);
    }


    static public FixedList<SignRequest> GetSignedRequests(Contact contact,
                                                           string filter, string sort) {
      if (String.IsNullOrWhiteSpace(filter)) {
        filter = GeneralDataOperations.AllRecordsFilter;
      }
      var op = DataOperation.Parse("@qryEOPSignRequestsForContactInStatus",
                                   contact.Id, (char) SignStatus.Signed, filter);

      return DataReader.GetFixedList<SignRequest>(op);
    }

    #endregion SignRequest Query methods

    #region Command internal methods

    static internal void AppendSignEvent(SignEvent o) {
      var op = DataOperation.Parse("apdEOPSignEvent", o.Id, o.UID,
                                   o.SignRequest.Id, (char) o.EventType,
                                   o.DigitalSign, o.Timestamp,
                                   o.Integrity.GetUpdatedHashCode());

      DataWriter.Execute(op);
    }


    static internal void WriteDocument(SignableDocument o) {
      throw new NotImplementedException();
    }


    static internal void WriteSignRequest(SignRequest o) {
      var op = DataOperation.Parse("writeEOPSignRequest", o.Id, o.UID,
                    o.RequestedBy.Id, o.RequestedTime, o.RequestedTo.Id,
                    o.Document.Id, o.SignatureKind, o.ExtensionData.ToString(),
                    (char) o.SignStatus, o.SignTime, o.DigitalSign,
                    o.Integrity.GetUpdatedHashCode());

      DataWriter.Execute(op);
    }

    #endregion Command internal methods

  } // class SignServicesRepository

} // namespace Empiria.OnePoint.ESign
