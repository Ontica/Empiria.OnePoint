/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Data Layer                              *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Data services                           *
*  Type     : WorkflowObjectLinksData                    License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Read and write services for workflow object definition links.                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

namespace Empiria.Workflow.Definition.Data {

  /// <summary>Read and write services for workflow object definition links.</summary>
  static internal class WorkflowObjectLinksData {

    static internal FixedList<T> GetSources<T>(WorkflowObjectLinkType linkType,
                                               WorkflowObject targetObject) where T : WorkflowObject {
      var sql = "SELECT * FROM WKF_OBJECTS " +
               $"WHERE WKF_OBJECT_ID IN " +
                  $"(SELECT WKF_OBJ_LINK_SOURCE_OBJECT_ID " +
                    $"FROM WKF_OBJ_LINK_TYPE_ID = {linkType.Id} AND " +
                    $"WKF_OBJ_LINK_TARGET_OBJECT_ID = {targetObject.Id} AND " +
                    $"WKF_OBJ_LINK_STATUS <> 'X') " +
               "ORDER BY WKF_OBJECT_NAME";

      return DataReader.GetFixedList<T>(DataOperation.Parse(sql));
    }


    static internal FixedList<T> GetTargets<T>(WorkflowObjectLinkType linkType,
                                               WorkflowObject sourceObject) where T : WorkflowObject {
      var sql = "SELECT * FROM WKF_OBJECTS " +
               $"WHERE WKF_OBJECT_ID IN " +
                  $"(SELECT WKF_OBJ_LINK_TARGET_OBJECT_ID " +
                    $"FROM WKF_OBJECT_LINKS " +
                    $"WHERE WKF_OBJ_LINK_TYPE_ID = {linkType.Id} AND " +
                    $"WKF_OBJ_LINK_SOURCE_OBJECT_ID = {sourceObject.Id} AND " +
                    $"WKF_OBJ_LINK_STATUS <> 'X') " +
               "ORDER BY WKF_OBJECT_NAME";

      return DataReader.GetFixedList<T>(DataOperation.Parse(sql));
    }

  }  // class WorkflowObjectLinksData

}  // namespace Empiria.OnePoint.Workflow.Definition.Data
