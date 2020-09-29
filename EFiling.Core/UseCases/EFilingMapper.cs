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

namespace Empiria.OnePoint.EFiling {

  /// <summary>Data mapping methods between electronic filing entities and their data transfer objects.</summary>
  static internal class EFilingMapper {


    static internal FixedList<EFilingRequestDTO> Map(FixedList<EFilingRequest> source) {
      return new FixedList<EFilingRequestDTO>(source.Select((x) => Map(x)));
    }


    static internal EFilingRequestDTO Map(EFilingRequest request) {
      var r = new EFilingRequestDTO {
        UID = request.UID,
        ProcedureType = request.Procedure.NamedKey,
        RequestedBy = request.RequestedBy,
        Preparer = new Preparer {
          Agency = request.Agency.Alias,
          Agent = request.Agent.Alias,
        },
        Summary = request.Procedure.DisplayName,
        LastUpdateTime = request.LastUpdate,
        Status = new NamedStatus {
          Type = request.Status.ToString(),
          Name = request.StatusName,
        }
      };

      if (request.ApplicationForm.HasItems) {
        var appForm = new ApplicationFormDTO {
          UID = request.UID,
          Type = request.Procedure.NamedKey,
          TypeName = request.Procedure.DisplayName,
          FilledOutBy = request.PostedBy.Alias,
          FilledOutTime = request.LastUpdate,
          Fields = request.ApplicationForm.ToObject()
        };

        r.Form = appForm;
      }

      if (request.IsSigned) {
        r.Esign = new ESignDataDTO {
          Hash = request.GetSecurityHash(),
          Seal = request.GetElectronicSeal(),
          Sign = request.ElectronicSign
        };
      }

      if (request.HasTransaction) {
        var t = request.Transaction;

        Assertion.AssertObject(t, $"Can't retrive transaction with UID ${t.UID}.");

        r.Transaction = new TransactionDataDTO {
          UID = t.UID,
          Status = t.Status,
          PresentationDate = t.PresentationTime
        };

        if (request.HasPaymentOrder) {
          r.PaymentOrder = new PaymentDataDTO(request);
        }

        if (request.IsClosed) {
          r.OutputDocuments = request.Transaction.OutputDocuments;
        }
      }

      var userContext = EFilingUserContext.Current();

      r.Permissions = new PermissionsDTO {
        CanManage = userContext.IsManager,
        CanRegister = userContext.IsRegister,
        CanSendToSign = userContext.IsRegister && !userContext.IsSigner,
        CanSign = userContext.IsSigner
      };

      return r;
    }


  }  // class EFilingMapper

}  // namespace Empiria.OnePoint.ESign
