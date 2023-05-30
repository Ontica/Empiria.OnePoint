/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security                                     Component : Authentication Services               *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Information holder                    *
*  Type     : EmpiriaUser                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Represents a system's user.                                                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.StateEnums;

using Empiria.Security;

namespace Empiria.OnePoint.Security {

  /// <summary>Represents a system's user.</summary>
  internal sealed class EmpiriaUser : IEmpiriaUser {

    #region Constructors and parsers

    private EmpiriaUser() {
      // Required by Empiria Framework
    }


    static internal EmpiriaUser Parse(Claim userData) {
      Assertion.Require(userData, nameof(userData));

      return new EmpiriaUser {
        Contact = Contact.Parse(userData.SubjectId),
        UserName = userData.Key,
        Status = userData.Status,
        IsAuthenticated = false
      };
    }


    #endregion Constructors and parsers

    #region Authenticate methods

    static internal IEmpiriaUser Authenticate(Claim userData) {
      Assertion.Require(userData, nameof(userData));

      var user = EmpiriaUser.Parse(userData);

      user.EnsureCanAuthenticate();

      user.IsAuthenticated = true;

      return user;
    }

    #endregion Authenticate methods

    #region Public propertiese

    public bool IsActive {
      get {
        return this.Status == EntityStatus.Active;
      }
    }

    public bool IsAuthenticated {
      get;
      private set;
    }

    public bool PasswordExpired {
      get;
      private set;
    }

    public string UserName {
      get;
      private set;
    }


    public string FullName {
      get {
        return this.Contact.FullName;
      }
    }


    public string EMail {
      get {
        return this.Contact.EMail;
      }
    }

    public Contact Contact {
      get; private set;
    }


    private EntityStatus Status {
      get; set;
    }

    #endregion Public properties

    #region Private methods

    private void EnsureCanAuthenticate() {

      if (!this.IsActive) {
        throw new SecurityException(SecurityException.Msg.NotActiveUser, this.UserName);
      }

      if (this.PasswordExpired) {
        throw new SecurityException(SecurityException.Msg.UserPasswordExpired, this.UserName);
      }
    }

    #endregion Private methods

  } // class EmpiriaUser

} // namespace Empiria.OnePoint.Security
