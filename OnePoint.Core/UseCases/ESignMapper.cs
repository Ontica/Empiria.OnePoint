/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Use cases                               *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Use Cases Data Mapper                   *
*  Type     : ESignMapper                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data mapping methods between electronic sign entities and their data transfer objects.         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;

namespace Empiria.OnePoint.ESign {

  /// <summary>Data mapping methods between electronic sign entities and their data transfer objects.</summary>
  static internal class ESignMapper {


    static internal FixedList<SignRequestDTO> Map(FixedList<SignRequest> source) {
      return new FixedList<SignRequestDTO>(source.Select((x) => Map(x)));
    }


    static internal FixedList<SignEventDTO> Map(FixedList<SignEvent> source) {
      return new FixedList<SignEventDTO>(source.Select((x) => Map(x)));
    }


    static internal FixedList<SignRequest> Map(FixedList<string> source) {
      var enumerable = source.Select(uid => SignRequest.Parse(uid));

      return new FixedList<SignRequest>(enumerable);
    }


    static internal SignRequestDTO Map(SignRequest request) {
      SignableDocument document = request.Document;

      return new SignRequestDTO() {
        uid = request.UID,
        requestedBy = request.RequestedBy.Alias,
        requestedTime = request.RequestedTime,
        signStatus = request.SignStatus.ToString(),
        signatureKind = request.SignatureKind,
        digitalSignature = request.DigitalSign,

        document = Map(document),

        filing = new SignRequestFilingDTO() {
          filingNo = document.TransactionNo,
          filingTime = document.RequestedTime,
          filedBy = document.RequestedBy,
          postedBy = document.PostedBy.Alias,
        },
      };
    }


    static internal SignableDocumentDTO Map(SignableDocument document) {
      return new SignableDocumentDTO() {
        uid = document.UID,
        type = document.DocumentType,
        documentNo = document.DocumentNo,
        description = document.Description,
        uri = document.Uri
      };
    }


    static internal SignEventDTO Map(SignEvent signEvent) {
      return new SignEventDTO() {
        uid = signEvent.UID,
        eventType = signEvent.EventType.ToString(),
        timeStamp = signEvent.Timestamp,
        signRequest = Map(signEvent.SignRequest)
      };
    }


    static internal SignTask Map(SignTaskDTO signTaskDTO) {
      FixedList<SignRequest> signRequests = Map(signTaskDTO.signRequests);
      SignCredentials credentials = Map(signTaskDTO.credentials);

      return new SignTask(signTaskDTO.eventType, signRequests, credentials);
    }


    static internal SignCredentials Map(SignCredentialsDTO credentialsDTO) {
      return new SignCredentials(credentialsDTO.password);
    }


  }  // partial class ESignMapper

}  // namespace Empiria.OnePoint.ESign
