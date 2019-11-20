/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : EFilingUseCases                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic filing services.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

// using Empiria.OnePoint.ESign;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Use cases that implements electronic filing services.</summary>
  static public class EFilingUseCases {

    #region Use cases


    //static public EFilingRequest CreateApplicationForm(string filingRequestUID, JsonObject formData) {
    //  EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);
    //  Assertion.AssertObject(formData, "formData");

    //  var applicationForm = filingRequest.AddApplicationForm(formData);

    //  applicationForm.Save();

    //  return filingRequest;
    //}


    static public EFilingRequestDTO CreateEFilingRequest(CreateEFilingRequestDTO requestDTO) {
      Assertion.AssertObject(requestDTO, "requestDTO");

      var procedure = Procedure.Parse(requestDTO.procedureType);

      var filingRequest = new EFilingRequest(procedure, requestDTO.requestedBy);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO GeneratePaymentOrderForEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.GeneratePaymentOrder();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO GetEFilingRequest(string filingRequestUID) {
      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      return EFilingMapper.Map(filingRequest);
    }


    static public FixedList<EFilingRequestDTO> GetEFilingRequestListByStatus(EFilingRequestStatus status,
                                                                             string keywords) {
      var list = EFilingRequest.GetList(status, keywords);

      return EFilingMapper.Map(list);
    }


    static public EFilingRequestDTO RevokeEFilingRequestSign(string filingRequestUID,
                                                             JsonObject revokeSignData) {
      Assertion.AssertObject(revokeSignData, "revokeSignData");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.RevokeSign(revokeSignData);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO SendEFilingRequestToSign(string filingRequestUID) {
      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.SendToSign();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO SetPaymentReceipt(string filingRequestUID,
                                                      string receiptNo) {
      Assertion.AssertObject(receiptNo, "receiptNo");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.SetPaymentReceipt(receiptNo);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO SignEFilingRequest(string filingRequestUID,
                                                       JsonObject signInputData) {
      Assertion.AssertObject(signInputData, "signInputData");

      EFilingRequest filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.Sign(signInputData);

      //DocumentPostDTO document = BuildDocumentPostDTO(filingRequest);

      //SignRequestDTO signRequestDTO = ESignUseCases.PostDocument(document);

      //SignTaskDTO signTaskDTO = BuildSignTaskDTO(signRequestDTO, signInputData);

      //SignEventDTO signEvent = ESignUseCases.Sign(signTaskDTO)[0];

      //filingRequest.SetESignData(BuildFilingRequestSignData(signEvent));

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO SubmitEFilingRequest(string filingRequestUID) {
      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.Submit();

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO UpdateApplicationForm(string filingRequestUID, JsonObject json) {
      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.SetApplicationForm(json);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    static public EFilingRequestDTO UpdateEFilingRequest(string filingRequestUID,
                                                         Requester requestedBy) {
      Assertion.AssertObject(requestedBy, "requestedBy");

      var filingRequest = ParseEFilingRequest(filingRequestUID);

      filingRequest.Update(requestedBy);

      filingRequest.Save();

      return EFilingMapper.Map(filingRequest);
    }


    #endregion Use cases


    #region Utility methods


    //static private DocumentPostDTO BuildDocumentPostDTO(EFilingRequest filingRequest) {
    //  SignRequestPostDTO signRequestDTO = new SignRequestPostDTO() {
    //    requestedById = filingRequest.PostedBy.Id,
    //    signerId = filingRequest.Agent.Id,
    //    signatureKind = "Firma notario"
    //  };

    //  return new DocumentPostDTO() {
    //    documentType = filingRequest.Procedure.DisplayName,
    //    requestedBy = filingRequest.RequestedBy.name,
    //    requestedTime = filingRequest.PostingTime,
    //    signInputData = filingRequest.GetESignInputData(),
    //    postedById = filingRequest.PostedBy.Id,
    //    postingTime = DateTime.Now,
    //    signRequests = new SignRequestPostDTO[1]{ signRequestDTO }
    //  };
    //}


    //static private JsonObject BuildFilingRequestSignData(SignEventDTO signEvent) {
    //  var json = new JsonObject();

    //  json.Add("signature", signEvent.signRequest.digitalSignature);
    //  json.Add("timestamp", signEvent.timeStamp);

    //  return json;
    //}


    //static private SignTaskDTO BuildSignTaskDTO(SignRequestDTO signRequestDTO, JsonObject signInputData) {
    //  return new SignTaskDTO() {
    //    credentials = new SignCredentialsDTO() {
    //      password = signInputData.Get<string>("signToken")
    //    },
    //    signRequests = new FixedList<string>(new string[1] { signRequestDTO.uid })
    //  };
    //}


    static private EFilingRequest ParseEFilingRequest(string filingRequestUID) {
      Assertion.AssertObject(filingRequestUID, "filingRequestUID");

      var request = EFilingRequest.TryParse(filingRequestUID);

      if (request == null) {
        throw new ResourceNotFoundException("ElectronicFilingRequest.UID.NotFound",
                                            $"No tenemos registrada ninguna solicitud con identificador {filingRequestUID}.");
      }

      return request;
    }


    #endregion Utility methods


  }  // class EFilingUseCases

}  // namespace Empiria.OnePoint.EFiling
