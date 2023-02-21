/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management                 Component : Domain Layer                          *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Service provider                      *
*  Type     : SubjectSecurityItemsEditor                   License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Provides services for subject's security items edition.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.OnePoint.Security.Data;

namespace Empiria.OnePoint.Security.Subjects {

  /// <summary>Provides services for subject's security items edition.</summary>
  internal class SubjectSecurityItemsEditor {

    private readonly IIdentifiable _subject;
    private readonly SecurityContext _context;

    #region Constructors and parsers

    public SubjectSecurityItemsEditor(IIdentifiable subject) {
      Assertion.Require(subject, nameof(subject));

      _subject = subject;
      _context = SecurityContext.Empty;
    }


    public SubjectSecurityItemsEditor(IIdentifiable subject, SecurityContext context) {
      Assertion.Require(subject, nameof(subject));
      Assertion.Require(context, nameof(context));

      _subject = subject;
      _context = context;
    }


    #endregion Constructors and parsers

    internal void AssignContext(SecurityContext context) {
      Assertion.Require(context, nameof(context));

      var data = new SecurityItemDataDto(SecurityItemType.SubjectContext,
                                         SecurityContext.Empty,
                                        _subject,
                                         context);

      SecurityItemsDataWriter.CreateSecurityItem(data);
    }


    internal void AssignFeature(Feature feature) {
      Assertion.Require(feature, nameof(feature));

    }


    internal void AssignRole(Role role) {
      Assertion.Require(role, nameof(role));

      throw new NotImplementedException();
    }


    internal void UnassignContext(SecurityContext context) {
      Assertion.Require(context, nameof(context));

      var data = SecurityItemDataDto.Parse(SecurityItemType.SubjectContext,
                                           SecurityContext.Empty,
                                           _subject,
                                           target: context);

      SecurityItemsDataWriter.RemoveSecurityItem(data);
    }


    internal void UnassignFeature(Feature feature) {
      Assertion.Require(feature, nameof(feature));

      throw new NotImplementedException();
    }


    internal void UnassignRole(Role role) {
      Assertion.Require(role, nameof(role));

      throw new NotImplementedException();
    }

  }  // class SubjectSecurityItemsEditor

}  // namespace Empiria.OnePoint.Security.Subjects
