/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing                          Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.dll                       Pattern   : Application Service interface           *
*  Type     : ITransactionAppService                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Acts as an abstract class that holds data for an external transaction request, that may be     *
*             integrated into an Empiria Land transaction.                                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.OnePoint.EFiling {

 public interface IFilingTransactionProvider {

    IFilingTransaction CreateTransaction(EFilingRequest filingRequest);


    IFilingTransaction GetTransaction(string transactionUID);


    IFilingTransaction SetPayment(string transactionUID, JsonObject paymentData);


    IFilingTransaction SubmitTransaction(string transactionUID);


    IFilingTransaction UpdateTransaction(EFilingRequest filingRequest);


  }  // interface IFilingTransactionProvider


}  // namespace Empiria.OnePoint.EFiling
