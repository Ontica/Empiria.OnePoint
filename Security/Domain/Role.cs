﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Domain Layer                          *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Information holder                    *
*  Type     : Role                                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Represents an identity role that holds feature access permissions.                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security;

using Empiria.OnePoint.Security.Data;

namespace Empiria.OnePoint.Security {

  /// <summary>Represents an identity role that holds feature access permissions.</summary>
  internal class Role : SecurityItem, INamedEntity {

    #region Constructors and parsers

    internal Role(SecurityItemType powerType) : base(powerType) {
      // Required by Empiria Framework for all partitioned types.
    }


    static internal new Role Parse(int id) {
      return BaseObject.ParseId<Role>(id);
    }


    static internal FixedList<Role> GetList(ClientApplication app) {
      return SecurityItemsDataReader.GetContextItems<Role>(app,
                                                           SecurityItemType.ClientAppRole);
    }


    static internal FixedList<Role> GetList(ClientApplication app, IIdentifiable subject) {
      return SecurityItemsDataReader.GetSubjectTargetItems<Role>(app, subject,
                                                                 SecurityItemType.SubjectRole);
    }


    static internal bool IsSubjectInRole(ClientApplication app,
                                         IIdentifiable subject,
                                         string roleKey) {
      FixedList<Role> subjectRoles = GetList(app, subject);

      return subjectRoles.Contains(x => x.Key == roleKey);
    }

    #endregion Constructors and parsers

    public string Key {
      get {
        return base.BaseKey;
      }
    }


    public string Name {
      get {
        return ExtensionData.Get("roleName", base.BaseKey);
      }
    }


    Feature[] _grants;

    internal Feature[] Grants {
      get {
        if (_grants == null) {
          _grants = ExtensionData.GetList<Feature>("grants", false)
                                 .ToArray();
        }

        return _grants;
      }
    }

    Feature[] _revokes;

    internal Feature[] Revokes {
      get {
        if (_revokes == null) {
          _revokes = ExtensionData.GetList<Feature>("revokes", false)
                                  .ToArray();
        }

        return _revokes;
      }
    }

  }  // class Role

}  // namespace Empiria.OnePoint.Security
