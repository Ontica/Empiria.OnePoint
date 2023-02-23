/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Services                              *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Service provider                      *
*  Type     : PermissionsService                           License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides subject permissions read services.                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security;
using Empiria.Security.Providers;

using Empiria.OnePoint.Security.SecurityItems.Adapters;

namespace Empiria.OnePoint.Security.Services {

  /// <summary>Provides subject permissions read services.</summary>
  public class PermissionsService : IPermissionsProvider {

    public FixedList<string> GetFeaturesPermissions(EmpiriaIdentity subject,
                                                    ClientApplication context) {
      Assertion.Require(subject, nameof(subject));
      Assertion.Require(context, nameof(context));

      var permissionsBuilder = new PermissionsBuilder(subject, context);

      return permissionsBuilder.BuildFeatures()
                               .Select(x => x.Key)
                               .ToFixedList();
    }


    public FixedList<IObjectAccessRule> GetObjectAccessRules(EmpiriaIdentity subject,
                                                             ClientApplication context) {
      Assertion.Require(subject, nameof(subject));
      Assertion.Require(context, nameof(context));

      var permissionsBuilder = new PermissionsBuilder(subject, context);

      return permissionsBuilder.BuildObjectAccessRules()
                               .Select(x => MapToObjectAccessRulesDto(x))
                               .ToFixedList();
    }


    public FixedList<string> GetRoles(EmpiriaIdentity subject, ClientApplication context) {
      Assertion.Require(subject, nameof(subject));
      Assertion.Require(context, nameof(context));

      var permissionsBuilder = new PermissionsBuilder(subject, context);

      return permissionsBuilder.BuildRoles()
                               .Select(x => x.Key)
                               .ToFixedList();
    }


    public bool IsSubjectInRole(IIdentifiable subject, ClientApplication context, string role) {
      Assertion.Require(subject, nameof(subject));
      Assertion.Require(context, nameof(context));
      Assertion.Require(role, nameof(role));

      return Role.IsSubjectInRole(subject, context, role);
    }


    private IObjectAccessRule MapToObjectAccessRulesDto(ObjectAccessRule rule) {
      return new ObjectAccessRuleDto {
        TypeName = rule.TypeName,
        ObjectsUIDs = rule.ObjectsUIDs.ToArray()
      };
    }

  }  // class AuthenticationService

}  // namespace Empiria.OnePoint.Security.Services
