/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Fields Input DTO                        *
*  Type     : RequestFieldsDto                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input fields DTO used to create or update a request.                                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.DataObjects;

namespace Empiria.Workflow.Requests.Adapters {

  /// <summary>Input fields DTO used to create or update a request.</summary>
  public class RequestFieldsDto {

    [Newtonsoft.Json.JsonProperty(PropertyName = "RequestTypeUID")]
    public string RequestDefUID {
      get; set;
    } = string.Empty;


    public string RequesterOrgUnitUID {
      get; set;
    } = string.Empty;


    public string RequestedByUID {
      get; set;
    } = string.Empty;


    public string Description {
      get; set;
    } = string.Empty;


    public FixedList<FieldValue> RequestTypeFields {
      get; set;
    } = new FixedList<FieldValue>();

  }  // class RequestFieldsDto

}  // namespace Empiria.Workflow.Requests.Adapters
