/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management                 Component : Use cases Layer                       *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Use case interactor                   *
*  Type     : SubjectUseCases                              License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Use cases for subject's management.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Services;

using Empiria.OnePoint.Security.Data;

using Empiria.OnePoint.Security.Subjects.Adapters;

namespace Empiria.OnePoint.Security.Subjects.UseCases {

  /// <summary>Use cases for subject's management.</summary>
  public class SubjectUseCases : UseCase {

    #region Constructors and parsers

    protected SubjectUseCases() {
      // no-op
    }

    static public SubjectUseCases UseCaseInteractor() {
      return CreateInstance<SubjectUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public SubjectDto CreateSubject(SubjectFields fields) {
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      PersonFields personFields = MapToPersonFields(fields);

      var person = new Person(personFields);

      person.Save();

      var editor = new SubjectSecurityItemsEditor(person);

      editor.CreateSubjectCredentials(fields);

      return (SubjectDto) SubjectsData.GetSubject(person);
    }


    public void RemoveSubject(string subjectUID) {
      Assertion.Require(subjectUID, nameof(subjectUID));

      throw new NotImplementedException();
    }


    public FixedList<SubjectDto> SearchSubjects(SubjectsQuery query) {
      Assertion.Require(query, nameof(query));

      string filter = query.MapToFilterString();

      FixedList<SubjectData> users = SubjectsData.SearchSubjects(filter);

      return users.Select(x => (SubjectDto) x)
                  .ToFixedList();
    }


    public SubjectDto UpdateSubject(string subjectUID, SubjectFields fields) {
      Assertion.Require(subjectUID, nameof(subjectUID));
      Assertion.Require(fields, nameof(fields));

      throw new NotImplementedException();
    }


    public FixedList<NamedEntityDto> Workareas() {
      FixedList<Organization> workareas = SubjectsData.Workareas();

      return workareas.MapToNamedEntityList();
    }


    #endregion Use cases

    #region Helpers

    private PersonFields MapToPersonFields(SubjectFields fields) {
      return new PersonFields {
        FullName = fields.FullName,
        ShortName = fields.FullName,
        EMail = fields.EMail,
        Organization = fields.GetWorkarea(),
        JobPosition = fields.JobPosition,
        EmployeeNo = fields.EmployeeNo,
      };
    }

    #endregion Helpers

  }  // class SubjectUseCases

}  // namespace Empiria.OnePoint.Security.Subjects.UseCases
