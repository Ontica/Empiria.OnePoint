/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowObject                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Abstract class that represents a workflow object defintion. A workflow object can be a task    *
*             or an activity, gateway element, a connector or a workflow artifact as a data object.          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;
using Empiria.Parties;
using Empiria.StateEnums;

namespace Empiria.Workflow.Definition {

  /// <summary>Abstract class that represents a workflow object definition. A workflow object can be a task
  /// or an activity, a gateway element, a connector or a workflow artifact as a data object.</summary>
  public abstract class WorkflowObject : BaseObject, INamedEntity {

    static internal WorkflowObject Parse(int id) {
      return BaseObject.ParseId<WorkflowObject>(id);
    }

    static internal WorkflowObject Parse(string uid) {
      return BaseObject.ParseKey<WorkflowObject>(uid);
    }

    static public WorkflowObject Empty => BaseObject.ParseEmpty<WorkflowObject>();

    #region Properties

    [DataField("WKF_OBJECT_CODE")]
    public string Code {
      get; protected set;
    }

    [DataField("WKF_OBJECT_NAME")]
    public string Name {
      get; protected set;
    }

    [DataField("WKF_OBJECT_DESCRIPTION")]
    public string Description {
      get; protected set;
    }

    [DataField("WKF_OBJECT_TAGS")]
    public string Tags {
      get; protected set;
    }

    [DataField("WKF_OBJECT_MANAGER_ID")]
    public Party Manager {
      get; protected set;
    }

    [DataField("WKF_OBJECT_EXT_OBJECT_TYPE_ID")]
    protected int ExtendedObjectTypeId {
      get; set;
    }

    [DataField("WKF_OBJECT_CONFIG_DATA")]
    protected JsonObject ConfigurationData {
      get; private set;
    }

    [DataField("WKF_OBJECT_EXT_DATA")]
    protected JsonObject ExtensionData {
      get; private set;
    }

    internal protected virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Code, Name, Tags, Description);
      }
    }

    [DataField("WKF_OBJECT_START_DATE")]
    public DateTime StartDate {
      get; protected set;
    }

    [DataField("WKF_OBJECT_END_DATE")]
    public DateTime EndDate {
      get; protected set;
    }

    [DataField("WKF_OBJECT_POSTING_TIME")]
    public DateTime PostingTime {
      get; private set;
    }

    [DataField("WKF_OBJECT_POSTED_BY_ID")]
    public Party PostedBy {
      get; private set;
    }

    [DataField("WKF_OBJECT_STATUS", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get; private set;
    }

    #endregion Properties

    #region Methods

    public T GetParty<T>(string partyIdField) where T : Party {
      Assertion.Require(partyIdField, nameof(partyIdField));

      return ConfigurationData.Get<T>($"parties/{partyIdField}");
    }


    public bool HasParty(string partyIdField) {
      Assertion.Require(partyIdField, nameof(partyIdField));

      return ConfigurationData.HasValue($"parties/{partyIdField}");
    }

    #endregion Methods

  }  // class WorkflowObject

}  // namespace Empiria.Workflow.Definition
