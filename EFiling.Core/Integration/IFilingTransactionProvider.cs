/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Adapter Interface                       *
*  Type     : IFilingTransactionProvider                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Interface used to connect with external filing transaction providers.                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Interface used to connect with external filing transaction providers.</summary>
  public interface IFilingTransactionProvider {


    IFilingTransaction CreateTransaction(EFilingRequest filingRequest);


    IFilingTransaction GetTransaction(string transactionUID);


    IFilingTransaction SetPayment(string transactionUID, string receiptNo);


    IFilingTransaction SubmitTransaction(string transactionUID);


    EPayments.PaymentOrderDTO TryGetPaymentOrderData(string transactionUID);


    IFilingTransaction UpdateTransaction(EFilingRequest filingRequest);


  }  // interface IFilingTransactionProvider

}  // namespace Empiria.OnePoint.EFiling
