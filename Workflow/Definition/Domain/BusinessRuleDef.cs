/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : BusinessRuleDef                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a workflow task definition used to model the evaluation                             *
*             of a business rule using a decision model engine.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Represents a workflow task definition used to model the evaluation
  ///  of a business rule using a decision model engine.</summary>
  public class BusinessRuleDef : ActivityDef {

    static internal new BusinessRuleDef Parse(int id) {
      return BaseObject.ParseId<BusinessRuleDef>(id);
    }

    static internal new BusinessRuleDef Parse(string uid) {
      return BaseObject.ParseKey<BusinessRuleDef>(uid);
    }

  }  // class BusinessRuleDef

}  // namespace Empiria.Workflow.Definition
