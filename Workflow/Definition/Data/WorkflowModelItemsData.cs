/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Data Layer                              *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Data services                           *
*  Type     : WorkflowModelItemsData                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Read and write services for workflow model items.                                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Data;

namespace Empiria.Workflow.Definition.Data {

  /// <summary>Read and write services for workflow model items.</summary>
  static internal class WorkflowModelItemsData {

    static internal FixedList<WorkflowModelItem> GetModelItems(ProcessDef processDef) {
      Assertion.Require(processDef, nameof(processDef));

      var sql = "SELECT * FROM WMS_MODEL_ITEMS " +
               $"WHERE WMS_MDL_ITEM_PROCESS_DEF_ID = {processDef.Id} AND " +
                "WMS_MDL_ITEM_STATUS <> 'X' " +
                "ORDER BY WMS_MDL_ITEM_POSITION";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<WorkflowModelItem>(op);
    }


    static internal FixedList<T> GetSources<T>(ProcessDef processDef,
                                               WorkflowModelItemType modelItemType,
                                               WorkflowObject targetObject) where T : WorkflowObject {
      var sql = "SELECT * FROM WMS_OBJECTS " +
               $"WHERE WMS_OBJECT_ID IN " +
                  $"(SELECT WMS_MDL_ITEM_SOURCE_OBJECT_ID " +
                    $"FROM WMS_MODEL_ITEMS " +
                    $"WHERE WMS_MDL_ITEM_PROCESS_DEF_ID = {processDef.Id} AND " +
                    $"WMS_MODEL_ITEM_TYPE_ID = {modelItemType.Id} AND " +
                    $"WMS_MDL_ITEM_TARGET_OBJECT_ID = {targetObject.Id} AND " +
                    $"WMS_MDL_ITEM_STATUS <> 'X' " +
                    $"ORDER BY WMS_MDL_ITEM_POSITION)";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<T>(op);
    }


    static internal FixedList<T> GetTargets<T>(ProcessDef processDef,
                                               WorkflowModelItemType modelItemType,
                                               WorkflowObject sourceObject) where T : WorkflowObject {
      var sql = "SELECT * FROM WMS_OBJECTS " +
               $"WHERE WMS_OBJECT_ID IN " +
                 $"(SELECT WMS_MDL_ITEM_TARGET_OBJECT_ID " +
                   $"FROM WMS_MODEL_ITEMS " +
                   $"WHERE WMS_MDL_ITEM_PROCESS_DEF_ID = {processDef.Id} AND " +
                   $"WMS_MODEL_ITEM_TYPE_ID = {modelItemType.Id} AND " +
                   $"WMS_MDL_ITEM_SOURCE_OBJECT_ID = {sourceObject.Id} AND " +
                   $"WMS_MDL_ITEM_STATUS <> 'X' " +
                   $"ORDER BY WMS_MDL_ITEM_POSITION)";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<T>(op);
    }

  }  // class WorkflowModelItemsData

}  // namespace Empiria.OnePoint.Workflow.Definition.Data
