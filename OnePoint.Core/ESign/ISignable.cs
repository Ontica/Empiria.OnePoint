/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Dependency Inversion Interface          *
*  Type     : ISignable                                    License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a signable document.                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.ESign {

  /// <summary>Represents a signable document.</summary>
  public interface ISignable {

    string UID {
      get;
    }

    // ToDo: Must return a byte[]
    string SignInputData {
      get;
    }

  }  // interface ISignable

}  // namespace Empiria.OnePoint.ESign
