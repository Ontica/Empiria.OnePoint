/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : AssignationRules                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds assignation rules information for a step definition or workflow model item.              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Json;

namespace Empiria.Workflow.Definition {

  public enum AssignationRule {

    None,

    CurrentUser,

    RequestRequester,

    RequestResponsible,

    FixedValue,

  }


  /// <summary>Holds assignation rules information for a step definition or workflow model item.</summary>
  public class AssignationRules {

    static internal AssignationRules Parse(JsonObject json) {
      return new AssignationRules {
        RequestedBy = json.Get(WorkflowConstants.ASSIGN_RULE_REQUESTED_BY, AssignationRule.None),
        RequestedByOrgUnit = json.Get(WorkflowConstants.ASSIGN_RULE_REQUESTED_BY_ORG_UNIT, AssignationRule.None),
        AssignedTo = json.Get(WorkflowConstants.ASSIGN_RULE_ASSIGNED_TO, AssignationRule.None),
        AssignedToOrgUnit = json.Get(WorkflowConstants.ASSIGN_RULE_ASSIGNED_TO_ORG_UNIT, AssignationRule.None),
      };
    }

    public AssignationRule RequestedBy {
      get; private set;
    } = AssignationRule.None;


    public AssignationRule RequestedByOrgUnit {
      get; private set;
    } = AssignationRule.None;


    public AssignationRule AssignedTo {
      get; private set;
    } = AssignationRule.None;


    public AssignationRule AssignedToOrgUnit {
      get; private set;
    } = AssignationRule.None;

  }  //class AssignationRules

}  // namespace Empiria.Workflow.Definition
