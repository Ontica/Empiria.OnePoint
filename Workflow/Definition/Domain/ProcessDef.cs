/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : ProcessDef                                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a workflow process or subprocess definition.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Workflow.Definition.Data;

namespace Empiria.Workflow.Definition {

  /// <summary>Represents a workflow process or subprocess definition.</summary>
  public class ProcessDef : ActivityDef {

    #region Fields

    private Lazy<FixedList<WorkflowModelItem>> _model = new Lazy<FixedList<WorkflowModelItem>>();

    #endregion Fields

    #region Constructors and parsers

    static internal new ProcessDef Parse(int id) {
      return BaseObject.ParseId<ProcessDef>(id);
    }

    static internal new ProcessDef Parse(string uid) {
      return BaseObject.ParseKey<ProcessDef>(uid);
    }

    static internal new ProcessDef Empty => ParseEmpty<ProcessDef>();

    protected override void OnLoad() {
      if (!this.IsEmptyInstance) {
        _model = new Lazy<FixedList<WorkflowModelItem>>(() => WorkflowModelItemsData.GetModelItems(this));
      }
    }

    #endregion Constructors and parsers

    #region Methods

    public FixedList<WorkflowModelItem> Model {
      get {
        return _model.Value;
      }
    }

    #endregion Methods

    #region Methods

    internal FixedList<WorkflowModelItem> GetInitialSteps() {
      return Model.FindAll(x => x.WorkflowModelItemType.Equals(WorkflowModelItemType.SequenceFlow));
    }

    #endregion Methods

  }  // class ProcessDef

}  // namespace Empiria.Workflow.Definition
