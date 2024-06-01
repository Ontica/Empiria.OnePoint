/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : MergeGatewayDef                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : A merge gateway definition that describes a workflow step that merges two or more activities.  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>A merge gateway definition that describes a workflow step that
  /// merges two or more activities.</summary>
  public class MergeGatewayDef : GatewayDef {

    static internal new MergeGatewayDef Parse(int id) {
      return BaseObject.ParseId<MergeGatewayDef>(id);
    }

    static internal new MergeGatewayDef Parse(string uid) {
      return BaseObject.ParseKey<MergeGatewayDef>(uid);
    }

  }  // class MergeGatewayDef

}  // namespace Empiria.Workflow.Definition
