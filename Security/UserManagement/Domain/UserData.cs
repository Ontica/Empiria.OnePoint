/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : User Management                              Component : Domain Layer                          *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Information Holder                    *
*  Type     : UserData                                     License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Holds user data.                                                                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.StateEnums;

using Empiria.OnePoint.Security.UserManagement.Adapters;

namespace Empiria.OnePoint.Security.UserManagement {

  /// <summary>Holds user data.</summary>
  internal class UserData : UserDto {

    [DataField("ContactId")]
    internal int Id {
      get; private set;
    }


    [DataField("ContactTypeId")]
    internal int TypeId {
      get; private set;
    }


    [DataField("ContactUID")]
    public string UID {
      get; private set;
    }


    [DataField("ContactFullName")]
    public string FullName {
      get; private set;
    }


    [DataField("NickName")]
    public string NickName {
      get; private set;
    }


    public string BusinessID {
      get {
        return Id.ToString("000000000");
      }
    }


    public string Workplace {
      get {
        return "Subdirección de Registro Contable";
      }
    }


    [DataField("UserName")]
    public string UserID {
      get; private set;
    }


    [DataField("ContactEMail")]
    public string EMail {
      get; private set;
    }


    public DateTime LastAccess {
      get {
        return DateTime.Today.AddDays(EmpiriaMath.GetRandom(-20, 0));
      }
    }


    public bool IsSystem {
      get {
        return TypeId == 103;
      }
    }


    [DataField("ContactStatus", Default = EntityStatus.Suspended)]
    public EntityStatus Status {
      get; private set;
    }

  }  // class UserData

} // namespace Empiria.OnePoint.Security.UserManagement
