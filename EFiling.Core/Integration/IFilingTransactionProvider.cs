/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Adapter Interface                       *
*  Type     : IFilingTransactionProvider                 License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Interface used to connect with external filing transaction providers.                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using Empiria.OnePoint.EPayments;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Interface used to connect with external filing transaction providers.</summary>
  public interface IFilingTransactionProvider {


    IPayable CreateTransaction(EFilingRequest filingRequest);


    void EventProcessed(string transactionUID, string eventName);


    FixedList<EFilingDocument> GetOutputDocuments(string transactionUID);


    IFilingTransaction GetTransaction(string transactionUID);


    IPayable GetTransactionAsPayable(string transactionUID);


    IFilingTransaction SetPayment(string transactionUID, string receiptNo);


    IFilingTransaction SetPaymentOrder(IPayable transaction, PaymentOrderDTO paymentOrderData);


    IFilingTransaction SubmitTransaction(string transactionUID);


    PaymentOrderDTO TryGetPaymentOrderData(string transactionUID);


    IFilingTransaction UpdateTransaction(EFilingRequest filingRequest);


  }  // interface IFilingTransactionProvider

}  // namespace Empiria.OnePoint.EFiling
