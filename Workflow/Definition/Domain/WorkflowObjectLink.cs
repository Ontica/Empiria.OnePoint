/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Partitioned type                        *
*  Type     : WorkflowObjectLink                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Partitioned type that describes a workflow objects link. A workflow object link defines a      *
*             relationship between workflow objects and also serves to give the structure of a workflow.     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

using Empiria.Ontology;
using Empiria.StateEnums;

using Empiria.Workflow.Definition.Data;

namespace Empiria.Workflow.Definition {

  /// <summary>Partitioned type that describes a workflow objects link. A workflow object link defines a
  /// relationship between workflow objects and also serves to give the structure of a workflow.</summary>
  [PartitionedType(typeof(WorkflowObjectLinkType))]
  public class WorkflowObjectLink : BaseObject, INamedEntity {

    #region Constructors and parsers

    protected WorkflowObjectLink() {
      // Required by Empiria Framework.
    }

    protected WorkflowObjectLink(WorkflowObjectLinkType powertype) : base(powertype) {
      // Required by Empiria Framework for all partitioned types.
    }

    static public WorkflowObjectLink Parse(int id) {
      return BaseObject.ParseId<WorkflowObjectLink>(id);
    }


    static public WorkflowObjectLink Parse(string uid) {
      return BaseObject.ParseKey<WorkflowObjectLink>(uid);
    }


    static public FixedList<T> GetSources<T>(WorkflowObjectLinkType linkType,
                                             WorkflowObject targetObject) where T : WorkflowObject {
      return WorkflowObjectLinksData.GetSources<T>(linkType, targetObject);
    }

    static public FixedList<T> GetTargets<T>(WorkflowObjectLinkType linkType,
                                             WorkflowObject sourceObject) where T : WorkflowObject {
      return WorkflowObjectLinksData.GetTargets<T>(linkType, sourceObject);
    }


    static public WorkflowObjectLink Empty => BaseObject.ParseEmpty<WorkflowObjectLink>();

    #endregion Constructors and parsers

    #region Properties

    public WorkflowObjectLinkType WorkflowObjectLinkType {
      get {
        return (WorkflowObjectLinkType) base.GetEmpiriaType();
      }
    }


    [DataField("WKF_OBJ_LINK_CODE")]
    public string Code {
      get; protected set;
    }


    [DataField("WKF_OBJ_LINK_NAME")]
    public string Name {
      get; protected set;
    }

    [DataField("WKF_OBJ_LINK_NOTES")]
    public string Description {
      get; protected set;
    }

    [DataField("WKF_OBJ_LINK_TAGS")]
    public string Tags {
      get; protected set;
    }

    [DataField("WKF_OBJ_LINK_EXT_OBJECT_TYPE_ID")]
    public int ExternalObjectTypeId {
      get; protected set;
    }

    [DataField("WKF_OBJ_LINK_WORKFLOW_ID")]
    public ProcessDef Workflow {
      get; protected set;
    }

    [DataField("WKF_OBJ_LINK_SOURCE_OBJECT_ID")]
    public WorkflowObject SourceObject {
      get; protected set;
    }

    [DataField("WKF_OBJ_LINK_TARGET_OBJECT_ID")]
    public WorkflowObject TargetObject {
      get; protected set;
    }

    [DataField("WKF_OBJ_LINK_CONFIG_DATA")]
    protected internal JsonObject ConfigurationData {
      get; private set;
    }

    [DataField("WKF_OBJ_LINK_EXT_DATA")]
    protected internal JsonObject ExtendedData {
      get; private set;
    }


    public virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Name, Code, SourceObject.Keywords, TargetObject.Keywords);
      }
    }

    [DataField("WKF_OBJ_LINK_POSITION")]
    internal int Position {
      get; set;
    }

    [DataField("WKF_OBJ_LINK_START_DATE")]
    internal DateTime StartDate {
      get; set;
    }

    [DataField("WKF_OBJ_LINK_END_DATE")]
    internal DateTime EndDate {
      get; private set;
    }

    [DataField("WKF_OBJ_LINK_POSTED_BY_ID")]
    internal int PostedById {
      get; private set;
    }

    [DataField("WKF_OBJ_LINK_POSTING_TIME")]
    internal DateTime PostingTime {
      get; private set;
    }

    [DataField("WKF_OBJ_LINK_STATUS", Default = EntityStatus.Active)]
    public EntityStatus Status {
      get; protected set;
    }

    #endregion Properties

  } // class WorkflowObjectLink

} // namespace Empiria.Workflow.Definition
