/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                     Component : Signer                                *
*  Assembly : Empiria.OnePoint.ESign.dll                   Pattern   : External service interface            *
*  Type     : IESignClient                                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides services for messages electronic signing.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.OnePoint.ESign.Providers {

  public interface IESignClient {

    string Sign(string message);

  }  // interface IESignClient

}  // namespace Empiria.OnePoint.ESign.Providers
