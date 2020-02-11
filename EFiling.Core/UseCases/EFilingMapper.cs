/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Use cases                               *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Use Cases Data Mapper                   *
*  Type     : EFilingMapper                              License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Data mapping methods between electronic filing entities and their data transfer objects.       *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Linq;

namespace Empiria.OnePoint.EFiling {

  /// <summary>Data mapping methods between electronic filing entities and their data transfer objects.</summary>
  static internal class EFilingMapper {


    static internal FixedList<EFilingRequestDTO> Map(FixedList<EFilingRequest> source) {
      return new FixedList<EFilingRequestDTO>(source.Select((x) => Map(x)));
    }


    static internal EFilingRequestDTO Map(EFilingRequest request) {
      var r = new EFilingRequestDTO {
        uid = request.UID,
        procedureType = request.Procedure.NamedKey,
        requestedBy = request.RequestedBy,
        preparer = new Preparer {
          agency = request.Agency.Alias,
          agent = request.Agent.Alias,
        },
        summary = request.Procedure.DisplayName,
        lastUpdateTime = request.LastUpdateTime,
        status = new NamedStatus {
          type = request.Status.ToString(),
          name = request.StatusName,
        }
      };

      if (request.ApplicationForm.HasItems) {
        var appForm = new ApplicationFormDTO {
          uid = request.UID,
          type = request.Procedure.NamedKey,
          typeName = request.Procedure.DisplayName,
          filledOutBy = request.PostedBy.Alias,
          filledOutTime = request.LastUpdateTime,
          fields = request.ApplicationForm.ToObject()
        };

        r.form = appForm;
      }

      if (request.IsSigned) {
        r.esign = new ESignDataDTO {
          hash = request.GetSecurityHash(),
          seal = request.GetElectronicSeal(),
          sign = request.ElectronicSign
        };
      }

      if (request.HasTransaction) {
        var t = request.GetTransaction();

        Assertion.AssertObject(t, $"Can't retrive transaction with UID ${t.UID}.");

        var p = request.GetPaymentOrder();

        if (p != null) {
          r.paymentOrder = new PaymentOrderDTO {
            urlPath = $"land.registration.system.transactions/bank.payment.order.aspx?id={t.Id}",
            routeNumber = p.RouteNumber,
            receiptNo = request.GetPaymentReceipt(),
            dueDate = p.DueDate,
            total = p.PaymentTotal
          };
        }

        r.transaction = new TransactionDataDTO {
          uid = t.UID,
          status = t.StatusName,
          presentationDate = t.PresentationTime
        };

      }

      if (request.HasTransaction && request.IsClosed) {
        r.outputDocuments = GetOutputDocuments(request);
      }

      var userContext = EFilingUserContext.Current();

      r.permissions = new PermissionsDTO {
        canManage = userContext.IsManager,
        canRegister = userContext.IsRegister,
        canSendToSign = userContext.IsRegister && !userContext.IsSigner,
        canSign = userContext.IsSigner
      };

      return r;
    }


    private static FixedList<EFilingDocumentDTO> GetOutputDocuments(EFilingRequest filingRequest) {
      var provider = ExternalProviders.GetFilingTransactionProvider(filingRequest.Procedure);

      return provider.GetOutputDocuments(filingRequest.TransactionUID);
    }

  }  // class EFilingMapper

}  // namespace Empiria.OnePoint.ESign
