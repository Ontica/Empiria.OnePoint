/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowInstance                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : A workflow instance is a runtime representation of a workflow process model definition.        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

using Empiria.StateEnums;

using Empiria.Workflow.Requests;
using Empiria.Workflow.Definition;
using Empiria.Workflow.Execution.Data;

namespace Empiria.Workflow.Execution {

  /// <summary>A workflow instance is a runtime representation of a workflow process model definition.</summary>
  public class WorkflowInstance : BaseObject {

    #region Constructors and parsers

    static internal WorkflowInstance Parse(int id) {
      return BaseObject.ParseId<WorkflowInstance>(id);
    }

    static internal WorkflowInstance Parse(string uid) {
      return BaseObject.ParseKey<WorkflowInstance>(uid);
    }

    static internal WorkflowInstance Empty => ParseEmpty<WorkflowInstance>();

    #endregion Constructors and parsers

    #region Properties

    [DataField("WKF_INSTANCE_MODEL_ID")]
    public ProcessDef Model {
      get; private set;
    }

    [DataField("WKF_INSTANCE_REQUEST_ID")]
    public Request Request {
      get; private set;
    }

    [DataField("WKF_INSTANCE_PARENT_ID")]
    private int _parentId;

    public WorkflowInstance Parent {
      get {
        if (this.IsEmptyInstance) {
          return this;
        }
        return WorkflowInstance.Parse(_parentId);
      }
      private set {
        _parentId = value.Id;
      }
    }


    [DataField("WKF_INSTANCE_EXT_DATA")]
    internal JsonObject ExtensionData {
      get; private set;
    }


    [DataField("WKF_INSTANCE_START_TIME")]
    public DateTime StartTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WKF_INSTANCE_CLOSE_TIME")]
    public DateTime CloseTime {
      get; private set;
    } = ExecutionServer.DateMaxValue;


    [DataField("WKF_INSTANCE_STATUS", Default = ActivityStatus.Pending)]
    public ActivityStatus Status {
      get; private set;
    }


    internal protected virtual string Keywords {
      get {
        return EmpiriaString.BuildKeywords(Request.Keywords);
      }
    }

    #endregion Properties

    #region Methods

    protected override void OnSave() {
      if (base.IsNew) {
        StartTime = EmpiriaDateTime.NowWithCentiseconds;
        Status = ActivityStatus.Active;
      }
      WorkflowExecutionData.Write(this);
    }

    #endregion Methods

  }  // class WorkflowInstance

}  // namespace Empiria.Workflow.Execution
