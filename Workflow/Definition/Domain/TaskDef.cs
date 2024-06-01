/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : TaskDef                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a workflow task definition. A task is an atomic work element in a workflow system.  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Represents a workflow task definition. A task is an atomic work element
  /// in a workflow system.</summary>
  public class TaskDef : ActivityDef {

    static internal new TaskDef Parse(int id) {
      return BaseObject.ParseId<TaskDef>(id);
    }

    static internal new TaskDef Parse(string uid) {
      return BaseObject.ParseKey<TaskDef>(uid);
    }

  }  // class TaskDef

}  // namespace Empiria.Workflow.Definition
