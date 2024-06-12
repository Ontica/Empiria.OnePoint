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

    public string RequestTypeUID {
      get; set;
    }

    public string RequesterOrgUnitUID {
      get; set;
    }

    public FixedList<FieldValue> RequestTypeFields {
      get; set;
    }


  }  // class RequestFieldsDto

}  // namespace Empiria.Workflow.Requests.Adapters
