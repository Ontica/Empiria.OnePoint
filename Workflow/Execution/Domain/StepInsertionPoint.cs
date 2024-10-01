/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Execution                         Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Value object                            *
*  Type     : StepInsertionPoint                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Value object with information about a workflow step insertion point.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;
using Empiria.Commands;

namespace Empiria.Workflow.Execution {

  /// <summary>Value object with information about a workflow step insertion point.</summary>
  internal class StepInsertionPoint {

    internal StepInsertionPoint(PositioningRule rule, WorkflowStep offsetStep) {
      Assertion.Require(rule, nameof(rule));
      Assertion.Require(offsetStep, nameof(offsetStep));

      this.Rule = rule;
      this.OffsetStep = offsetStep;
    }


    internal PositioningRule Rule {
      get;
    }


    internal WorkflowStep OffsetStep {
      get;
    }

    #region Methods

    internal int CalculateInsertionIndex(FixedList<WorkflowStep> stepsList) {
      Assertion.Require(stepsList, nameof(stepsList));

      switch (Rule) {
        case PositioningRule.AtStart:
          return 0;

        case PositioningRule.AfterOffset:
          return stepsList.IndexOf(OffsetStep) + 1;

        case PositioningRule.BeforeOffset:
          return stepsList.IndexOf(OffsetStep);

        case PositioningRule.AtEnd:
          return stepsList.Count;

        default:
          throw Assertion.EnsureNoReachThisCode($"Unhandled positioning rule {Rule}.");
      }
    }

    #endregion Methods

  }  // class StepInsertionPoint

}  // namespace Empiria.Workflow.Execution
