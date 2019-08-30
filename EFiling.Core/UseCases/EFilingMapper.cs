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

      if (request.ElectronicSign.Length != 0) {
        r.esign = new ESignDataDTO {
          hash = request.UID.ToString().Substring(0, 8).ToUpperInvariant(),
          seal = request.GetElectronicSeal(),
          sign = request.ElectronicSign
        };
      }

      if (request.TransactionUID.Length != 0) {
        var t = request.GetTransaction();

        var p = request.GetPaymentOrder();

        r.paymentOrder = new PaymentOrderDTO {
          urlPath = $"land.registration.system.transactions/bank.payment.order.aspx?id={t.Id}",
          routeNumber = p.RouteNumber,
          receiptNo = request.GetPaymentReceipt(),
          dueDate = p.DueDate,
          total = p.PaymentTotal
        };

        r.transaction = new TransactionDataDTO {
          uid = t.UID,
          status = t.StatusName,
          presentationDate = t.PresentationTime
        };

      }

      return r;
    }

  }  // class EFilingMapper

}  // namespace Empiria.OnePoint.ESign
