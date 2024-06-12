/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Adapters Layer                          *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Output DTO                              *
*  Type     : RequestTypeDto                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output data transfer object for RequestType instances.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.DataObjects;

namespace Empiria.Workflow.Requests.Adapters {

  /// <summary>Output data transfer object for RequestType instances.</summary>
  public class RequestTypeDto {

    public string UID {
      get; internal set;
    }


    public string Name {
      get; internal set;
    }

    public FixedList<DataField> InputData {
      get; internal set;
    }

  }  // class RequestTypeDto

}  // namespace Empiria.Workflow.Requests.Adapters
