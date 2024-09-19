/* Empiria OnePoint  *****************************************************************************************
*                                                                                                            *
*  Module   : One Point Workflow Services                  Component : Test cases                            *
*  Assembly : Empiria.OnePoint.Workflow.Tests.dll          Pattern   : Testing constants                     *
*  Type     : TestingConstants                             License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides testing constants for Empiria OnePoint workflow services.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.Workflow.Definition;
using Empiria.Workflow.Execution;

namespace Empiria.Tests.Workflow {

  /// <summary>Provides testing constants for Empiria OnePoint workflow services.</summary>
  static public class TestingConstants {

    static internal readonly ProcessDef PROCESS_DEF_WITH_STEPS = ProcessDef.Parse(101);

    static internal readonly WorkflowInstance WORKFLOW_INSTANCE = WorkflowInstance.Parse(1);

  }  // class TestingConstants

}  // namespace namespace Empiria.Tests.Workflow
