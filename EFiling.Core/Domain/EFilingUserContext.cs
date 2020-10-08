/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Information Provider                    *
*  Type     : EFilingUserContext                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides information about user's operation and security context.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.Contacts;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Provides information about user's operation and security context.</summary>
  internal class EFilingUserContext {

    #region Constructors and parsers


    private EFilingUserContext(Contact user) {
      this.User = user;
    }


    static internal EFilingUserContext Current() {
      Contact currentUser = Contact.Parse(ExecutionServer.CurrentUserId);

      return new EFilingUserContext(currentUser);
    }


    static internal EFilingUserContext ParseFor(Person user) {
      Assertion.AssertObject(user, "user");

      return new EFilingUserContext(user);
    }


    #endregion Constructors and parsers


    #region Properties


    internal Organization Agency {
      get {
        int agencyId = this.User.ExtendedData.Get<int>("eFiling/agencyId");

        return Organization.Parse(agencyId);
      }
    }


    internal Person Agent {
      get {
        int agentId = this.Agency.ExtendedData.Get<int>("eFiling/agentId");

        return Person.Parse(agentId);
      }
    }


    internal bool IsManager {
      get {
        return this.Managers.Contains(this.User);
      }
    }


    internal bool IsRegister {
      get {
        return this.Registers.Contains(this.User);
      }
    }


    internal bool IsSigner {
      get {
        return this.User.Equals(Agent);
      }
    }


    internal FixedList<Contact> Managers {
      get {
        return this.Agency.ExtendedData.GetList<Contact>("eFiling/managers")
                   .ToFixedList();
      }
    }


    internal FixedList<Procedure> Procedures {
      get {
        return this.Agency.ExtendedData.GetList<Procedure>("eFiling/procedures")
                   .ToFixedList();
      }
    }


    internal FixedList<Contact> Registers {
      get {
        return this.Agency.ExtendedData.GetList<Contact>("eFiling/registers")
                   .ToFixedList();
      }
    }


    internal Contact User {
      get;
    }

    #endregion Properties

  }  // class EFilingUserContext

}  // namespace Empiria.OnePoint.EFiling
