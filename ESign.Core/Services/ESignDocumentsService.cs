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
      var eSignClientService = new ESignClientService();
      FixedList<SignRequestDTO> documentsToSign = eSignClientService.SeguriSignService(signTaskDTO);

      //TODO guardar el proceso de firma en las tablas de EOP...


      
      return new FixedList<SignRequestDTO>(); //TODO mapper
    }

    #endregion Public methods


  }
}
