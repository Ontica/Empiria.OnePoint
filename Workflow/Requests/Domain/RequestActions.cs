/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Service provider                        *
*  Type     : RequestActions                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Determines the actions that can be performed for a workflow request.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.StateEnums;

namespace Empiria.Workflow.Requests {

  /// <summary>Determines the actions that can be performed for a workflow request.</summary>
  public class RequestActions : IWorkflowActions {

    private readonly Request _request;

    public RequestActions(Request request) {
      _request = request;
    }

    public bool CanActivate() {
      if (_request.Status == ActivityStatus.Suspended &&
          RequestHasWorkflowInstances()) {
        return true;
      }
      return false;
    }


    public bool CanCancel() {
      if ((_request.Status == ActivityStatus.Active ||
          _request.Status == ActivityStatus.Suspended) &&
          RequestHasWorkflowInstances()) {
        return true;
      }
      return false;
    }


    public bool CanComplete() {
      if (_request.Status == ActivityStatus.Active &&
          RequestHasWorkflowInstances()) {
        return true;
      }
      return false;
    }


    public bool CanDelete() {
      if (_request.Status == ActivityStatus.Pending &&
          RequestHasWorkflowInstances()) {
        return true;
      }
      return false;
    }


    public bool CanStart() {
      if (_request.Status == ActivityStatus.Pending &&
          !RequestHasWorkflowInstances()) {
        return true;
      }

      return false;
    }


    public bool CanSuspend() {
      if (_request.Status == ActivityStatus.Active &&
          RequestHasWorkflowInstances()) {
        return true;
      }
      return false;
    }


    public bool CanUpdate() {
      if (_request.Status == ActivityStatus.Pending &&
          !RequestHasWorkflowInstances()) {
        return true;
      }
      return false;
    }

    #region Helpers

    private bool RequestHasWorkflowInstances() {
      return _request.GetWorkflowInstances().Count > 0;
    }

    #endregion Helpers

  }  // class RequestActions

}  // namespace Empiria.Workflow.Requests
