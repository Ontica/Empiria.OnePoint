/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : EventDef                                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a workflow event definition.                                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Represents a workflow event definition.</summary>
  public class EventDef : StepDef {

    static internal new EventDef Parse(int id) {
      return BaseObject.ParseId<EventDef>(id);
    }

    static internal new EventDef Parse(string uid) {
      return BaseObject.ParseKey<EventDef>(uid);
    }

    #region Properties

    public bool CatchesMessage {
      get {
        return base.ConfigurationData.Get(WorkflowConstants.EVENT_CATCHES_MESSAGE, false);
      }
    }


    public bool IsEndEvent {
      get {
        return base.ConfigurationData.Get(WorkflowConstants.IS_END_EVENT, false);
      }
    }


    public bool IsIntermediateEvent {
      get {
        return base.ConfigurationData.Get(WorkflowConstants.IS_INTERMEDIATE_EVENT, false);
      }
    }

    public bool IsStartEvent {
      get {
        return base.ConfigurationData.Get(WorkflowConstants.IS_START_EVENT, false);
      }
    }


    public bool ThrowsMessage {
      get {
        return base.ConfigurationData.Get(WorkflowConstants.EVENT_THROWS_MESSAGE, false);
      }
    }

    #endregion Properties

  }  // class EventDef

}  // namespace Empiria.Workflow.Definition
