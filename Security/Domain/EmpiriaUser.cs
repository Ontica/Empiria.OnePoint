﻿/* Empiria OnePoint ******************************************************************************************
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
using Empiria.Security.Claims;

namespace Empiria.OnePoint.Security {

  /// <summary>Represents a system's user.</summary>
  internal sealed class EmpiriaUser : IEmpiriaUser {

    #region Constructors and parsers

    private EmpiriaUser() {
      // Required by Empiria Framework
    }


    static private EmpiriaUser Parse(Claim userData) {
      Assertion.Require(userData, nameof(userData));

      return new EmpiriaUser {
        Contact = Contact.Parse(userData.SubjectId),
        UserName = userData.Key,
        Status = userData.Status,
      };
    }


    static public EmpiriaUser Parse(string username, string email) {
      Assertion.Require(username, nameof(username));
      Assertion.Require(email, nameof(email));

      var service = new OnePoint.Security.Services.AuthenticationService();

      Claim userData = service.TryGetUserWithUserName(EmpiriaPrincipal.Current.ClientApp, username);

      if (userData == null) {
        throw new SecurityException(SecurityException.Msg.UserWithEMailNotFound, username, email);
      }

      var user = EmpiriaUser.Parse(userData);

      if (user.EMail.Equals(email)) {
        return user;
      } else {
        throw new SecurityException(SecurityException.Msg.UserWithEMailNotFound, username, email);
      }
    }


    /// <summary>Determines whether a contact belongs to the specified role.</summary>
    static public bool IsInRole(Contact user, string role) {
      Assertion.Require(user, nameof(user));
      Assertion.Require(role, nameof(role));

      var service = new OnePoint.Security.Services.AuthorizationService();

      return service.IsSubjectInRole(user, EmpiriaPrincipal.Current.ClientApp, role);
    }


    #endregion Constructors and parsers

    #region Authenticate methods

    static internal IEmpiriaUser Authenticate(IClientApplication app,
                                              string username,
                                              string password,
                                              string entropy) {
      Assertion.Require(app, nameof(app));
      Assertion.Require(username, nameof(username));
      Assertion.Require(password, nameof(password));
      Assertion.Require(entropy, nameof(entropy));

      var service = new OnePoint.Security.Services.AuthenticationService();

      Claim userData = service.Authenticate(app, username, password, entropy);

      var user = EmpiriaUser.Parse(userData);

      user.EnsureCanAuthenticate();

      user.IsAuthenticated = true;

      return user;
    }


    static internal IEmpiriaUser Authenticate(IEmpiriaSession activeSession) {
      Assertion.Require(activeSession, nameof(activeSession));

      if (!activeSession.IsStillActive) {
        throw new SecurityException(SecurityException.Msg.ExpiredSessionToken,
                                    activeSession.Token);
      }

      var service = new OnePoint.Security.Services.AuthenticationService();

      Claim userData = service.TryGetUser(activeSession);

      if (userData == null) {
        throw new SecurityException(SecurityException.Msg.EnsureClaimFailed);
      }

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


    string IClaimsSubject.ClaimsToken {
      get {
        return this.Contact.UID;
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
