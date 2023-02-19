/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : User Management                              Component : Use cases Layer                       *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Use case interactor                   *
*  Type     : UserSecurityItemsUseCases                    License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Use cases for user's assigned security items.                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Services;

using Empiria.Security;

namespace Empiria.OnePoint.Security.UserManagement.UseCases {

  /// <summary>Use cases for user's assigned security items.</summary>
  public class UserSecurityItemsUseCases : UseCase {

    #region Constructors and parsers

    protected UserSecurityItemsUseCases() {
      // no-op
    }

    static public UserSecurityItemsUseCases UseCaseInteractor() {
      return CreateInstance<UserSecurityItemsUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public FixedList<NamedEntityDto> GetSubjectFeatures(string subjectUID, string contextUID) {
      Assertion.Require(subjectUID, nameof(subjectUID));
      Assertion.Require(contextUID, nameof(contextUID));

      var subject = Contact.Parse(subjectUID);
      var context = ClientApplication.ParseActive(contextUID);

      FixedList<Feature> features = Feature.GetList(subject, context);

      return features.MapToNamedEntityList();
    }


    public FixedList<NamedEntityDto> GetSubjectRoles(string subjectUID, string contextUID) {
      Assertion.Require(subjectUID, nameof(subjectUID));
      Assertion.Require(contextUID, nameof(contextUID));

      var subject = Contact.Parse(subjectUID);
      var context = ClientApplication.ParseActive(contextUID);

      FixedList<Role> roles = Role.GetList(subject, context);

      return roles.MapToNamedEntityList();
    }

    #endregion Helpers

  }  // class UserSecurityItemsUseCases

}  // namespace Empiria.OnePoint.Security.UserManagement.UseCases
