/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases                               *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Data Transfer Objects                   *
*  Type     : EFiling Output DTOs                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Contains output data transfer objects for electronic sign use cases.                           *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Storage.Documents;

namespace Empiria.OnePoint.EFiling.UseCases {

  public class EFilingRequestDto {

    private readonly EFilingRequest request;

    internal EFilingRequestDto(EFilingRequest request) {
      this.request = request;
    }


    public string UID {
      get {
        return this.request.UID;
      }
    }

    public string ProcedureType {
      get {
        return request.Procedure.NamedKey;
      }
    }

    public RequesterDto RequestedBy {
      get {
        return request.RequestedBy;
      }
    }

    public Preparer Preparer {
      get {
        return new Preparer {
          Agency = request.Agency.ShortName,
          Agent = request.Agent.ShortName,
        };
      }
    }

    public string Summary {
      get {
        return request.Procedure.DisplayName;
      }
    }

    public DateTime LastUpdateTime {
      get {
        return request.LastUpdate;
      }
    }

    public NamedStatus Status {
      get {
        return new NamedStatus {
          Type = request.Status.ToString(),
          Name = request.StatusName,
        };
      }
    }

    public ApplicationFormDto Form {
      get;
      internal set;
    }

    public PaymentDataDto PaymentOrder {
      get;
      internal set;
    }

    public ESignDataDto Esign {
      get;
      internal set;
    }

    public TransactionDataDto Transaction {
      get;
      internal set;
    }

    public DocumentationDto Documentation {
      get;
      internal set;
    }

    public FixedList<EFilingDocument> OutputDocuments {
      get;
      internal set;
    } = new FixedList<EFilingDocument>();


    public PermissionsDto Permissions {
      get {
        var userContext = EFilingUserContext.Current();

        return new PermissionsDto(userContext);
      }
    }

  }  // class EFilingRequestDTO


  /// <summary>E-filing request documentation DTO.</summary>
  public class DocumentationDto {

    private readonly Documentation _documentation;


    internal DocumentationDto(EFilingRequest request) {
      _documentation = request.Documentation;
    }


    public string OverallStatus {
      get {
        return _documentation.OverallStatus.ToString();
      }
    }


    public FixedList<DocumentFulfillmentDto> Fulfillment {
      get {
        var fulfillment = _documentation.GetFulfillment();

        List<DocumentFulfillmentDto> dtoList = new List<DocumentFulfillmentDto>(fulfillment.Count);

        foreach (var item in fulfillment) {
          var dto = new DocumentFulfillmentDto(item);

          dtoList.Add(dto);
        }

        return dtoList.ToFixedList();
      }
    }
  }


  public class DocumentFulfillmentDto {

    private readonly DocumentFulfillment _fulfillment;

    public DocumentFulfillmentDto(DocumentFulfillment fulfillment) {
      _fulfillment = fulfillment;
    }


    public string Status {
      get {
        return _fulfillment.Status.ToString();
      }
    }

  }



  /// <summary>Electronic filing transaction DTO.</summary>
  public class TransactionDataDto {

    private readonly EFilingTransaction transaction;

    internal TransactionDataDto(EFilingRequest request) {
      this.transaction = request.Transaction;
    }


    public string UID {
      get {
        return this.transaction.UID;
      }
    }

    public string Status {
      get {
        return this.transaction.Status;
      }
    }

    public DateTime PresentationDate {
      get {
        return this.transaction.PresentationTime;
      }
    }

  } // class TransactionDataDto



  /// <summary>Electronic sign data for filing requests.</summary>
  public class ESignDataDto {

    private readonly EFilingRequest request;

    internal ESignDataDto(EFilingRequest request) {
      this.request = request;
    }


    public string Hash {
      get {
        return request.SecurityData.GetSecurityHash();
      }
    }

    public string Seal {
      get {
        return request.SecurityData.GetElectronicSeal();
      }
    }

    public string Sign {
      get {
        return request.SecurityData.ElectronicSign;
      }
    }

  } // class ESignDataDto



  /// <summary>Payment order data for filing requests.</summary>
  public class PaymentDataDto {

    private readonly EFilingRequest request;

    internal PaymentDataDto(EFilingRequest request) {
      this.request = request;
    }


    public string UrlPath {
      get {
        return $"land.registration.system.transactions/bank.payment.order.aspx?" +
               $"uid={request.Transaction.UID}&externalUID={request.UID}";
      }
    }

    public string RouteNumber {
      get {
        return request.PaymentOrder.RouteNumber;
      }
    }

    public DateTime DueDate {
      get {
        return request.PaymentOrder.DueDate;
      }
    }

    public decimal Total {
      get {
        return request.PaymentOrder.Total;
      }
    }

    public string ReceiptNo {
      get {
        return request.PaymentOrder.ReceiptNo;
      }
    }

  } // class PaymentDataDto



  /// <summary>Application form associated with a filing request.</summary>
  public class ApplicationFormDto {

    private readonly EFilingRequest request;

    internal ApplicationFormDto(EFilingRequest request) {
      this.request = request;
    }

    public string UID {
      get {
        return request.UID;
      }
    }

    public string Type {
      get {
        return request.Procedure.NamedKey;
      }
    }

    public string TypeName {
      get {
        return request.Procedure.DisplayName;
      }
    }

    public string FilledOutBy {
      get {
        return request.PostedBy.ShortName;
      }
    }

    public DateTime FilledOutTime {
      get {
        return request.LastUpdate;
      }
    }

    public dynamic Fields {
      get {
        return request.ApplicationForm.ToObject();
      }
    }

  } // class ApplicationFormDto



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
  public class PermissionsDto {

    private readonly EFilingUserContext userContext;

    internal PermissionsDto(EFilingUserContext userContext) {
      this.userContext = userContext;
    }


    public bool CanManage {
      get {
        return userContext.IsManager;
      }
    }

    public bool CanRegister {
      get {
        return userContext.IsRegister;
      }
    }

    public bool CanSendToSign {
      get {
        return userContext.IsRegister && !userContext.IsSigner;
      }
    }

    public bool CanSign {
      get {
        return userContext.IsSigner;
      }
    }

  }  // class PermissionsDto


} // namespace Empiria.OnePoint.EFiling.UseCases
