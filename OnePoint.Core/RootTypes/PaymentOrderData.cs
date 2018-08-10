﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Solution : Empiria OnePoint                             System  : Core Domain                             *
*  Assembly : Empiria.OnePoint.dll                         Pattern : Information Holder                      *
*  Type     : PaymentOrderData                             License : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds payment order data that serves as a DTO used to interact with a ITreasuryConnector.      *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.OnePoint {

  /// <summary>Holds payment order data that serves as a Data Transfer Object (DTO)
  /// used interact with a ITreasuryConnector.</summary>
  public class PaymentOrderData : IPaymentOrderData {

    #region Constructors and parsers

    protected PaymentOrderData() {
      // Public instance creation not allowed. Instances must be created using a derived class.
    }

    public PaymentOrderData(string routeNumber, DateTime dueDate, string controlTag) {
      Assertion.AssertObject(routeNumber, "routeNumber");
      Assertion.Assert(dueDate < DateTime.Today.AddYears(1),
                       "dueDate must be within one year starting today.");
      Assertion.AssertObject(controlTag, "controlTag");

      this.RouteNumber = routeNumber;
      this.DueDate = dueDate;
      this.ControlTag = controlTag;
    }


    static public PaymentOrderData Empty {
      get;
    } = new PaymentOrderData() {
      IsEmptyInstance = true
    };


    static public PaymentOrderData Parse(JsonObject jsonObject) {
      if (jsonObject.IsEmptyInstance) {
        return PaymentOrderData.Empty;
      }

      var paymentOrder = new PaymentOrderData();

      paymentOrder.RouteNumber = jsonObject.Get<string>("RouteNumber");
      paymentOrder.IssueTime = jsonObject.Get<DateTime>("IssueTime");
      paymentOrder.DueDate = jsonObject.Get<DateTime>("DueDate");
      paymentOrder.ControlTag = jsonObject.Get<string>("ControlTag");

      paymentOrder.IsCompleted = jsonObject.Get<bool>("IsCompleted", false);
      if (paymentOrder.IsCompleted) {
        paymentOrder.PaymentDate = jsonObject.Get<DateTime>("PaymentDate");
        paymentOrder.PaymentReference = jsonObject.Get<string>("PaymentRef");
        paymentOrder.PaymentTotal = jsonObject.Get<decimal>("PaymentTotal");
      }

      return paymentOrder;
    }

    #endregion Constructors and parsers

    #region Public properties

    public bool IsEmptyInstance {
      get;
      private set;
    } = false;


    /// <summary>Línea de captura.</summary>
    public string RouteNumber {
      get;
      private set;
    } = String.Empty;


    /// <summary>Fecha de emisión de la línea de captura.</summary>
    public DateTime IssueTime {
      get;
      private set;
    } = DateTime.Now;


    /// <summary>Fecha de vigencia de la línea de captura.</summary>
    public DateTime DueDate {
      get;
      private set;
    } = ExecutionServer.DateMaxValue;


    /// <summary>Token para poder verificar el estado del pago.</summary>
    public string ControlTag {
      get;
      private set;
    } = String.Empty;


    /// <summary>True si el pago fue efectuado y verificado.</summary>
    public bool IsCompleted {
      get;
      private set;
    } = false;

    /// <summary>Fecha en que se efectuó el pago.</summary>
    public DateTime PaymentDate {
      get;
      private set;
    } = ExecutionServer.DateMaxValue;


    /// <summary>Folio de referencia del pago.</summary>
    public string PaymentReference {
      get;
      private set;
    } = String.Empty;


    /// <summary>Importe total del pago.</summary>
    public decimal PaymentTotal {
      get;
      private set;
    } = decimal.Zero;


    #endregion Public properties

    #region Methods

    public virtual void AssertIsValid() {

    }

    public virtual void SetPaymentData(DateTime paymentDate,
                                       decimal paymentTotal,
                                       string paymentReference) {
      Assertion.Assert(paymentDate <= DateTime.Now, "Invalid payment date.");
      Assertion.Assert(paymentTotal >= decimal.Zero, "Payment total must have a no negative value.");
      Assertion.Assert(paymentReference != null, "Payment reference can't be null.");

      this.PaymentDate = paymentDate;
      this.PaymentTotal = paymentTotal;
      this.PaymentReference = paymentReference;

      this.IsCompleted = true;
    }

    public virtual JsonObject ToJson() {
      var json = new JsonObject();

      json.Add("RouteNumber", this.RouteNumber);
      json.Add("IssueTime", this.IssueTime);
      json.Add("DueDate", this.DueDate);
      json.Add("ControlTag", this.ControlTag);

      if (this.IsCompleted) {
        json.Add("IsCompleted", this.IsCompleted);
        json.Add("PaymentDate", this.PaymentDate);
        json.Add("PaymentRef", this.PaymentReference);
        json.Add("PaymentTotal", this.PaymentTotal);
      }
      return json;
    }

    public override string ToString() {
      return this.ToJson().ToString();
    }

    #endregion Methods

  }  // class PaymentOrderData

}  // namespace Empiria.OnePoint
