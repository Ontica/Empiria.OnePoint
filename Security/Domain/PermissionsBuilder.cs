﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Domain Layer                          *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Service provider                      *
*  Type     : PermissionsBuilder                           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Builds a string permissions array for a given identity.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Linq;

using Empiria.Security;

namespace Empiria.OnePoint.Security {

  /// <summary>Builds a string permissions array for a given identity.</summary>
  internal class PermissionsBuilder {

    private readonly ClientApplication _clientApp;
    private readonly EmpiriaIdentity _identity;

    internal PermissionsBuilder(ClientApplication clientApp, EmpiriaIdentity identity) {
      Assertion.Require(clientApp, nameof(clientApp));
      Assertion.Require(identity, nameof(identity));

      _clientApp = clientApp;
      _identity = identity;
    }

    #region Methods

    internal FixedList<Feature> BuildFeatures() {
      var features = new List<Feature>(64);

      FillIdentityFeatures(features);

      FixedList<Role> identityRoles = Role.GetList(_clientApp, _identity.User.Contact);

      FillGrantedFeatures(features, identityRoles);

      RemoveRevokedFeatures(features, identityRoles);

      return features.Distinct()
                     .ToFixedList();
    }


    internal FixedList<ObjectAccessRule> BuildObjectAccessRules() {

      FixedList<Feature> features = BuildFeatures();

      var objectRules = new List<ObjectAccessRule>(64);

      foreach (var feature in features) {
        objectRules.AddRange(feature.ObjectsGrants);
      }

      return objectRules.Distinct()
                        .ToFixedList();
    }


    internal FixedList<Role> BuildRoles() {
      return Role.GetList(_clientApp, _identity.User.Contact);
    }

    #endregion Methods

    #region Helpers

    private void FillIdentityFeatures(List<Feature> list) {

      FixedList<Feature> identityFeatures = Feature.GetList(_clientApp,
                                                            _identity.User.Contact);

      list.AddRange(identityFeatures);

      foreach (var feature in identityFeatures) {
        list.AddRange(feature.Requires);

        foreach (var require in feature.Requires) {
          list.AddRange(require.Requires);

          foreach (var item in require.Requires) {
            list.AddRange(item.Requires);
          }
        }
      }
    }


    private void FillGrantedFeatures(List<Feature> list,
                                     FixedList<Role> roles) {
      foreach (var role in roles) {
        list.AddRange(role.Grants);

        foreach (var grant in role.Grants) {
          list.AddRange(grant.Requires);

          foreach (var require in grant.Requires) {
            list.AddRange(require.Requires);

            foreach (var item in require.Requires) {
              list.AddRange(item.Requires);
            }
          }
        }
      }
    }


    private void RemoveRevokedFeatures(List<Feature> list,
                                       FixedList<Role> roles) {
      foreach (var role in roles) {
        foreach (var revoke in role.Revokes) {
          list.Remove(revoke);
        }
      }
    }

    #endregion Helpers

  }  // class PermissionsBuilder

}  // namespace Empiria.OnePoint.Security
