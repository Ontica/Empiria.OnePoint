/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Security Management                          Component : Providers                             *
*  Assembly : Empiria.OnePoint.Security.dll                Pattern   : Enumeration                           *
*  Types    : MessagingEvents                              License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : List of messaging events dispatched by the services offered by this component.                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.Security.Providers {

  /// <summary>List of messaging events dispatched by the services offered by this component.</summary>
  internal enum MessagingEvents {

    UserPasswordCreated,

    UserPasswordChanged

  } // enum MessagingEvents

}  // namespace Empiria.OnePoint.Security.Providers
