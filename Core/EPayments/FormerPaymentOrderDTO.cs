﻿/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Payment Services                Component : Integration Layer                       *
*  Assembly : Empiria.OnePoint.Integration.dll           Pattern   : Adapter Interface                       *
*  Type     : FormerPaymentOrderDTO                      License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Holds payment order data used to interact with external systems (former version).              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using Empiria.Json;

namespace Empiria.OnePoint.EPayments {

  /// <summary>Holds payment order data used to interact with external systems (former version).</summary>
  public class FormerPaymentOrderDTO {

    #region Constructors and parsers

    protected FormerPaymentOrderDTO() {
      // Public instance creation not allowed. Instances must be created using a derived class.
    }

    public FormerPaymentOrderDTO(string routeNumber, DateTime dueDate, string controlTag) {
      Assertion.Require(routeNumber, "routeNumber");
      Assertion.Require(dueDate < DateTime.Today.AddYears(1),
                       "dueDate must be within one year starting today.");
      Assertion.Require(controlTag, "controlTag");

      this.RouteNumber = routeNumber;
      this.DueDate = dueDate;
      this.ControlTag = controlTag;
    }


    static public FormerPaymentOrderDTO Empty {
      get;
    } = new FormerPaymentOrderDTO {
      IsEmptyInstance = true
    };


    static public FormerPaymentOrderDTO Parse(JsonObject jsonObject) {
      if (jsonObject.IsEmptyInstance) {
        return FormerPaymentOrderDTO.Empty;
      }

      var paymentOrder = new FormerPaymentOrderDTO();

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
    }


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
    }


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
      set;
    }


    #endregion Public properties

    #region Methods


    public virtual void AssertIsValid() {

    }


    public virtual void SetPaymentData(DateTime paymentDate,
                                       decimal paymentTotal,
                                       string paymentReference) {
      Assertion.Require(paymentDate <= DateTime.Now, "Invalid payment date.");
      Assertion.Require(paymentTotal >= decimal.Zero, "Payment total must have a no negative value.");
      Assertion.Require(paymentReference != null, "Payment reference can't be null.");

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

  }  // class FormerPaymentOrderDTO

}  // namespace Empiria.OnePoint.EPayments
