/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Workflow Definition                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Information Holder                      *
*  Type     : ProcessGroup                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds a list of workflow processes types.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Workflow.Definition {

  /// <summary>Holds a list of workflow processes types</summary>
  public class ProcessGroup : GeneralObject {

    static public ProcessGroup Parse(string uid) {
      return BaseObject.ParseKey<ProcessGroup>(uid);
    }


    public FixedList<ProcessType> Processes {
      get {
        return base.ExtendedDataField.GetFixedList<ProcessType>("processes")
                                     .Sort((x, y) => x.Name.CompareTo(y.Name));
      }
    }

  }  // class ProcessGroup

}  // namespace Empiria.Workflow.Definition
