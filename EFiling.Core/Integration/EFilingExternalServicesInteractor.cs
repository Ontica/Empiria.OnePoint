/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases Layer                         *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases class                         *
*  Type     : EFilingExternalServicesInteractor          License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Use cases that implements electronic filing services.                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

using Empiria.OnePoint.EPayments;

namespace Empiria.OnePoint.EFiling {

  internal class EFilingExternalServicesInteractor {

    #region Fields

    internal readonly EFilingRequest FilingRequest;

    private string TransactionUID => this.FilingRequest.Transaction.UID;

    #endregion Fields

    #region Constructors and parsers

    internal EFilingExternalServicesInteractor(EFilingRequest filingRequest) {
      this.FilingRequest = filingRequest;

      this.SetTransactionProvider();
    }


    #endregion Constructors and parsers


    #region Transaction Methods

    internal Task<IPayable> CreateTransaction() {
      Assertion.Assert(!this.FilingRequest.HasTransaction,
                       "This filing already has a transaction.");

      var provider = this.GetTransactionProvider();

      return Task.Run(() => {
        return provider.CreateTransaction(this.FilingRequest);
      });
    }


    internal Task<FixedList<EFilingDocument>> GetOutputDocuments() {
      Assertion.Assert(this.FilingRequest.HasTransaction,
                       "This filing has not been linked to a transaction.");

      var provider = this.GetTransactionProvider();

      return Task.Run(() => {
        return provider.GetOutputDocuments(this.TransactionUID);
      });
    }


    internal Task<IFilingTransaction> GetTransaction() {
      Assertion.Assert(this.FilingRequest.HasTransaction,
                       "This filing has not been linked to a transaction.");

      var provider = this.GetTransactionProvider();

      return Task.Run(() => {
        return provider.GetTransaction(this.TransactionUID);
      });
    }


    internal void InformEventProcessed(string transactionUID, string eventName) {
      Assertion.AssertObject(transactionUID, "transactionUID");
      Assertion.AssertObject(eventName, "eventName");

      var provider = this.GetTransactionProvider();

      provider.EventProcessed(transactionUID, eventName);
    }


    internal Task<IFilingTransaction> Submit() {
      Assertion.Assert(this.FilingRequest.HasTransaction,
                       "This filing has not been linked to a transaction.");

      var provider = this.GetTransactionProvider();

      return Task.Run(() => {
        return provider.SubmitTransaction(this.TransactionUID);
      });
    }


    internal Task<IFilingTransaction> UpdateExternalTransaction() {
      var provider = this.GetTransactionProvider();

      return Task.Run(() => {
        return provider.UpdateTransaction(this.FilingRequest);
      });
    }


    #endregion Transaction Methods


    #region Payment Order methods


    internal async Task<FormerPaymentOrderDTO> GeneratePaymentOrder() {
      Assertion.Assert(FilingRequest.HasTransaction,
                       "This filing has not be linked to a transaction.");
      Assertion.Assert(!FilingRequest.HasPaymentOrder,
                       $"This filing already has a payment order.");

      var provider = this.GetTransactionProvider();

      IPayable transaction = provider.GetTransactionAsPayable(this.TransactionUID);

      FormerPaymentOrderDTO paymentOrder = await GeneratePaymentOrder(transaction).ConfigureAwait(false);

      provider.SetPaymentOrder(transaction, paymentOrder);

      return paymentOrder;
    }


    internal Task<FormerPaymentOrderDTO> TryGetPaymentOrder() {
      var provider = this.GetTransactionProvider();

      return Task.Run(() => {
        return provider.TryGetPaymentOrderData(this.TransactionUID);
      });
    }


    internal Task<IFilingTransaction> SetPayment() {
      Assertion.Assert(this.FilingRequest.HasTransaction,
                       "This filing has not been linked to a transaction.");
      Assertion.AssertObject(this.FilingRequest.PaymentOrder.ReceiptNo,
                             "This filing payment order has not a payment receipt.");

      var provider = this.GetTransactionProvider();

      return Task.Run(() => {
        return provider.SetPayment(this.TransactionUID,
                                   this.FilingRequest.PaymentOrder.ReceiptNo);
      });
    }


    static private async Task<FormerPaymentOrderDTO> GeneratePaymentOrder(IPayable transaction) {
      try {
        bool USE_PAYMENT_ORDER_MOCK_SERVICE = ConfigurationData.Get("UsePaymentOrderMockService", false);

        if (USE_PAYMENT_ORDER_MOCK_SERVICE) {
          return PaymentOrderMockData();
        }

        return await EPaymentsUseCases.RequestPaymentOrderData(transaction)
                                      .ConfigureAwait(false);

      } catch (Exception e) {
        throw new ServiceException("GeneratePaymentOrder.ServiceUnavailable",
                                   "El servicio externo de la Secretaría de Finanzas " +
                                   "que genera las órdenes de pago no está disponible. " +
                                   "Favor de intentar más tarde.", e);
      }
    }


    static private FormerPaymentOrderDTO PaymentOrderMockData() {
      var routeNumber = EmpiriaString.BuildRandomString(16);
      var controlTag = EmpiriaString.BuildRandomString(6);

      return new FormerPaymentOrderDTO(routeNumber, DateTime.Today.AddDays(20), controlTag);
    }


    #endregion Payment Order methods


    #region Utility methods

    private IFilingTransactionProvider _transactionProvider;

    private IFilingTransactionProvider GetTransactionProvider() {
      return _transactionProvider;
    }


    private void SetTransactionProvider() {
      Procedure procedure = this.FilingRequest.Procedure;

      _transactionProvider = procedure.GetFilingTransactionProvider();
    }


    #endregion Utility methods

  }  // class EFilingExternalServicesInteractor

} // namespace Empiria.OnePoint.EFiling
