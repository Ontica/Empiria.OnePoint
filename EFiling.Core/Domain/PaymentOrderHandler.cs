/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Information Holder                      *
*  Type     : PaymentOrderHandler                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Collaborates with EFilingRequest entities holding their payment order data.                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Threading.Tasks;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Collaborates with EFilingRequest entities holding their payment order data.</summary>
  internal class PaymentOrderHandler {

    #region Fields

    private readonly EFilingRequest _request;

    private readonly EFilingExternalServicesInteractor _externalServices;

    #endregion Fields


    #region Constructors and parsers

    internal PaymentOrderHandler(EFilingRequest request,
                                 EFilingExternalServicesInteractor externalServices) {
      Assertion.AssertObject(request, "request");
      Assertion.AssertObject(externalServices, "externalServices");

      _request = request;
      _externalServices = externalServices;
    }

    #endregion Constructors and parsers


    #region Properties

    public bool HasPaymentOrder {
      get {
         return this.PaymentOrder.RouteNumber.Length != 0;
      }
    }


    internal PaymentOrder PaymentOrder {
      get;
      private set;
    } = PaymentOrder.Empty;

    #endregion Properties


    #region Methods

    internal async Task CreatePaymentOrder() {
      var dto = await _externalServices.GeneratePaymentOrder();

      this.PaymentOrder = new PaymentOrder(dto);
    }


    internal void LoadPaymentOrder() {
      if (_request.Transaction.ExtensionData.Contains("paymentData")) {
        var paymentData = _request.Transaction.ExtensionData.Slice("paymentData");

        this.PaymentOrder = PaymentOrder.Parse(paymentData);
      }
    }


    internal void SetPaymentData() {
      _request.Transaction.ExtensionData.SetIfValue("paymentData", this.PaymentOrder.ToJson());
    }


    internal void SetPaymentReceipt(string receiptNo) {
      Assertion.AssertObject(receiptNo, "receiptNo");

      this.PaymentOrder.ReceiptNo = receiptNo;
    }


    internal async Task Synchronize() {
      await this.PaymentOrder.Synchronize(_externalServices);
    }

    #endregion Methods

  }  // internal class PaymentOrderHandler

}  // namespace Empiria.OnePoint.EFiling
