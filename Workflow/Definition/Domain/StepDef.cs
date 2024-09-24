/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : StepDef                                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract class that represents a definition of a workflow activity, event or gateway.          *
*             In BPM a step is known as a flow object.                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Abstract class that represents a definition of a workflow activity, event or gateway.
  /// In BPM a step is known as a flow object.</summary>
  public abstract class StepDef : WorkflowObject {

    #region Constructors and parsers

    static internal new StepDef Parse(int id) {
      return BaseObject.ParseId<StepDef>(id);
    }

    static internal new StepDef Parse(string uid) {
      return BaseObject.ParseKey<StepDef>(uid);
    }

    static internal new StepDef Empty => BaseObject.ParseEmpty<StepDef>();

    #endregion Constructors and parsers

    #region Properties

    public AssignationRules AssignationRules {
      get {
        return ConfigurationData.Get(WorkflowConstants.ASSIGNATION_RULES, new AssignationRules());
      }
    }


    public bool Autoactivate {
      get {
        return ConfigurationData.Get<bool>(WorkflowConstants.AUTOACTIVATE, false);
      }
    }

    #endregion Properties

  }  // class StepDef

}  // namespace Empiria.Workflow.Definition
