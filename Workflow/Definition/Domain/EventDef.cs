/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : EventDef                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a workflow event definition.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Represents a workflow event definition.</summary>
  public class EventDef : StepDef {

    static internal new EventDef Parse(int id) {
      return BaseObject.ParseId<EventDef>(id);
    }

    static internal new EventDef Parse(string uid) {
      return BaseObject.ParseKey<EventDef>(uid);
    }

  }  // class EventDef

}  // namespace Empiria.Workflow.Definition
