/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Empiria Object Type                     *
*  Type     : Procedure                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Defines a procedure and its rules.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Defines a procedure and its rules.</summary>
  public class Procedure: BaseObject, IProcedure {

    #region Constructors and parsers

    private Procedure() {
      // Required by Empiria Framework
    }


    static internal Procedure Parse(int id) {
      return BaseObject.ParseId<Procedure>(id);
    }


    static public Procedure Parse(string uid) {
      return BaseObject.ParseKey<Procedure>(uid);
    }


    static public Procedure Empty {
      get {
        return BaseObject.ParseEmpty<Procedure>();
      }
    }


    static public FixedList<Procedure> GetList() {
      return BaseObject.GetList<Procedure>().ToFixedList();
    }

    #endregion Constructors and parsers


    #region Properties

    [DataField("ObjectKey")]
    public string NamedKey {
      get;
      private set;
    }


    [DataField("ObjectName")]
    public string DisplayName {
      get;
      private set;
    }


    [DataField("ObjectExtData")]
    public JsonObject ExtensionData {
      get;
      private set;
    } = new JsonObject();


    public bool RequiresSign {
      get {
        return this.ExtensionData.Get("requiresSign", true);
      }
    }


    #endregion Properties


    #region Methods


    internal IFilingTransactionProvider GetFilingTransactionProvider() {
      return ExternalProviders.GetFilingTransactionProvider(this);
    }


    #endregion Methods

  }  // class Procedure

}  // namespace Empiria.OnePoint.EFiling
