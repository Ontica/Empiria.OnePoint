/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : E-Sign Services                         *
*  Assembly : Empiria.OnePoint.AppServices.dll             Pattern : Application Service                     *
*  Type     : ESignServices                                License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic signature services.                                       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Empiria.Contacts;
using Empiria.Security;

namespace Empiria.OnePoint.ESign {

  /// <summary>Use cases that implements electronic signature services.</summary>
  static public class ESignServices {

    #region Query services

    /// <summary>Gets a list of pending documents to sign for the current authenticated user.</summary>
    static public Task<FixedList<SignRequest>> GetMyPendingSignRequests(string filter, string sort) {
      Contact me = EmpiriaUser.Current.AsContact();

      // ToDo: Assert 'me' is a valid e-signer contact.
      // ToDo: Do something with filter and sort before send them as parameters.

      return Task.FromResult(SignServicesRepository.GetPendingSignRequests(me, filter, sort));
    }


    /// <summary>Gets a list of electronically signed documents by the current authenticated user.</summary>
    static public Task<FixedList<SignRequest>> GetMySignedRequests(string filter, string sort) {
      Contact me = EmpiriaUser.Current.AsContact();

      // ToDo: Assert 'me' is a valid e-signer contact.
      // ToDo: Do something with filter and sort before send them as parameters.

      return Task.FromResult(SignServicesRepository.GetSignedRequests(me, filter, sort));
    }


    /// <summary>Gets a list of refused documents to sign by the current authenticated user.</summary>
    static public Task<FixedList<SignRequest>> GetMyRefusedToSignRequests(string filter, string sort) {
      Contact me = EmpiriaUser.Current.AsContact();

      // ToDo: Assert 'me' is a valid e-signer contact.
      // ToDo: Do something with filter and sort before send them as parameters.

      return Task.FromResult(SignServicesRepository.GetRefusedRequests(me, filter, sort));
    }

    #endregion Query services

  }  // class Empiria.OnePoint.ESign

}  // namespace Empiria.OnePoint.AppServices
