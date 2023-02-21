/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management                 Component : Use cases Layer                       *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Use case interactor                   *
*  Type     : SubjectSecurityItemsUseCases                 License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Use cases for subject's assigned security items.                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Services;

using Empiria.Security;
using System.Runtime.Remoting.Contexts;

namespace Empiria.OnePoint.Security.Subjects.UseCases {

  /// <summary>Use cases for subject's assigned security items.</summary>
  public class SubjectSecurityItemsUseCases : UseCase {

    #region Constructors and parsers

    protected SubjectSecurityItemsUseCases() {
      // no-op
    }

    static public SubjectSecurityItemsUseCases UseCaseInteractor() {
      return CreateInstance<SubjectSecurityItemsUseCases>();
    }


    #endregion Constructors and parsers

    #region Use cases


    public FixedList<NamedEntityDto> GetSubjectContexts(string subjectUID) {
      Assertion.Require(subjectUID, nameof(subjectUID));

      var subject = Contact.Parse(subjectUID);

      FixedList<SecurityContext> contexts = SecurityContext.GetList(subject);

      return contexts.MapToNamedEntityList();
    }


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

  }  // class SubjectSecurityItemsUseCases

}  // namespace Empiria.OnePoint.Security.Subjects.UseCases
