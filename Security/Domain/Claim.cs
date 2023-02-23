/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Domain Layer                          *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Information holder                    *
*  Type     : Claim                                        License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Contains attributes that identifies a subject, like a userID or password.                      *
*             Subjects are users, applications, systems, services or computers.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security.Providers;

using Empiria.OnePoint.Security.Data;

namespace Empiria.OnePoint.Security {

  /// <summary>Contains attributes that identifies a subject, like a userID or password.
  /// Subjects are users, applications, systems, services or computers.</summary>
  internal class Claim : SecurityItem, ISubjectClaim {

    #region Constructors and parsers

    internal Claim(SecurityItemType powerType) : base(powerType) {
      // Required by Empiria Framework for all partitioned types.
    }


    static internal new Claim Parse(int id) {
      return BaseObject.ParseId<Claim>(id);
    }


    static internal Claim TryParse(SecurityItemType claimType,
                                   IIdentifiable app,
                                   int subjectId) {
      var context = SecurityContext.Parse(52);

      return SecurityItemsDataReader.TryGetSubjectItemWithId<Claim>(context,
                                                                    claimType,
                                                                    subjectId);
    }


    static internal Claim TryParseWithKey(SecurityItemType claimType,
                                          IIdentifiable app,
                                          string securityKey) {
      var context = SecurityContext.Parse(52);

      return SecurityItemsDataReader.TryGetSubjectItemWithKey<Claim>(context,
                                                                     claimType,
                                                                     securityKey);
    }

    #endregion Constructors and parsers

    #region Properties

    public string Key {
      get {
        return base.BaseKey;
      }
    }


    public int SubjectId {
      get {
        return base.BaseSubjectId;
      }
    }


    internal T GetAttribute<T>(string attributeName) {
      Assertion.Require(attributeName, nameof(attributeName));

      return base.ExtensionData.Get<T>(attributeName);
    }

    #endregion Properties

  }  // class Claim

}  // namespace Empiria.OnePoint.Security
