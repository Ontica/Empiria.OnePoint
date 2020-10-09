/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases                               *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Input Data Transfer Objects             *
*  Type     : EFiling Input Data Transfer Objects        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains output data transfer objects for electronic sign use cases.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.OnePoint.EFiling.UseCases {

  /// <summary>Create electronic filing request data.</summary>
  public class CreateEFilingRequestDto {

    public string ProcedureType {
      get; set;
    }

    public RequesterDto RequestedBy {
      get; set;
    }

  }  // class CreateEFilingRequestDto


}  // namespace Empiria.OnePoint.EFiling.UseCases
