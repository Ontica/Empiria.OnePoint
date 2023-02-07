﻿/* Empiria Core **********************************************************************************************
*                                                                                                            *
*  Module   : User Management                            Component : Data Access Layer                       *
*  Assembly : Empiria.Core.Services.dll                  Pattern   : Data Services                           *
*  Type     : UserManagementData                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data read and write methods for user management services.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

using Empiria.Security;

using Empiria.OnePoint.Security.UserManagement;

namespace Empiria.OnePoint.Security.Data {

  static internal class UserManagementData {

    static internal FixedList<UserData> SearchUsers(string keywords) {
      string sql = "SELECT * FROM Contacts " +
                   "WHERE UserName IS NOT NULL OR UserName <> ''";

      return DataReader.GetPlainObjectFixedList<UserData>(DataOperation.Parse(sql));
    }


    static internal void ChangePassword(string username, string password) {
      if (ConfigurationData.Get("UseFormerPasswordEncryption", false)) {
        ChangePasswordUsingFormerEncryption(username, password);
        return;
      }

      var dataRow = DataReader.GetDataRow(DataOperation.Parse("getContactWithUserName", username));

      if (dataRow == null) {
        throw new SecurityException(SecurityException.Msg.InvalidUserCredentials);
      }

      string p;

      bool useSecurityModelV3 = ConfigurationData.Get("UseSecurityModel.V3", false);

      if (useSecurityModelV3) {
        p = Cryptographer.Encrypt(EncryptionMode.EntropyKey,
                                  Cryptographer.GetSHA256(password), username);

      } else {
        p = FormerCryptographer.Encrypt(EncryptionMode.EntropyKey,
                                        FormerCryptographer.GetMD5HashCode(password), username);
      }

      string sql = $"UPDATE Contacts SET UserPassword = '{p}' WHERE UserName = '{username}'";

      DataWriter.Execute(DataOperation.Parse(sql));
    }

    #region Helpers

    static private void ChangePasswordUsingFormerEncryption(string username, string password) {
      var dataRow = DataReader.GetDataRow(DataOperation.Parse("getContactWithUserName", username));
      if (dataRow == null) {
        throw new SecurityException(SecurityException.Msg.InvalidUserCredentials);
      }

      password = FormerCryptographer.Encrypt(EncryptionMode.EntropyHashCode, password, username);
      password = FormerCryptographer.Decrypt(password, username);

      password = FormerCryptographer.Encrypt(EncryptionMode.EntropyKey, password, username);

      string sql = "UPDATE Contacts SET UserPassword = '{0}' WHERE UserName = '{1}'";

      DataWriter.Execute(DataOperation.Parse(string.Format(sql, password, username)));
    }

    #endregion Helpers

  } // class SecurityData

} // namespace Empiria.OnePoint.Security.Data
