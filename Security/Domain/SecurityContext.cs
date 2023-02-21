/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Domain Layer                          *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Information holder                    *
*  Type     : SecurityContext                              License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Holds information about a security context.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security;

using Empiria.OnePoint.Security.Data;

namespace Empiria.OnePoint.Security {

  /// <summary>Holds information about a security context.</summary>
  internal class SecurityContext : SecurityItem, INamedEntity {

    #region Constructors and parsers

    private SecurityContext(SecurityItemType powerType) : base(powerType) {
      // Required by Empiria Framework for all partitioned types.
    }


    static internal new SecurityContext Parse(int id) {
      return BaseObject.ParseId<SecurityContext>(id);
    }


    static internal FixedList<SecurityContext> GetList() {
      var list = SecurityItemsDataReader.GetSecurityItems<SecurityContext>(SecurityItemType.SecurityContext);

      list.Sort((x, y) => x.Name.CompareTo(y.Name));

      return list;
    }

    static internal FixedList<SecurityContext> GetList(IIdentifiable subject) {
      return SecurityItemsDataReader.GetSubjectTargetItems<SecurityContext>(subject, SecurityItem.Empty,
                                                                            SecurityItemType.SubjectContext);
    }

    #endregion Constructors and parsers

    #region Properties

    public string Key {
      get {
        return base.BaseKey;
      }
    }


    public string Name {
      get {
        return ExtensionData.Get("contextName", this.Key);
      }
    }


    ClientApplication[] _clients;

    public ClientApplication[] Clients {
      get {
        if (_clients == null) {
          _clients = ExtensionData.GetList<ClientApplication>("clients", false)
                                  .ToArray();
        }

        return _clients;
      }
    }


    WebServer[] _servers;

    public WebServer[] Servers {
      get {
        if (_servers == null) {
          _servers = ExtensionData.GetList<WebServer>("servers", false)
                                  .ToArray();
        }

        return _servers;
      }
    }

    #endregion Properties

  }  // class SecurityContext

}  // namespace Empiria.OnePoint.Security
