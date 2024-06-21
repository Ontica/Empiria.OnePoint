/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : ProcessDef                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a workflow process or subprocess definition.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Represents a workflow process or subprocess definition.</summary>
  public class ProcessDef : ActivityDef {

    static internal new ProcessDef Parse(int id) {
      return BaseObject.ParseId<ProcessDef>(id);
    }

    static internal new ProcessDef Parse(string uid) {
      return BaseObject.ParseKey<ProcessDef>(uid);
    }

    static internal ProcessDef Empty => ParseEmpty<ProcessDef>();

  }  // class ProcessDef

}  // namespace Empiria.Workflow.Definition
