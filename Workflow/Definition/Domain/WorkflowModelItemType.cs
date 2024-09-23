/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Power type                              *
*  Type     : WorkflowModelItemType                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Power type that describes a WorkflowModelItem. A workflow model item defines a relationship    *
*             between workflow objects and also serves to give the structure of a workflow.                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Ontology;

namespace Empiria.Workflow.Definition {

  /// <summary>Power type that describes a WorkflowModelItem. A workflow model item defines a relationship
  /// between workflow objects and also serves to give the structure of a workflow.</summary>
  [Powertype(typeof(WorkflowModelItem))]
  public class WorkflowModelItemType : Powertype {

    #region Constructors and parsers

    private WorkflowModelItemType() {
      // Empiria powertype types always have this constructor.
    }

    static public new WorkflowModelItemType Parse(int typeId) {
      return ObjectTypeInfo.Parse<WorkflowModelItemType>(typeId);
    }

    static public new WorkflowModelItemType Parse(string typeName) {
      return WorkflowModelItemType.Parse<WorkflowModelItemType>(typeName);
    }

    #endregion Constructors and parsers

    #region Types

    static public WorkflowModelItemType SequenceFlow => Parse("ObjectTypeInfo.WorkflowModelItem.Connector.SequenceFlow");

    #endregion Types

  } // class WorkflowModelItemType

} // namespace Empiria.Workflow.Definition
