/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.EFiling.WebApi.dll        Pattern   : Web Api Controller                      *
*  Type     : DocumentsController                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api controller that provides input and output e-documents for filing requests.             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Web.Http;

using Empiria.WebApi;

using Empiria.OnePoint.EFiling.UseCases;

namespace Empiria.OnePoint.EFiling.WebApi {

  /// <summary>Web api controller that provides input and output e-documents for filing requests.</summary>
  [WebApiAuthorizationFilter(WebApiClaimType.ClientAppHasControllerAccess, "Electronic.Filing.Client.Application")]
  public class DocumentsController : WebApiController {

    [HttpGet]
    [Route("v2/electronic-filing/filing-requests/{filingRequestUID:guid}/output-documents")]
    public CollectionModel GetOutputDocuments([FromUri] string filingRequestUID) {

      using (var usecases = new EFilingDocumentsUseCases()) {
        FixedList<EFilingDocument> documents = usecases.GetOutputDocuments(filingRequestUID);

        return new CollectionModel(this.Request, documents);
      }
    }


  }  // class DocumentsController

}  // namespace Empiria.OnePoint.EFiling.WebApi
