/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Use cases                               *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Use Cases class                         *
*  Type     : ESignUseCases                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic signature services.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Security;

namespace Empiria.OnePoint.ESign {

  /// <summary>Use cases that implements electronic signature services.</summary>
  static public class ESignUseCases {

    #region Use cases


    static public FixedList<SignRequestDTO> GetESignRequests(IContact requestedTo) {
      return GetESignRequests(requestedTo, String.Empty);
    }


    static public FixedList<SignRequestDTO> GetESignRequests(IContact requestedTo,
                                                             string keywords) {
      FixedList<SignRequest> r = SignDataServices.GetESignRequests(requestedTo, keywords);

      return ESignMapper.Map(r);
    }


    static public FixedList<SignRequestDTO> GetESignRequests(IContact requestedTo, SignStatus status) {
      return GetESignRequests(requestedTo, status, String.Empty);
    }


    static public FixedList<SignRequestDTO> GetESignRequests(IContact requestedTo, SignStatus status,
                                                             string keywords) {
      FixedList<SignRequest> r = SignDataServices.GetESignRequests(requestedTo, status, keywords);

      return ESignMapper.Map(r);
    }


    static public FixedList<SignEventDTO> GetLastESignEvents(IContact requestedTo) {
      return GetLastESignEvents(requestedTo, String.Empty);
    }


    static public FixedList<SignEventDTO> GetLastESignEvents(IContact requestedTo,
                                                             string keywords) {
      FixedList<SignEvent> r = SignDataServices.GetLastESignEvents(requestedTo, keywords);

      return ESignMapper.Map(r);
    }


    static public FixedList<SignRequestDTO> GetMyESignRequests(string keywords) {
      return GetESignRequests(GetCurrentUser(), keywords);
    }


    static public FixedList<SignRequestDTO> GetMyESignRequests(SignStatus status, string keywords) {
      return GetESignRequests(GetCurrentUser(), status, keywords);
    }


    static public FixedList<SignEventDTO> GetMyLastESignEvents() {
      return GetLastESignEvents(GetCurrentUser());
    }


    static public FixedList<SignEventDTO> GetMyLastESignEvents(string keywords) {
      return GetLastESignEvents(GetCurrentUser(), keywords);
    }


    static public SignRequestDTO GetESignRequestByDocumentNo(string documentNo) {
      SignRequest r = SignDataServices.GetESignRequestByDocumentNo(documentNo);

      return ESignMapper.Map(r);
    }


    static public FixedList<SignEventDTO> Sign(SignTaskDTO signTaskDTO) {
      return ExecuteSignTask(signTaskDTO, SignEventType.Signed);
    }


    static public FixedList<SignEventDTO> RefuseSign(SignTaskDTO signTaskDTO) {
      return ExecuteSignTask(signTaskDTO, SignEventType.Refused);
    }


    static public FixedList<SignEventDTO> RevokeSign(SignTaskDTO signTaskDTO) {
      return ExecuteSignTask(signTaskDTO, SignEventType.Revoked);
    }


    static public FixedList<SignEventDTO> UnrefuseSign(SignTaskDTO eSignTask) {
      return ExecuteSignTask(eSignTask, SignEventType.Unrefused);
    }


    #endregion Use cases


    #region Utility methods


    static public void EnsureValidSignTaskDTO(SignTaskDTO signTaskDTO) {
      Assertion.AssertObject(signTaskDTO, "signTaskDTO");

      Assertion.AssertObject(signTaskDTO.credentials,
                            "signTaskDTO.credentials");

      Assertion.AssertObject(signTaskDTO.credentials.password,
                             "signTaskDTO.credentials.password");

      Assertion.AssertObject(signTaskDTO.signRequests,
                             "signTaskDTO.signRequests");

      Assertion.Assert(signTaskDTO.signRequests.Count > 0,
                      "signTaskDTO.signRequests must be a no empty array");

    }


    static private FixedList<SignEventDTO> ExecuteSignTask(SignTaskDTO signTaskDTO,
                                                           SignEventType eventType) {
      EnsureValidSignTaskDTO(signTaskDTO);

      signTaskDTO.eventType = eventType;

      SignTask signTask = ESignMapper.Map(signTaskDTO);

      var signTaskProcessor = new SignTaskProcessor();

      FixedList<SignEvent> signEvents = signTaskProcessor.Execute(signTask);

      return ESignMapper.Map(signEvents);
    }


    static private Contact GetCurrentUser() {
      return EmpiriaUser.Current.AsContact();
    }


    #endregion Utility methods

  }  // class ESignUseCases

}  // namespace Empiria.OnePoint.ESign
