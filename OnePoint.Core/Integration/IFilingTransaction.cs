/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing                          Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Application Service interface           *
*  Type     : IFilingTransaction                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Acts as an abstract class that holds data for an external transaction request, that may be     *
*             integrated into an Empiria Land transaction.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.EFiling {

  public interface IFilingTransaction {

    string UID {
      get;
    }


    DateTime PresentationTime {
      get;
    }


    //KeyValue Status {
    //  get;
    //}


  }  // IFilingTransaction

}  // namespace Empiria.OnePoint.EFiling
