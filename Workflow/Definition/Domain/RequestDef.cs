/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : RequestDef                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a request definition.                                                               *
*                                                                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.DataObjects;
using Empiria.Parties;

using Empiria.Workflow.Requests;

namespace Empiria.Workflow.Definition {

  /// <summary>Represents a request definition.</summary>
  public class RequestDef : WorkflowObject {

    #region Constructors and parsers

    static internal new RequestDef Parse(int id) {
      return ParseId<RequestDef>(id);
    }

    static internal new RequestDef Parse(string uid) {
      return ParseKey<RequestDef>(uid);
    }


    static internal FixedList<RequestDef> GetList(string listName) {
      Assertion.Require(listName, nameof(listName));

      var list = WorkflowObjectsGroup.ParseWithCode(listName);

      return list.GetItems<RequestDef>(WorkflowModelItemType.RequestDefListItem);
    }


    static internal new RequestDef Empty => ParseEmpty<RequestDef>();

    #endregion Constructors and parsers

    #region Properties

    public ProcessDef DefaultProcessDefinition {
      get {
        return base.ConfigurationData.Get<ProcessDef>(WorkflowConstants.DEFAULT_PROCESS_DEFINITION_ID);
      }
    }


    public FixedList<DataField> InputData {
      get {
        return base.ConfigurationData.GetFixedList<DataField>(WorkflowConstants.INPUT_DATA_LIST, false);
      }
    }


    public OrganizationalUnit ResponsibleOrgUnit {
      get {
        return base.ConfigurationData.Get(WorkflowConstants.RESPONSIBLE_ORG_UNIT_ID, OrganizationalUnit.Empty);
      }
    }


    public RequestType RequestType {
      get {
        return base.ConfigurationData.Get(WorkflowConstants.REQUEST_TYPE_ID, RequestType.Empty);
      }
    }

    #endregion Properties

  }  // class RequestDef

}  // namespace Empiria.Workflow.Definition
