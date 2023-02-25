/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security                                     Component : Data access layer                     *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Data service                          *
*  Type     : SessionData                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Empiria sessions data service.                                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Data;

using Empiria.Security;

namespace Empiria.OnePoint.Security.Data {

  /// <summary>Empiria sessions data service.</summary>
  static internal class SessionData {

    static internal void CloseSession(EmpiriaSession o) {
      var op = DataOperation.Parse("doCloseUserSession", o.Token, o.EndTime);

      DataWriter.Execute(op);
    }


    static internal int CreateSession(EmpiriaSession o) {
      var op = DataOperation.Parse("apdUserSession", o.Token, o.ServerId,
                        o.ClientAppId, o.UserId, o.ExpiresIn,
                        o.RefreshToken, o.UserHostAddress,
                        o.ExtendedData.ToString(), o.StartTime, o.EndTime);

      return DataWriter.Execute<int>(op);
    }


    static internal EmpiriaSession GetSession(string sessionToken) {
      var op = DataOperation.Parse("getUserSession", sessionToken);

      var session = DataReader.GetPlainObject<EmpiriaSession>(op, null);

      if (session == null) {
        throw new SecurityException(SecurityException.Msg.SessionTokenNotFound, sessionToken);
      }

      return session;
    }

  } // class SessionData

} // namespace Empiria.Security.Data
