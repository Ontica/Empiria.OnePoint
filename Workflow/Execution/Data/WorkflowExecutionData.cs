/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Data Layer                              *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Data Services                           *
*  Type     : WorkflowExecutionData                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write methods for workflow execution types.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Collections.Generic;

using Empiria.Data;

using Empiria.Workflow.Requests;

namespace Empiria.Workflow.Execution.Data {

  /// <summary>Provides data read and write methods for workflow execution types.</summary>
  static internal class WorkflowExecutionData {

    #region Read methods

    static internal List<WorkflowStep> GetSteps(WorkflowInstance workflowInstance) {
      if (workflowInstance.IsEmptyInstance || workflowInstance.IsNew) {
        return new List<WorkflowStep>();
      }

      var sql = "SELECT * FROM WKF_Steps " +
               $"WHERE WKF_INSTANCE_ID = {workflowInstance.Id} AND " +
                "WKF_STEP_STATUS <> 'X' " +
                "ORDER BY WKF_STEP_NO";

      var op = DataOperation.Parse(sql);

      return DataReader.GetList<WorkflowStep>(op);
    }


    static internal FixedList<WorkflowInstance> GetWorkflowInstances(Request request) {
      if (request.IsEmptyInstance || request.IsNew) {
        return new FixedList<WorkflowInstance>();
      }

      var sql = "SELECT * FROM WKF_Instances " +
                $"WHERE WKF_INSTANCE_REQUEST_ID = {request.Id} AND " +
                "WKF_INSTANCE_STATUS <> 'X' " +
                "ORDER BY WKF_INSTANCE_ID";

      var op = DataOperation.Parse(sql);

      return DataReader.GetFixedList<WorkflowInstance>(op);
    }

    #endregion Read methods

    #region Write methods

    static internal void Write(WorkflowInstance o) {
      var op = DataOperation.Parse("write_WKF_Instance", o.Id, o.UID,
                                  o.ProcessDefinition.Id, o.Request.Id, o.Parent.Id,
                                  o.ExtensionData.ToString(), o.Keywords,
                                  o.StartTime, o.EndTime, (char) o.Status);

      DataWriter.Execute(op);
    }


    static internal void Write(WorkflowStep o, string extensionData) {
      var op = DataOperation.Parse("write_WKF_Step", o.Id, o.UID,
                      o.WorkflowInstance.Id, o.WorkflowModelItem.Id, o.No, o.Description,
                      o.Tags, o.ExternalObjectId, o.RequestedBy.Id, o.RequestedByOrgUnit.Id,
                      o.AssignedTo.Id, o.AssignedToOrgUnit.Id, o.Deadline, o.CheckInTime,
                      o.EndTime, o.CheckOutTime, o.PreviousStep.Id, o.NextStep.Id,
                      extensionData, o.Keywords, (char) o.Status);

      DataWriter.Execute(op);
    }

    #endregion Write methods

  }  // class WorkflowExecutionData

}  // namespace Empiria.Workflow.Execution.Data
