/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.Integration.dll           Pattern   : Adapter Interface                       *
*  Type     : IProcedure                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Defines a procedure.                                                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.OnePoint.EFiling {

  /// <summary>Defines a procedure.</summary>
  public interface IProcedure {

    string UID {
      get;
    }

  }  // interface IProcedure

}
