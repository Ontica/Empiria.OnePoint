/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Domain Layer                          *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Information holder                    *
*  Type     : WebServer                                    License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Represents a web server.                                                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.Security {

  /// <summary>Represents a web server.</summary>
  internal sealed class WebServer : SecurityItem, INamedEntity {

    internal WebServer(SecurityItemType powerType) : base(powerType) {
      // Required by Empiria Framework for all partitioned types.
    }

    static public new WebServer Parse(int id) {
      return BaseObject.ParseId<WebServer>(id);
    }


    public string Name {
      get {
        return ExtensionData.Get("serverName", base.BaseKey);
      }
    }

  } // class WebServer

} // namespace Empiria.OnePoint.Security
