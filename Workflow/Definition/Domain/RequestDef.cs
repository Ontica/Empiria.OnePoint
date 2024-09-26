/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : RequestDef                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a request definition.                                                               *
*                                                                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Represents a request definition.</summary>
  public class RequestDef : WorkflowObject {

    #region Constructors and parsers

    static internal new RequestDef Parse(int id) {
      return BaseObject.ParseId<RequestDef>(id);
    }

    static internal new RequestDef Parse(string uid) {
      return BaseObject.ParseKey<RequestDef>(uid);
    }

    static internal new RequestDef Empty => BaseObject.ParseEmpty<RequestDef>();

    #endregion Constructors and parsers

  }  // class RequestDef

}  // namespace Empiria.Workflow.Definition
