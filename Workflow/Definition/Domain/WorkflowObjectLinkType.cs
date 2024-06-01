/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Power type                              *
*  Type     : WorkflowObjectLinkType                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Power type that describes a workflow objects link. A workflow object link defines a            *
*             relationship between workflow objects and also serves to give the structure of a workflow.     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Ontology;

namespace Empiria.Workflow.Definition {

  /// <summary>Power type that describes a workflow objects link. A workflow object link defines a
  /// relationship between workflow objects and also serves to give the structure of a workflow.</summary>
  [Powertype(typeof(WorkflowObjectLink))]
  public class WorkflowObjectLinkType : Powertype {

    #region Constructors and parsers

    private WorkflowObjectLinkType() {
      // Empiria powertype types always have this constructor.
    }

    static public new WorkflowObjectLinkType Parse(int typeId) {
      return ObjectTypeInfo.Parse<WorkflowObjectLinkType>(typeId);
    }

    static public new WorkflowObjectLinkType Parse(string typeName) {
      return WorkflowObjectLinkType.Parse<WorkflowObjectLinkType>(typeName);
    }

    #endregion Constructors and parsers

    #region Types

    static public WorkflowObjectLinkType ProcessesGroupType => Parse("ObjectTypeInfo.WorkflowObjectLink.ProcessesGroup");

    #endregion Types

  } // class WorkflowObjectLinkType

} // namespace Empiria.Workflow.Definition
