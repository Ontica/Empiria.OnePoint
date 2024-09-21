/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Service Provider                        *
*  Type     : WorkflowStepAssigner                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Service that determines the assignation rules for a given workflow step.                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Parties;

using Empiria.Workflow.Definition;

namespace Empiria.Workflow.Execution {

  /// <summary>Service that determines the assignation rules for a given workflow step.</summary>
  internal class WorkflowStepAssigner {

    private readonly WorkflowStep _step;

    internal WorkflowStepAssigner(WorkflowStep step) {
      _step = step;
    }

    #region Properties

    public Party RequestedBy {
      get {
        return GetParty(AssignationRules.RequestedBy, WorkflowConstants.REQUESTED_BY_ID);
      }
    }


    public OrganizationalUnit RequestedByOrgUnit {
      get {
        return GetOrganizationalUnit(AssignationRules.RequestedByOrgUnit,
                                     WorkflowConstants.REQUESTED_BY_ORG_UNIT_ID);
      }
    }


    public Party AssignedTo {
      get {
        return GetParty(AssignationRules.AssignedTo, WorkflowConstants.ASSIGNED_TO_ID);
      }
    }


    public OrganizationalUnit AssignedToOrgUnit {
      get {
        return GetOrganizationalUnit(AssignationRules.AssignedToOrgUnit,
                                     WorkflowConstants.ASSIGNED_TO_ORG_UNIT_ID);
      }
    }

    #endregion Properties

    #region Helpers

    private AssignationRules AssignationRules {
      get {
        return _step.WorkflowModelItem.AssignationRules;
      }
    }


    private OrganizationalUnit GetOrganizationalUnit(AssignationRule assignationRule,
                                                     string assignedOrgUnitField = "") {
      switch (assignationRule) {

        case AssignationRule.CurrentUser:
          return OrganizationalUnit.Parse(2);

        case AssignationRule.RequestRequester:
          return _step.WorkflowInstance.Request.RequesterOrgUnit;

        case AssignationRule.RequestResponsible:
          return _step.WorkflowInstance.Request.ResponsibleOrgUnit;

        case AssignationRule.FixedValue:
          return _step.WorkflowModelItem.ConfigurationData.Get(assignedOrgUnitField, OrganizationalUnit.Empty);

        default:
          return OrganizationalUnit.Empty;
      }
    }


    private Party GetParty(AssignationRule assignationRule,
                           string assignedPartyField = "") {

      switch (assignationRule) {

        case AssignationRule.CurrentUser:
          return Party.ParseWithContact(ExecutionServer.CurrentContact);

        case AssignationRule.RequestRequester:
          return _step.WorkflowInstance.Request.Requester;

        case AssignationRule.RequestResponsible:
          return _step.WorkflowInstance.Request.ResponsibleOrgUnit;

        case AssignationRule.FixedValue:
          return _step.WorkflowModelItem.ConfigurationData.Get(assignedPartyField, Party.Empty);

        default:
          return Party.Empty;
      }
    }

    #endregion Helpers

  }  // class WorkflowStepAssigner

}  // namespace Empiria.Workflow.Execution
