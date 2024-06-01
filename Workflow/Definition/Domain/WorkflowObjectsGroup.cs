/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : WorkflowObjectsGroup                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Worflow object that serves to group other workflow objects.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.Workflow.Definition {

  /// <summary>Worflow object that serves to group other workflow objects.</summary>
  public class WorkflowObjectsGroup : ArtifactDef {

    static public new WorkflowObjectsGroup Parse(string uid) {
      return BaseObject.ParseKey<WorkflowObjectsGroup>(uid);
    }

    internal static WorkflowObjectsGroup ParseWithCode(string code) {
      var group = BaseObject.TryParse<WorkflowObjectsGroup>($"WKF_OBJECT_CODE = '{code}'");

      Assertion.Require(group, $"Unrecognized process group code '{code}'.");

      return group;
    }

    #region Properties

    public FixedList<T> GetItems<T>(WorkflowObjectLinkType linkType) where T : WorkflowObject {
      return WorkflowObjectLink.GetTargets<T>(linkType, this);
    }

    #endregion Properties

  }  // class WorkflowObjectsGroup

}  // namespace Empiria.Workflow.Definition
