﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Domain Layer                          *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : PowerType                             *
*  Type     : SecurityItemType                             License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Powertype used to describe security items.                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Ontology;

namespace Empiria.OnePoint.Security {

  /// <summary>Powertype used to describe security items.</summary>
  [Powertype(typeof(SecurityItem))]
  internal class SecurityItemType : Powertype {

    #region Constructors and parsers

    private SecurityItemType() {
      // Empiria power types always have this constructor.
    }


    static public new SecurityItemType Parse(int typeId) {
      return ObjectTypeInfo.Parse<SecurityItemType>(typeId);
    }


    static internal new SecurityItemType Parse(string typeName) {
      return ObjectTypeInfo.Parse<SecurityItemType>(typeName);
    }


    static public SecurityItemType ClientAppFeature
                => Parse("ObjectType.SecurityItem.Feature.ClientAppFeature");


    static public SecurityItemType ClientAppRole
                => Parse("ObjectType.SecurityItem.Role.ClientAppRole");


    static public SecurityItemType SubjectCredentials
                => Parse("ObjectType.SecurityItem.Claim.SubjectCredentials");


    static public SecurityItemType SubjectFeature
                => Parse("ObjectType.SecurityItem.Feature.SubjectFeature");


    static public SecurityItemType SubjectRole
                => Parse("ObjectType.SecurityItem.Role.SubjectRole");


    #endregion Constructors and parsers

  } // class SecurityItemType

} // namespace Empiria.OnePoint.Security