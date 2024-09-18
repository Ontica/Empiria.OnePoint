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

    static internal new StepDef Parse(int id) {
      return BaseObject.ParseId<StepDef>(id);
    }

    static internal new StepDef Parse(string uid) {
      return BaseObject.ParseKey<StepDef>(uid);
    }

  }  // class StepDef

}  // namespace Empiria.Workflow.Definition
