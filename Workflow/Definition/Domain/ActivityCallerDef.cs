/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : ActivityCallerDef                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a workflow call activity definition. An activity caller invokes an activity         *
*             defined in other processes, and serves for reuse subprocesses.                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Represents a workflow call activity definition. An activity caller invokes an activity
  /// defined in other processes, and serves for reuse subprocesses.</summary>
  public class ActivityCallerDef : ActivityDef {

    static internal new ActivityCallerDef Parse(int id) {
      return BaseObject.ParseId<ActivityCallerDef>(id);
    }

    static internal new ActivityCallerDef Parse(string uid) {
      return BaseObject.ParseKey<ActivityCallerDef>(uid);
    }

  }  // class ActivityCallerDef

}  // namespace Empiria.Workflow.Definition
