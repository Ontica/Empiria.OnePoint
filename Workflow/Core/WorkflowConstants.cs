/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Core                              Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowConstants                          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds static constants for workflow configuration.                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow {

  /// <summary>Holds static constants for workflow configuration.</summary>
  static internal class WorkflowConstants {

    #region Assignation Constants

    static readonly internal string ASSIGNATION_RULES = "assignationRules";

    static readonly internal string ASSIGN_RULE_REQUESTED_BY = "requestedBy";

    static readonly internal string ASSIGN_RULE_REQUESTED_BY_ORG_UNIT = "requestedByOrgUnit";

    static readonly internal string ASSIGN_RULE_ASSIGNED_TO = "assignedTo";

    static readonly internal string ASSIGN_RULE_ASSIGNED_TO_ORG_UNIT = "assignedToOrgUnit";

    static readonly internal string REQUESTED_BY_ID = "requestedById";

    static readonly internal string REQUESTED_BY_ORG_UNIT_ID = "requestedByOrgUnitId";

    static readonly internal string RESPONSIBLE_ORG_UNIT_ID = "responsibleOrgUnitId";

    static readonly internal string ASSIGNED_TO_ID = "assignedToId";

    static readonly internal string ASSIGNED_TO_ORG_UNIT_ID = "assignedToOrgUnitId";

    #endregion Assignation Constants

    #region Control Constants

    static readonly internal string AUTOACTIVATE = "autoactivate";

    static readonly internal string IS_OPTIONAL = "isOptional";

    #endregion Control Constants

    #region Event Constants

    static readonly internal string EVENT_CATCHES_MESSAGE = "catchesMessage";

    static readonly internal string IS_END_EVENT = "isEndEvent";

    static readonly internal string IS_INTERMEDIATE_EVENT = "isIntermediateEvent";

    static readonly internal string IS_START_EVENT = "isStartEvent";

    static readonly internal string EVENT_THROWS_MESSAGE = "throwsMessage";

    #endregion Event Constants

    #region Data Constants

    static readonly internal string INPUT_DATA_LIST = "inputData";

    static readonly internal string OUTPUT_DATA_LIST = "outputData";

    #endregion Data Constants

    #region Process Constants

    static readonly internal string REQUEST_TYPE_ID = "requestTypeId";

    static readonly internal string DEFAULT_PROCESS_DEFINITION_ID = "defaultProcessDefinitionId";

    #endregion Process Constants

  }  // class WorkflowConstants

}  // namespace Empiria.Workflow
