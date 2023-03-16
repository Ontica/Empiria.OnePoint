/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Services                                *
*  Assembly : Empiria.OnePoint.ESign.dll                 Pattern   : Service class                           *
*  Type     : ESignClientService                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Service to communicate with ESign clients services.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign.Services {


  /// <summary>Service to communicate with ESign clients services.</summary>
  internal class ESignClientService {


    internal FixedList<SignRequestDTO> SeguriSignService(SignTaskDTO signTaskDTO) {


      return new FixedList<SignRequestDTO>();
    }


  }
}
