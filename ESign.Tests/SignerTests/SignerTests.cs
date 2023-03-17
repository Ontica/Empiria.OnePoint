/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                     Component : Test cases                            *
*  Assembly : Empiria.OnePoint.ESign.Tests                 Pattern   : Services tests                        *
*  Type     : SignerTests                                  License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Test cases for the signer service.                                                             *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Xunit;

using Empiria.OnePoint.ESign.Services;

namespace Empiria.OnePoint.ESign.Tests {

  /// <summary>Test cases for the signer service.</summary>
  public class SignerTests {

    #region Facts

    [Fact]
    public void Should_Sign_A_Message() {
      var service = new SignerService();

      string message = "This is the message to be signed.";

      string sut = service.Sign(message);

      Assert.NotNull(sut);
      Assert.NotEqual(sut, message);
    }

    [Fact]
    public void Should_Not_Sign_A_Null_Or_Empty_Message() {
      var sut = new SignerService();

      Assert.Throws<AssertionFailsException>(() => sut.Sign(null));

      Assert.Throws<AssertionFailsException>(() => sut.Sign(string.Empty));
    }

    #endregion Facts

  }  // class SignerTests

} // namespace Empiria.OnePoint.ESign.Tests
