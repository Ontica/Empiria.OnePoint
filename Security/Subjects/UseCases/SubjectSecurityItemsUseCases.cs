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

    public void AssignContext(string subjectUID, string contextUID) {
      IIdentifiable subject = GetSubject(subjectUID);
      SecurityContext context = GetContext(contextUID);

      var subjectSecurity = new SubjectSecurityItemsEditor(subject);

      subjectSecurity.AssignContext(context);
    }


    public void AssignFeature(string subjectUID, string contextUID, string featureUID) {
      Assertion.Require(featureUID, nameof(featureUID));

      var feature = Feature.Parse(featureUID);

      SubjectSecurityItemsEditor subject = GetSubjectSecurityItemsEditor(subjectUID, contextUID);

      subject.AssignFeature(feature);
    }


    public void AssignRole(string subjectUID, string contextUID, string roleUID) {
      Assertion.Require(roleUID, nameof(roleUID));

      var role = Role.Parse(roleUID);

      SubjectSecurityItemsEditor subjectSecurity = GetSubjectSecurityItemsEditor(subjectUID, contextUID);

      subjectSecurity.AssignRole(role);
    }


    public FixedList<NamedEntityDto> GetSubjectContexts(string subjectUID) {
      IIdentifiable subject = GetSubject(subjectUID);

      FixedList<SecurityContext> contexts = SecurityContext.GetList(subject);

      return contexts.MapToNamedEntityList();
    }


    public FixedList<NamedEntityDto> GetSubjectFeatures(string subjectUID, string contextUID) {
      IIdentifiable subject = GetSubject(subjectUID);
      SecurityContext context = GetContext(contextUID);

      //FixedList<Feature> features = Feature.GetList(subject, context);

      //return features.MapToNamedEntityList();

      throw new NotImplementedException();
    }



    public FixedList<NamedEntityDto> GetSubjectRoles(string subjectUID, string contextUID) {
      IIdentifiable subject = GetSubject(subjectUID);
      SecurityContext context = GetContext(contextUID);

      //FixedList<Role> roles = Role.GetList(subject, context);

      //return roles.MapToNamedEntityList();

      throw new NotImplementedException();
    }


    public void UnassignContext(string subjectUID, string contextUID) {
      IIdentifiable subject = GetSubject(subjectUID);
      SecurityContext context = GetContext(contextUID);

      var subjectSecurity = new SubjectSecurityItemsEditor(subject);

      subjectSecurity.UnassignContext(context);
    }


    public void UnassignFeature(string subjectUID, string contextUID, string featureUID) {
      Assertion.Require(featureUID, nameof(featureUID));

      var feature = Feature.Parse(featureUID);

      SubjectSecurityItemsEditor subjectSecurity = GetSubjectSecurityItemsEditor(subjectUID, contextUID);

      subjectSecurity.UnassignFeature(feature);
    }


    public void UnassignRole(string subjectUID, string contextUID, string roleUID) {
      Assertion.Require(roleUID, nameof(roleUID));

      var role = Role.Parse(roleUID);

      SubjectSecurityItemsEditor subjectSecurity = GetSubjectSecurityItemsEditor(subjectUID, contextUID);

      subjectSecurity.UnassignRole(role);
    }

    #endregion Use cases

    #region Helpers

    private SecurityContext GetContext(string contextUID) {
      Assertion.Require(contextUID, nameof(contextUID));

      return SecurityContext.Parse(contextUID);
    }


    private IIdentifiable GetSubject(string subjectUID) {
      Assertion.Require(subjectUID, nameof(subjectUID));

      return Contact.Parse(subjectUID);
    }


    private SubjectSecurityItemsEditor GetSubjectSecurityItemsEditor(string subjectUID,
                                                                     string contextUID) {
      IIdentifiable subject = GetSubject(subjectUID);
      SecurityContext context = GetContext(contextUID);

      return new SubjectSecurityItemsEditor(subject, context);
    }

    #endregion Helpers

  }  // class SubjectSecurityItemsUseCases

}  // namespace Empiria.OnePoint.Security.Subjects.UseCases
