/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Subjects Management               Component : Data Access Layer                       *
*  Assembly : Empiria.OnePoint.Security.dll              Pattern   : Data Services                           *
*  Type     : SubjectsData                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data read and write methods for user management services.                                      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Contacts;
using Empiria.Data;
using Empiria.Security;

using Empiria.OnePoint.Security.Subjects;

namespace Empiria.OnePoint.Security.Data {

  static internal class SubjectsData {

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


    static internal FixedList<SubjectData> SearchSubjects(string filter) {
      string sql = "SELECT * FROM " +
                   "SecurityItems INNER JOIN Contacts " +
                   "ON SecurityItems.SubjectId = Contacts.ContactId " +
                   $"WHERE SecurityItemTypeId = {SecurityItemType.SubjectCredentials.Id}";


      if (filter.Length != 0) {
        sql += " AND " + filter;
      }

      sql += $" ORDER BY ContactFullName";

      return DataReader.GetPlainObjectFixedList<SubjectData>(DataOperation.Parse(sql));
    }


    static internal FixedList<Organization> Workareas() {
      string sql = "SELECT * FROM Contacts " +
                   "WHERE ContactTypeId = 103 AND " +
                   "ContactTags LIKE '%systems-users-org%' AND " +
                   "ContactStatus <> 'X' " +
                   "ORDER BY ContactFullName";

      return DataReader.GetFixedList<Organization>(DataOperation.Parse(sql));
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

  } // class SubjectsData


} // namespace Empiria.OnePoint.Security.Data
