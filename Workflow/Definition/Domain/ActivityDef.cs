/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : ActivityDef                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract class that represents a workflow activity definition.                                 *
*             An activity can be a single task a process or subprocess, or an activity caller.               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.DataObjects;

namespace Empiria.Workflow.Definition {

  /// <summary>Abstract class that represents a workflow activity definition.
  /// An activity can be a single task, a process or subprocess, or an activity caller.</summary>
  public abstract class ActivityDef : StepDef {

    static internal new ActivityDef Parse(int id) {
      return BaseObject.ParseId<ActivityDef>(id);
    }

    static internal new ActivityDef Parse(string uid) {
      return BaseObject.ParseKey<ActivityDef>(uid);
    }

    #region Properties

    public FixedList<DataField> InputData {
      get {
        return base.ConfigurationData.GetFixedList<DataField>("inputData", false);
      }
    }

    public FixedList<DataField> OutputData {
      get {
        return base.ConfigurationData.GetFixedList<DataField>("ouputData", false);
      }
    }

    #endregion Properties

  }  // class ActivityDef

}  // namespace Empiria.Workflow.Definition
