﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security                                     Component : Authorization services                *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Service provider                      *
*  Type     : AuthorizationService                         License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides subject's authorization services.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security;
using Empiria.Security.Providers;

using Empiria.OnePoint.Security.SecurityItems.Adapters;

namespace Empiria.OnePoint.Security.Services {

  /// <summary>Provides subject's authorization services.</summary>
  public class AuthorizationService : IAuthorizationProvider {

    internal FixedList<string> GetFeaturesPermissions(EmpiriaIdentity subject,
                                                      IClientApplication clientApp) {
      Assertion.Require(subject, nameof(subject));
      Assertion.Require(clientApp, nameof(clientApp));

      var context = DetermineSecurityContext(clientApp);

      var permissionsBuilder = new PermissionsBuilder(subject, context);

      return permissionsBuilder.BuildFeatures()
                               .Select(x => x.Key)
                               .ToFixedList();
    }


    internal FixedList<IObjectAccessRule> GetObjectAccessRules(EmpiriaIdentity subject,
                                                               IClientApplication clientApp) {
      Assertion.Require(subject, nameof(subject));
      Assertion.Require(clientApp, nameof(clientApp));

      var context = DetermineSecurityContext(clientApp);

      var permissionsBuilder = new PermissionsBuilder(subject, context);

      return permissionsBuilder.BuildObjectAccessRules()
                               .Select(x => MapToObjectAccessRulesDto(x))
                               .ToFixedList();
    }


    internal FixedList<string> GetRoles(EmpiriaIdentity subject,
                                        IClientApplication clientApp) {
      Assertion.Require(subject, nameof(subject));
      Assertion.Require(clientApp, nameof(clientApp));

      var context = DetermineSecurityContext(clientApp);

      var permissionsBuilder = new PermissionsBuilder(subject, context);

      return permissionsBuilder.BuildRoles()
                               .Select(x => x.Key)
                               .ToFixedList();
    }


    public bool IsSubjectInRole(IIdentifiable subject, IClientApplication clientApp, string role) {
      Assertion.Require(subject, nameof(subject));
      Assertion.Require(clientApp, nameof(clientApp));
      Assertion.Require(role, nameof(role));

      var context = DetermineSecurityContext(clientApp);

      return Role.IsSubjectInRole(subject, context, role);
    }

    #region Helpers


    private SecurityContext DetermineSecurityContext(IClientApplication clientApp) {
      return SecurityContext.ParseWith(clientApp);
    }


    private IObjectAccessRule MapToObjectAccessRulesDto(ObjectAccessRule rule) {
      return new ObjectAccessRuleDto {
        TypeName = rule.TypeName,
        ObjectsUIDs = rule.ObjectsUIDs.ToArray()
      };
    }


    #endregion Helpers

  }  // class AuthorizationService

}  // namespace Empiria.OnePoint.Security.Services
