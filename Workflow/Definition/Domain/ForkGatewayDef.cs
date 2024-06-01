/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : ForkGatewayDef                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : A fork gateway definition that describes a workflow step                                       *
*             that generates two or more activities.                                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>A fork gateway definition that describes a workflow step
  /// that generates two or more activities.</summary>
  public class ForkGatewayDef : GatewayDef {

    static internal new ForkGatewayDef Parse(int id) {
      return BaseObject.ParseId<ForkGatewayDef>(id);
    }

    static internal new ForkGatewayDef Parse(string uid) {
      return BaseObject.ParseKey<ForkGatewayDef>(uid);
    }

  }  // class ForkGatewayDef

}  // namespace Empiria.Workflow.Definition
