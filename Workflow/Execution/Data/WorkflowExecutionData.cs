/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Data Layer                              *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Data Services                           *
*  Type     : WorkflowExecutionData                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides data read and write methods for workflow execution types.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Data;

namespace Empiria.Workflow.Execution.Data {

  /// <summary>Provides data read and write methods for workflow execution types.</summary>
  static internal class WorkflowExecutionData {

    static internal void Write(WorkflowInstance o) {
      var op = DataOperation.Parse("write_WKF_Instance", o.Id, o.UID,
                                  o.ProcessDefinition.Id, o.Request.Id, o.Parent.Id,
                                  o.ExtensionData.ToString(), o.Keywords,
                                  o.StartTime, o.CloseTime, (char) o.Status);

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


  }  // class WorkflowExecutionData

}  // namespace Empiria.Workflow.Execution.Data
