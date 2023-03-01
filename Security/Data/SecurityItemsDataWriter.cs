/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Data access layer                     *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Write data service using data adapter *
*  Type     : SecurityItemsDataWriter                      License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Persists security items data.                                                                  *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;
using Empiria.StateEnums;

namespace Empiria.OnePoint.Security.Data {

  /// <summary>Persists security items data.</summary>
  static internal class SecurityItemsDataWriter {

    #region Methods

    static internal void CreateSecurityItem(SecurityItemDataDto o) {
      o.AssignIdentificator(GetNextSecurityItemId());

      WriteSecurityItem(o);
    }


    static internal void RemoveSecurityItem(SecurityItemDataDto o) {
      o.Status = EntityStatus.Deleted;

      WriteSecurityItem(o);
    }

    internal static void UpdateSecurityItem(SecurityItemDataDto o) {
      WriteSecurityItem(o);
    }

    #endregion Methods

    #region Helpers

    static int GetNextSecurityItemId() {
      return DataWriter.CreateId("SecurityItems");
    }

    static void WriteSecurityItem(SecurityItemDataDto o) {
      var op = DataOperation.Parse("writeSecurityItem", o.Id, o.UID, o.SecurityItemType.Id,
                                   o.ContextId, o.SubjectId, o.TargetId,
                                   o.Key, o.ExtData.ToString(), o.Keywords,
                                   o.LastUpdate, o.UpdatedById,
                                   (char) o.Status, o.DataIntegrity);

      DataWriter.Execute(op);
    }

    #endregion Helpers

  }  // class SecurityItemsDataWriter

}  // namespace Empiria.OnePoint.Security.Data
