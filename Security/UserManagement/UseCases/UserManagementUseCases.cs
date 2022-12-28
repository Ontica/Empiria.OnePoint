/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : User Management                              Component : Use cases Layer                       *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Use case interactor                   *
*  Type     : UserManagementUseCases                       License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Use cases for user management.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Services;

using Empiria.OnePoint.Security.Data;

using Empiria.OnePoint.Security.UserManagement.Adapters;

namespace Empiria.OnePoint.Security.UserManagement.UseCases {

  /// <summary>Use cases for user management.</summary>
  public class UserManagementUseCases : UseCase {

    #region Constructors and parsers

    protected UserManagementUseCases() {
      // no-op
    }

    static public UserManagementUseCases UseCaseInteractor() {
      return CreateInstance<UserManagementUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public void CreateUser(UserFields userFields) {
      Assertion.Require(userFields, nameof(userFields));

      throw new NotImplementedException();
    }


    public void RemoveUser(string userUID) {
      Assertion.Require(userUID, nameof(userUID));

      throw new NotImplementedException();
    }


    public FixedList<UserDto> SearchUsers(string keywords) {
      var users = UserManagementData.SearchUsers(keywords);

      return users.Select(x => (UserDto) x)
                  .ToFixedList();
    }

    public UserDto UpdateUser(string userUID, UserFields userFields) {
      throw new NotImplementedException();
    }

    #endregion Helpers

  }  // class UserManagementUseCases

}  // namespace Empiria.OnePoint.Security.UserManagement.UseCases
