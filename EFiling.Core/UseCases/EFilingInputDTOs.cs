/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases                               *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Input Data Transfer Objects             *
*  Type     : EFiling Input Data Transfer Objects        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains output data transfer objects for electronic sign use cases.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.OnePoint.EFiling {

  /// <summary>Create electronic filing request data.</summary>
  public class CreateEFilingRequestDTO {

    public string procedureType {
      get; set;
    }

    public Requester requestedBy {
      get; set;
    }

  }  // class CreateEFilingRequestDTO


}  // namespace Empiria.OnePoint.EFiling
