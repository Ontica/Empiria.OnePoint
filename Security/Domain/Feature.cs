/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Domain Layer                          *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Information holder                    *
*  Type     : Feature                                      License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Holds information about a system feature.                                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Security;

using Empiria.OnePoint.Security.Data;

namespace Empiria.OnePoint.Security {

  /// <summary>Holds information about a system feature.</summary>
  internal class Feature : SecurityItem, INamedEntity {

    #region Constructors and parsers

    private Feature(SecurityItemType powerType) : base(powerType) {
      // Required by Empiria Framework for all partitioned types.
    }


    static internal new Feature Parse(int id) {
      return BaseObject.ParseId<Feature>(id);
    }


    static internal Feature Parse(string featureKey) {
      var feature = BaseObject.TryParse<Feature>($"SecurityItemKey = '{featureKey}'");

      if (feature != null) {
        return feature;
      }

      return Feature.Empty;
    }


    static internal FixedList<Feature> GetList(SecurityContext context) {
      return SecurityItemsDataReader.GetContextItems<Feature>(context, SecurityItemType.ClientAppFeature);
    }


    static internal FixedList<Feature> GetList(IIdentifiable subject, SecurityContext context) {
      return SecurityItemsDataReader.GetSubjectTargetItems<Feature>(subject, context,
                                                                    SecurityItemType.SubjectFeature);
    }

    static public new Feature Empty => ParseEmpty<Feature>();

    #endregion Constructors and parsers

    #region Properties

    public string Key {
      get {
        return base.BaseKey;
      }
    }


    public string Name {
      get {
        return ExtensionData.Get("featureName", this.Key);
      }
    }


    public bool IsAssignable {
      get {
        return ExtensionData.Get("assignable", true);
      }
    }


    ObjectAccessRule[] _objectsGrants;

    public ObjectAccessRule[] ObjectsGrants {
      get {
        if (_objectsGrants == null) {
          _objectsGrants = ExtensionData.GetList<ObjectAccessRule>("objectsGrants", false)
                                        .ToArray();

        }

        return _objectsGrants;
      }
    }


    Feature[] _requires;

    public Feature[] Requires {
      get {

        if (_requires == null) {
          _requires = ExtensionData.GetList<Feature>("requires", false)
                                   .ToArray();

        }

        return _requires;
      }
    }

    #endregion Properties

  }  // class Feature

}  // namespace Empiria.OnePoint.Security
