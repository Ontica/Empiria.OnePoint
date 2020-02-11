/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.EFiling.WebApi.dll        Pattern   : Web Api Controller                      *
*  Type     : EFilingDocumentsController                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api controller that provides input and output e-documents for filing requests.             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

namespace Empiria.OnePoint.EFiling.WebApi {

  /// <summary>Web api controller that provides input and output e-documents for filing requests.</summary>
  [WebApiAuthorizationFilter(WebApiClaimType.ClientApp_Controller, "Electronic.Filing.Client.Application")]
  public class EFilingDocumentsController : WebApiController {

    #region Query API


    [HttpGet]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID}/output-documents")]
    public CollectionModel GetOutputDocuments([FromUri] string filingRequestUID) {
      try {
        FixedList<EFilingDocumentDTO> documents = EFilingDocumentsUseCases.GetOutputDocuments(filingRequestUID);

        return GenerateResponse(documents);

      } catch (Exception e) {
        throw base.CreateHttpException(e);
      }
    }


    #endregion Query API


    #region Utility methods


    private CollectionModel GenerateResponse(FixedList<EFilingDocumentDTO> list) {
      return new CollectionModel(this.Request, list, typeof(EFilingDocumentDTO).FullName);
    }


    #endregion Utility methods

  }  // class EFilingDocumentsController

}  // namespace Empiria.OnePoint.EFiling.WebApi
