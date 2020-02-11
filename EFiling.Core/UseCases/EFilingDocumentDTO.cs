/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : EFilingDocumentsUseCases                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implement documentation services for electronic filing requests.                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.EFiling {

  public class EFilingDocumentDTO {

    public string uid {
      get; set;
    } = String.Empty;


    public string type {
      get; set;
    } = String.Empty;


    public string typeName {
      get; set;
    } = String.Empty;


    public string name {
      get; set;
    } = String.Empty;


    public string contentType {
      get; set;
    } = String.Empty;


    public string uri {
      get; set;
    } = String.Empty;


  }  // EFilingDocumentDTO

}  // namespace Empiria.OnePoint.EFiling
