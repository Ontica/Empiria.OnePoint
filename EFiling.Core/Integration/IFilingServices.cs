/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.Integration.dll           Pattern   : Service interface                       *
*  Type     : IFilingServices                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Interface used to gain access electronic filing services.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Interface used to gain access electronic filing services.</summary>
  public interface IFilingServices {


    EFilingRequest GetEFilingRequest(string filingRequestUID);


    Task NotifyEvent(string filingRequestUID, string eventName);


  }  // interface IFilingServices

}  // namespace Empiria.OnePoint.EFiling
