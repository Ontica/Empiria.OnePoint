/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Services                                *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Service class                           *
*  Type     : ESignDocumentsService                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Service to manage ESign documents.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign.Services {

  /// <summary>Service to manage ESign documents.</summary>
  static public class ESignDocumentsService {


    #region Public methods

    static public FixedList<SignRequestDTO> GenerateESignDocumentsList(SignTaskDTO signTaskDTO) {

      //TODO conectar al servicio de seguriSign para mandar a firmar documentos
      //y retornar documentos firmados y status

      FixedList<SignRequestDTO> documentsToSign = ESignService(signTaskDTO);

      return new FixedList<SignRequestDTO>(); //TODO mapper
    }


    #endregion Public methods


    #region Private methods


    static private FixedList<SignRequestDTO> ESignService(SignTaskDTO signTaskDTO) {

      var eSignClientService = new ESignClientService();

      SignRequestQuery requestQuery = MapToSignRequestQuery(signTaskDTO);

      return eSignClientService.SeguriSignService(requestQuery);

    }


    static private SignRequestQuery MapToSignRequestQuery(SignTaskDTO signTaskDTO) {

      return new SignRequestQuery {
        UserName = "catastro2022",
        Password = "12345678a",
        SignRequests = signTaskDTO.signRequests
      };
    }


    #endregion Private methods
  }


  internal class SignRequestQuery {

    public string UserName {
      get; set;
    }


    public string Password {
      get; set;
    }


    public FixedList<string> SignRequests {
      get; set;
    }


  }


}
