/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Requests Management                        Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.Workflow.dll              Pattern   : Power type                              *
*  Type     : RequestType                                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Power type that describes a Request partitioned type.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using Empiria.DataObjects;
using Empiria.Ontology;

using Empiria.Workflow.Definition;

using Empiria.Workflow.Requests.Adapters;

namespace Empiria.Workflow.Requests {

  /// <summary>Power type that describes a Request partitioned type.</summary>
  [Powertype(typeof(Request))]
  public sealed class RequestType : Powertype {

    #region Constructors and parsers

    private RequestType() {
      // Empiria power types always have this constructor.
    }

    static public new RequestType Parse(int typeId) {
      return ObjectTypeInfo.Parse<RequestType>(typeId);
    }

    static public new RequestType Parse(string typeName) {
      return RequestType.Parse<RequestType>(typeName);
    }

    static internal FixedList<RequestType> GetList(string listName) {
      Assertion.Require(listName, nameof(listName));

      return Empty.ExtensionData.GetFixedList<RequestType>($"lists/{listName}", false);
    }

    static public RequestType Empty => RequestType.Parse("ObjectTypeInfo.Request");

    #endregion Constructors and parsers

    #region Properties

    public ProcessDef DefaultProcessDefinition {
      get {
        return base.ExtensionData.Get<ProcessDef>("defaultProcessDefinitionId");
      }
    }


    public FixedList<DataField> InputData {
      get {
        return base.ExtensionData.GetFixedList<DataField>("inputData", false);
      }
    }

    #endregion Properties

    #region Methods

    public Request CreateRequest(RequestFieldsDto fields) {
      Assertion.Require(fields, nameof(fields));

      var request = base.CreateObject<Request>();

      request.Update(fields);

      return request;
    }

    #endregion Methods

  } // class RequestType

} // namespace Empiria.Workflow.Requests
