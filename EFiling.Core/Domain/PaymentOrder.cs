/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Information Holder                      *
*  Type     : PaymentOrder                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains data about a payment order and its payment receipt.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

using Empiria.Json;
using Empiria.OnePoint.EPayments;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Contains data about a payment order and its payment receipt.</summary>
  public class PaymentOrder {

    #region Constructors and parsers

    internal PaymentOrder() {

    }

    internal PaymentOrder(PaymentOrderDTO dto) {
      this.LoadFromDTO(dto);
    }


    static internal PaymentOrder Parse(JsonObject json) {
      var po = new PaymentOrder();

      po.RouteNumber = json.Get("paymentOrder/routeNumber", po.RouteNumber);
      po.IssueTime = json.Get("paymentOrder/issueTime", po.IssueTime);
      po.DueDate = json.Get("paymentOrder/dueDate", po.DueDate);
      po.Total = json.Get("paymentOrder/total", po.Total);
      po.Uri = json.Get("paymentOrder/uri", po.Uri);

      po.ReceiptNo = json.Get("payment/receiptNo", po.ReceiptNo);

      return po;
    }


    static internal PaymentOrder Empty {
      get {
        return new PaymentOrder() { IsEmptyInstance = true };
      }
    }

    #endregion Constructors and parsers


    #region Properties

    public bool IsEmptyInstance {
      get;
      private set;
    }


    public string RouteNumber {
      get;
      private set;
    } = String.Empty;


    public DateTime IssueTime {
      get;
      private set;
    } = ExecutionServer.DateMinValue;


    public DateTime DueDate {
      get;
      private set;
    } = ExecutionServer.DateMinValue;


    public decimal Total {
      get;
      private set;
    }


    public string Uri {
      get;
      private set;
    } = String.Empty;


    public string ReceiptNo {
      get;
      internal set;
    } = String.Empty;


    #endregion Properties


    #region Methods

    internal async Task Synchronize(EFilingExternalServicesInteractor handler) {
      var paymentOrderDTO = await handler.TryGetPaymentOrder();

      if (paymentOrderDTO != null) {
        this.LoadFromDTO(paymentOrderDTO);
      }
    }


    public JsonObject ToJson() {
      if (this.IsEmptyInstance) {
        return JsonObject.Empty;
      }

      var json = new JsonObject();

      json.SetIfValue("paymentOrder/routeNumber", this.RouteNumber);
      json.SetIf("paymentOrder/issueTime", this.IssueTime, this.DueDate != ExecutionServer.DateMinValue);
      json.SetIf("paymentOrder/dueDate", this.DueDate, this.DueDate != ExecutionServer.DateMinValue);
      json.SetIf("paymentOrder/total", this.Total, this.Total != 0);
      json.SetIfValue("paymentOrder/uri", this.Uri);

      json.SetIfValue("payment/receiptNo", this.ReceiptNo);

      return json;
    }


    private void LoadFromDTO(PaymentOrderDTO dto) {
      this.RouteNumber = dto.RouteNumber;
      this.IssueTime = dto.IssueTime;
      this.DueDate = dto.DueDate;
      this.Total = dto.PaymentTotal;
    }


    #endregion Methods

  }  // class EFilingPaymentOrder

}  // namespace Empiria.OnePoint.EFiling
