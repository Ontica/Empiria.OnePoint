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

    public string uid {
      get;
      internal set;
    }

    public string procedureType {
      get;
      internal set;
    }

    public Requester requestedBy {
      get;
      internal set;
    }

    public Preparer preparer {
      get;
      internal set;
    }

    public string summary {
      get;
      internal set;
    }

    public DateTime lastUpdateTime {
      get;
      internal set;
    }

    public NamedStatus status {
      get;
      internal set;
    }

    public ApplicationFormDTO form {
      get;
      internal set;
    }


    public PaymentOrderDTO paymentOrder {
      get;
      internal set;
    }


    public ESignDataDTO esign {
      get;
      internal set;
    }


    public TransactionDataDTO transaction {
      get;
      internal set;
    }


    public FixedList<EFilingDocumentDTO> inputDocuments {
      get;
      internal set;
    } = new FixedList<EFilingDocumentDTO>();


    public FixedList<EFilingDocumentDTO> outputDocuments {
      get;
      internal set;
    } = new FixedList<EFilingDocumentDTO>();


    public PermissionsDTO permissions {
      get;
      internal set;
    }

  }  // class EFilingRequestDTO



  /// <summary>Electronic filing transaction DTO.</summary>
  public class TransactionDataDTO {

    public string uid {
      get;
      internal set;
    }

    public string status {
      get;
      internal set;
    }

    public DateTime presentationDate {
      get;
      internal set;
    }

  } // class TransactionDataDTO



  /// <summary>Electronic sign data for filing requests.</summary>
  public class ESignDataDTO {

    public string hash {
      get;
      internal set;
    }

    public string seal {
      get;
      internal set;
    }

    public string sign {
      get;
      internal set;
    }

  } // class TransactionDataDTO



  /// <summary>Payment order data for filing requests.</summary>
  public class PaymentOrderDTO {

    public string urlPath {
      get;
      internal set;
    }

    public string routeNumber {
      get;
      internal set;
    }

    public DateTime dueDate {
      get;
      internal set;
    }

    public decimal total {
      get;
      internal set;
    }

    public string receiptNo {
      get;
      internal set;
    }

  } // class PaymentOrderDTO



  /// <summary>Application form associated with a filing request.</summary>
  public class ApplicationFormDTO {

    public string uid {
      get;
      internal set;
    }

    public string type {
      get;
      internal set;
    }

    public string typeName {
      get;
      internal set;
    }

    public string filledOutBy {
      get;
      internal set;
    }

    public DateTime filledOutTime {
      get;
      internal set;
    }

    public dynamic fields {
      get;
      internal set;
    } = new object();

  } // class ApplicationFormDTO



  /// <summary>The preparer of a filing request.</summary>
  public class Preparer {

    public string agency {
      get;
      internal set;
    }

    public string agent {
      get;
      internal set;
    }

  } // class Preparer



  /// <summary>Serves to send status type and name.</summary>
  public class NamedStatus {

    public string type {
      get;
      internal set;
    }

    public string name {
      get;
      internal set;
    }

  }  // class NamedStatus



  /// <summary>User permissions over a filing request.</summary>
  public class PermissionsDTO {

    public bool canManage {
      get;
      internal set;
    }

    public bool canRegister {
      get;
      internal set;
    }

    public bool canSendToSign {
      get;
      internal set;
    }

    public bool canSign {
      get;
      internal set;
    }

  }  // class PermissionsDTO


} // namespace Empiria.OnePoint.EFiling
