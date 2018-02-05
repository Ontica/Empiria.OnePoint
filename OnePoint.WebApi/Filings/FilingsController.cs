/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : OnePoint Web API                        *
*  Assembly : Empiria.OnePoint.WebApi.dll                  Pattern : Web Api Controller                      *
*  Type     : FilingsController                            License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains services that interacts with filings.                                                 *
*                                                                                                            *
********************************* Copyright (c) 2009-2018. La Vía Óntica SC, Ontica LLC and contributors.  **/
using System;
using System.Web.Http;

// ToDo: Merge Empiria.WebApi.Models types into Empiria.WebApi namespace.
using Empiria.WebApi;
using Empiria.WebApi.Models;

using Empiria.OnePoint.AppServices;

namespace Empiria.OnePoint.WebApi {

  /// <summary>Contains services that interacts with filings.</summary>
  public class FilingsController : WebApiController {

    #region GET methods

    [HttpGet]
    [Route("v2/filings/{filingUID}")]
    public SingleObjectModel GetFiling([FromUri] string filingUID) {
      try {
        IFiling filing = FilingServices.GetFiling(filingUID);

        return new SingleObjectModel(this.Request, filing.ToResponse());

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }


    [HttpGet]
    [Route("v2/filings")]
    public PagedCollectionModel GetFilingsList([FromUri] string filter,
                                               [FromUri] string sort) {
      try {
        throw new NotImplementedException();

      } catch (Exception e) {
        throw base.CreateHttpException(e);

      }
    }

    #endregion GET methods

  }  // class FilingsController

}  // namespace Empiria.OnePoint.WebApi
