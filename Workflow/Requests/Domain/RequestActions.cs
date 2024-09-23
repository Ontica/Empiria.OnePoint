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
      if (_request.HasWorkflowInstance &&
          _request.Status == ActivityStatus.Suspended) {
        return true;
      }
      return false;
    }


    public bool CanCancel() {
      if (_request.HasWorkflowInstance &&
         (_request.Status == ActivityStatus.Active ||
          _request.Status == ActivityStatus.Suspended)) {
        return true;
      }
      return false;
    }


    public bool CanComplete() {
      if (_request.HasWorkflowInstance &&
          _request.Status == ActivityStatus.Active) {
        return true;
      }
      return false;
    }


    public bool CanDelete() {
      if (!_request.HasWorkflowInstance &&
          _request.Status == ActivityStatus.Pending) {
        return true;
      }
      return false;
    }


    public bool CanStart() {
      if (!_request.HasWorkflowInstance &&
           _request.Status == ActivityStatus.Pending) {
        return true;
      }

      return false;
    }


    public bool CanSuspend() {
      if (_request.HasWorkflowInstance &&
          _request.Status == ActivityStatus.Active) {
        return true;
      }
      return false;
    }


    public bool CanUpdate() {
      if (!_request.HasWorkflowInstance &&
          _request.Status == ActivityStatus.Pending) {
        return true;
      }
      return false;
    }

  }  // class RequestActions

}  // namespace Empiria.Workflow.Requests
