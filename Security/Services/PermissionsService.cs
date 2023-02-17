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

namespace Empiria.OnePoint.Security.Services {

  /// <summary>Provides subject permissions read services.</summary>
  public class PermissionsService : IPermissionsProvider {

    public FixedList<string> GetFeaturesPermissions(ClientApplication app, EmpiriaIdentity subject) {
      Assertion.Require(app, nameof(app));
      Assertion.Require(subject, nameof(subject));

      var permissionsBuilder = new PermissionsBuilder(app, subject);

      return permissionsBuilder.BuildFeatures()
                               .Select(x => x.Key)
                               .ToFixedList();
    }


    public FixedList<IObjectAccessRule> GetObjectAccessRules(ClientApplication app, EmpiriaIdentity subject) {
      Assertion.Require(app, nameof(app));
      Assertion.Require(subject, nameof(subject));

      var permissionsBuilder = new PermissionsBuilder(app, subject);

      return permissionsBuilder.BuildObjectAccessRules()
                               .Select(x => MapToObjectAccessRulesDto(x))
                               .ToFixedList();
    }


    public FixedList<string> GetRoles(ClientApplication app, EmpiriaIdentity subject) {
      Assertion.Require(app, nameof(app));
      Assertion.Require(subject, nameof(subject));

      var permissionsBuilder = new PermissionsBuilder(app, subject);

      return permissionsBuilder.BuildRoles()
                               .Select(x => x.Key)
                               .ToFixedList();
    }


    public bool IsSubjectInRole(ClientApplication app, IIdentifiable subject, string role) {
      Assertion.Require(app, nameof(app));
      Assertion.Require(subject, nameof(subject));
      Assertion.Require(role, nameof(role));

      return Role.IsSubjectInRole(app, subject, role);
    }


    private IObjectAccessRule MapToObjectAccessRulesDto(ObjectAccessRule rule) {
      return new ObjectAccessRuleDto {
        TypeName = rule.TypeName,
        ObjectsUIDs = rule.ObjectsUIDs.ToArray()
      };
    }


  }  // class AuthenticationService



  internal class ObjectAccessRuleDto : IObjectAccessRule {

    public string TypeName {
      get;
      internal set;
    }


    public string[] ObjectsUIDs {
      get;
      internal set;
    }

  }  // class ObjectAccessRuleDto

}  // namespace Empiria.OnePoint.Security.Services
