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
using Empiria.StateEnums;

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

    public SubjectDto ActivateSubject(string subjectUID) {
      Assertion.Require(subjectUID, nameof(subjectUID));

      var contact = Contact.Parse(subjectUID);

      contact.ChangeStatus(EntityStatus.Active);

      contact.Save();

      var editor = new SubjectSecurityItemsEditor(contact);

      editor.ActivateSubjectCredentials();

      var subject = SubjectsDataService.GetSubject(contact);

      EmpiriaLog.UserManagementLog(subject.Contact, "Activación de la cuenta de usuario");

      return SubjectMapper.Map(subject);
    }


    public SubjectDto CreateSubject(SubjectFields fields) {
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      var existingUser = SubjectsDataService.TryGetSubjectWithUserID(fields.UserID);

      Assertion.Require(existingUser == null,
                        $"Ya existe otro usuario con el identificador proporcionado: '{fields.UserID}'");

      PersonFields personFields = MapToPersonFields(fields);

      personFields.FormerId = SubjectsDataService.TryGetFormerParticipantId(fields.UserID);

      var person = new Person(personFields);

      person.Save();

      var editor = new SubjectSecurityItemsEditor(person);

      editor.CreateSubjectCredentials(fields);

      var subject = SubjectsDataService.GetSubject(person);

      SubjectsDataService.WriteAsParticipant(subject);

      EmpiriaLog.UserManagementLog(subject.Contact, "Alta de usuario");

      return SubjectMapper.Map(subject);
    }


    public void RemoveSubject(string subjectUID) {
      Assertion.Require(subjectUID, nameof(subjectUID));

      var contact = Contact.Parse(subjectUID);

      contact.ChangeStatus(EntityStatus.Deleted);

      contact.Save();

      var editor = new SubjectSecurityItemsEditor(contact);

      editor.RemoveSubjectCredentials();

      var subject = SubjectsDataService.GetSubject(contact);

      EmpiriaLog.UserManagementLog(subject.Contact, "Baja de usuario");
    }


    public FixedList<SubjectDto> SearchSubjects(SubjectsQuery query) {
      Assertion.Require(query, nameof(query));

      string filter = query.MapToFilterString();

      FixedList<SubjectData> users = SubjectsDataService.SearchSubjects(filter);

      return SubjectMapper.Map(users);
    }


    public SubjectDto SuspendSubject(string subjectUID) {
      Assertion.Require(subjectUID, nameof(subjectUID));

      var contact = Contact.Parse(subjectUID);

      contact.ChangeStatus(EntityStatus.Suspended);

      contact.Save();

      var editor = new SubjectSecurityItemsEditor(contact);

      editor.SuspendSubjectCredentials();

      EmpiriaPrincipal.CloseAllSessions(contact);

      var subject = SubjectsDataService.GetSubject(contact);

      EmpiriaLog.UserManagementLog(subject.Contact, "Suspensión de la cuenta de usuario");

      return SubjectMapper.Map(subject);
    }


    public SubjectDto UpdateSubject(string subjectUID, SubjectFields fields) {
      Assertion.Require(subjectUID, nameof(subjectUID));
      Assertion.Require(fields, nameof(fields));

      fields.EnsureValid();

      PersonFields personFields = MapToPersonFields(fields);

      var person = Person.Parse(subjectUID);

      person.Update(personFields);

      person.Save();

      var subject = SubjectsDataService.GetSubject(person);

      SubjectsDataService.WriteAsParticipant(subject);

      EmpiriaLog.UserManagementLog(subject.Contact, "Modificación de usuario");

      return SubjectMapper.Map(subject);
    }


    public FixedList<NamedEntityDto> Workareas() {
      FixedList<Organization> workareas = SubjectsDataService.Workareas();

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
