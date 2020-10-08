/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.Integration.dll           Pattern   : Adapter Data Interface                  *
*  Type     : IFilingTransaction                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds information about an electronical filing transaction.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Holds information about an electronical filing transaction.</summary>
  public interface IFilingTransaction {

    int Id {
      get;
    }

    string UID {
      get;
    }

    string StatusName {
      get;
    }

    DateTime PresentationTime {
      get;
    }


  }  // IFilingTransaction.Integration

}  // namespace Empiria.OnePoint.EFiling
