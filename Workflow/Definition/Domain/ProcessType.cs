/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : ProcessType                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents a workflow process type.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Represents a workflow request type.</summary>
  public class ProcessType : GeneralObject {

    static internal ProcessType Parse(int id) {
      return BaseObject.ParseId<ProcessType>(id);
    }

    static internal ProcessType Parse(string uid) {
      return BaseObject.ParseKey<ProcessType>(uid);
    }

    public FixedList<DataField> InputData {
      get {
        return base.ExtendedDataField.GetFixedList<DataField>("inputData", false);
      }
    }

  }  // class ProcessType

}  // namespace Empiria.Workflow.Definition
