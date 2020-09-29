/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases                               *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Output Data Transfer Objects            *
*  Type     : EFiling Ouput DTOs                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains output data transfer objects for electronic sign use cases.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Electronic filing request DTO.</summary>
  public class EFilingRequestDTO {

    public string UID {
      get;
      internal set;
    }

    public string ProcedureType {
      get;
      internal set;
    }

    public Requester RequestedBy {
      get;
      internal set;
    }

    public Preparer Preparer {
      get;
      internal set;
    }

    public string Summary {
      get;
      internal set;
    }

    public DateTime LastUpdateTime {
      get;
      internal set;
    }

    public NamedStatus Status {
      get;
      internal set;
    }

    public ApplicationFormDTO Form {
      get;
      internal set;
    }


    public PaymentDataDTO PaymentOrder {
      get;
      internal set;
    }


    public ESignDataDTO Esign {
      get;
      internal set;
    }


    public TransactionDataDTO Transaction {
      get;
      internal set;
    }


    public FixedList<EFilingDocument> InputDocuments {
      get;
      internal set;
    } = new FixedList<EFilingDocument>();


    public FixedList<EFilingDocument> OutputDocuments {
      get;
      internal set;
    } = new FixedList<EFilingDocument>();


    public PermissionsDTO Permissions {
      get;
      internal set;
    }

  }  // class EFilingRequestDTO



  /// <summary>Electronic filing transaction DTO.</summary>
  public class TransactionDataDTO {

    public string UID {
      get;
      internal set;
    }

    public string Status {
      get;
      internal set;
    }

    public DateTime PresentationDate {
      get;
      internal set;
    }

  } // class TransactionDataDTO



  /// <summary>Electronic sign data for filing requests.</summary>
  public class ESignDataDTO {

    public string Hash {
      get;
      internal set;
    }

    public string Seal {
      get;
      internal set;
    }

    public string Sign {
      get;
      internal set;
    }

  } // class TransactionDataDTO



  /// <summary>Payment order data for filing requests.</summary>
  public class PaymentDataDTO {

    public PaymentDataDTO(EFilingRequest request) {
      var paymentOrder = request.PaymentOrder;

      this.UrlPath = $"land.registration.system.transactions/bank.payment.order.aspx?" +
                     $"uid={request.Transaction.UID}&externalUID={request.UID}";

      this.RouteNumber = paymentOrder.RouteNumber;
      this.DueDate = paymentOrder.DueDate;
      this.Total = paymentOrder.Total;

      this.ReceiptNo = paymentOrder.ReceiptNo;
    }


    public string UrlPath {
      get;
      internal set;
    }


    public string RouteNumber {
      get;
      internal set;
    }


    public DateTime DueDate {
      get;
      internal set;
    }


    public decimal Total {
      get;
      internal set;
    }


    public string ReceiptNo {
      get;
      internal set;
    }


  } // class PaymentOrderDTO



  /// <summary>Application form associated with a filing request.</summary>
  public class ApplicationFormDTO {

    public string UID {
      get;
      internal set;
    }

    public string Type {
      get;
      internal set;
    }

    public string TypeName {
      get;
      internal set;
    }

    public string FilledOutBy {
      get;
      internal set;
    }

    public DateTime FilledOutTime {
      get;
      internal set;
    }

    public dynamic Fields {
      get;
      internal set;
    } = new object();

  } // class ApplicationFormDTO



  /// <summary>The preparer of a filing request.</summary>
  public class Preparer {

    public string Agency {
      get;
      internal set;
    }

    public string Agent {
      get;
      internal set;
    }

  } // class Preparer



  /// <summary>Serves to send status type and name.</summary>
  public class NamedStatus {

    public string Type {
      get;
      internal set;
    }

    public string Name {
      get;
      internal set;
    }

  }  // class NamedStatus



  /// <summary>User permissions over a filing request.</summary>
  public class PermissionsDTO {

    public bool CanManage {
      get;
      internal set;
    }

    public bool CanRegister {
      get;
      internal set;
    }

    public bool CanSendToSign {
      get;
      internal set;
    }

    public bool CanSign {
      get;
      internal set;
    }

  }  // class PermissionsDTO


} // namespace Empiria.OnePoint.EFiling
