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

using Empiria.OnePoint.EPayments;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Interface used to connect with external filing transaction providers.</summary>
  public interface IFilingTransactionProvider {


    IPayable CreateTransaction(EFilingRequest filingRequest);


    IFilingTransaction GetTransaction(string transactionUID);


    IPayable GetTransactionAsPayable(string transactionUID);


    void MarkAsReceived(string transactionUID);


    IFilingTransaction SetPayment(string transactionUID, string receiptNo);


    IFilingTransaction SetPaymentOrder(IPayable transaction, OnePoint.EPayments.PaymentOrderDTO paymentOrderData);


    IFilingTransaction SubmitTransaction(string transactionUID);


    EPayments.PaymentOrderDTO TryGetPaymentOrderData(string transactionUID);


    IFilingTransaction UpdateTransaction(EFilingRequest filingRequest);

  }  // interface IFilingTransactionProvider

}  // namespace Empiria.OnePoint.EFiling
