/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Web Api Controller                      *
*  Type     : ESignServicesController                      License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api interface to manage document's e-signature services.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;
using System.Web.Http;

using Empiria.WebApi;
using Empiria.WebApi.Models;

namespace Empiria.OnePoint.ESign.WebApi {

  /// <summary>Services to manage document's e-signature services.</summary>
  public class ESignServicesController : WebApiController {

    #region GET methods

    [HttpGet]
    [Route("v2/e-sign/my-pending-documents")]
    public async Task<CollectionModel> GetDocumentsToSign([FromUri] string filter = "",
                                                          [FromUri] string sort = "") {
      try {
        FixedList<SignableDocument> documents = await ESignServices.GetMyDocumentsToSign(filter, sort);

        return new CollectionModel(this.Request, documents.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet]
    [Route("v2/e-sign/my-refused-documents")]
    public async Task<CollectionModel> GetRefusedDocuments([FromUri] string filter = "",
                                                           [FromUri] string sort = "") {
      try {
        FixedList<SignableDocument> documents = await ESignServices.GetMyRefusedToSignDocuments(filter, sort);

        return new CollectionModel(this.Request, documents.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet]
    [Route("v2/e-sign/my-signed-documents")]
    public async Task<CollectionModel> GetSignedDocuments([FromUri] string filter = "",
                                                          [FromUri] string sort = "") {
      try {
        FixedList<SignableDocument> documents = await ESignServices.GetMySignedDocuments(filter, sort);

        return new CollectionModel(this.Request, documents.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    #endregion GET methods

  }  // class ESignServicesController

}  // namespace Empiria.OnePoint.WebApi
