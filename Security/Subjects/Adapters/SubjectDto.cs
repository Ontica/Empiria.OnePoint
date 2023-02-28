/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management                 Component : Interface adapters                    *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Data Transfer Object                  *
*  Types    : SubjectDto, SubjectFields                    License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Data transfer objects for subjects information.                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;

namespace Empiria.OnePoint.Security.Subjects.Adapters {

  public class SubjectDto {

  }  // class SubjectDto



  /// <summary>Fields for create or update subjects.</summary>
  public class SubjectFields {

    public string FullName {
      get; set;
    } = string.Empty;


    public string UserID {
      get; set;
    } = string.Empty;


    public string EMail {
      get; set;
    } = string.Empty;


    public string WorkareaUID {
      get; set;
    } = string.Empty;


    public string JobPosition {
      get; set;
    } = string.Empty;


    public string EmployeeNo {
      get; set;
    } = string.Empty;

  }  // class SubjectFields



  static internal class SubjectFieldsExtension {

    static internal void EnsureValid(this SubjectFields fields) {
      Assertion.Require(fields.FullName, "fields.FullName");
      Assertion.Require(fields.UserID, "fields.UserID");
      Assertion.Require(fields.EMail, "fields.EMail");
    }


    static internal Organization GetWorkarea(this SubjectFields fields) {
      return Organization.Parse(fields.WorkareaUID);
    }

  }  // class SubjectFieldsExtension

}  // namespace Empiria.OnePoint.Security.Subjects.Adapters
