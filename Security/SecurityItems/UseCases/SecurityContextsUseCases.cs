/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Items                               Component : Use cases Layer                       *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Use case interactor                   *
*  Type     : SecurityContextsUseCases                     License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Use cases that retrieve security contexts information.                                         *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Services;

namespace Empiria.OnePoint.Security.SecurityItems.UseCases {

  /// <summary>Use cases that retrieve security contexts information.</summary>
  public class SecurityContextsUseCases : UseCase {

    #region Constructors and parsers

    protected SecurityContextsUseCases() {
      // no-op
    }

    static public SecurityContextsUseCases UseCaseInteractor() {
      return CreateInstance<SecurityContextsUseCases>();
    }

    #endregion Constructors and parsers

    #region Use cases

    public FixedList<NamedEntityDto> SecurityContexts() {
      var contexts = SecurityContext.GetList();

      return contexts.MapToNamedEntityList();
    }

    #endregion Helpers

  }  // class SecurityContextsUseCases

}  // namespace Empiria.OnePoint.Security.SecurityItems.UseCases
