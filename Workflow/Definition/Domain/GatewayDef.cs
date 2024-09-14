/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : GatewayDef                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract class that defines a workflow gateway.                                                *
*             A gateway serves for activities forking or activities merging.                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Abstract class that defines a workflow gateway.
  /// A gateway serves for activities forking or activities merging.</summary>
  public abstract class GatewayDef : WorkflowObject {

    static internal new GatewayDef Parse(int id) {
      return BaseObject.ParseId<GatewayDef>(id);
    }

    static internal new GatewayDef Parse(string uid) {
      return BaseObject.ParseKey<GatewayDef>(uid);
    }

  }  // class GatewayDef

}  // namespace Empiria.Workflow.Definition
