/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Information Holder                      *
*  Type     : EFilingDocument                            License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds data about an e-filing request input or output document.                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.OnePoint.EFiling {


  /// <summary>Holds data about an e-filing request input or output document.</summary>
  public class EFilingDocument {

    #region Constuctors and parsers

    static public EFilingDocument Parse(JsonObject json) {
      var doc = new EFilingDocument();

      doc.UID = json.Get("uid", doc.UID);
      doc.Type = json.Get("type", doc.Type);
      doc.TypeName = json.Get("typeName", doc.TypeName);
      doc.Name = json.Get("name", doc.Name);
      doc.ContentType = json.Get("contentType", doc.ContentType);
      doc.Uri = json.Get("uri", doc.Uri);

      return doc;
    }


    public JsonObject ToJson() {
      var json = new JsonObject();

      json.Add("uid", this.UID);
      json.Add("type", this.Type);
      json.Add("typeName", this.TypeName);
      json.Add("name", this.Name);
      json.Add("contentType", this.ContentType);
      json.Add("uri", this.Uri);

      return json;
    }


    #endregion Constuctors and parsers

    #region Properties

    public string UID {
      get; set;
    } = String.Empty;


    public string Type {
      get; set;
    } = String.Empty;


    public string TypeName {
      get; set;
    } = String.Empty;


    public string Name {
      get; set;
    } = String.Empty;


    public string ContentType {
      get; set;
    } = String.Empty;


    public string Uri {
      get; set;
    } = String.Empty;


    #endregion Properties

  }  // class EFilingDocument

}  // namespace Empiria.OnePoint.EFiling
