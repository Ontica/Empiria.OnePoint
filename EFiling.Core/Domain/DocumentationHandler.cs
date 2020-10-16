/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Service provider                        *
*  Type     : DocumentationHandler                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Collaborates with EFilingRequest entities controlling their documentation elements.            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Storage.Documents;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Collaborates with EFilingRequest entities controlling their documentation elements.</summary>
  internal class DocumentationHandler {

    private readonly EFilingRequest _request;
    private readonly Documenter _documenter;

    public DocumentationHandler(EFilingRequest request) {
      _request = request;
      _documenter = new Documenter(_request.Procedure, request);
    }


    public Documentation Documentation {
      get {
        return this._documenter.Documentation;
      }
    }

  }  // class DocumentationHandler

}  // namespace Empiria.OnePoint.EFiling
