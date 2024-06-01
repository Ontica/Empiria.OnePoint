/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : ArtifactDef                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract class that represents a workflow artifact definition. An artifact can be a data       *
*             object, a group of activites or processes, or a swimline of processes.                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Abstract class that represents a workflow artifact definition. An artifact can be a data
  /// object, a group of activites or processes, or a swimline of processes.</summary>
  public abstract class ArtifactDef : WorkflowObject {

    static internal new ArtifactDef Parse(int id) {
      return BaseObject.ParseId<ArtifactDef>(id);
    }

    static internal new ArtifactDef Parse(string uid) {
      return BaseObject.ParseKey<ArtifactDef>(uid);
    }

  }  // class ArtifactDef

}  // namespace Empiria.Workflow.Definition
