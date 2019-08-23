/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Web Api interface                       *
*  Assembly : Empiria.OnePoint.EFiling.WebApi.dll        Pattern   : Response methods                        *
*  Type     : EFilingRequestModels                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Response models for electronic application filing requests.                                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections;
using System.Collections.Generic;

namespace Empiria.OnePoint.EFiling.WebApi {

  /// <summary>Response models for electronic application filing requests.</summary>
  static internal class EFilingRequestModels {

    #region Response models

    static internal ICollection ToResponse(this IList<EFilingRequest> list) {
      ArrayList array = new ArrayList(list.Count);

      foreach (var item in list) {
        array.Add(item.ToResponse());
      }
      return array;
    }


    static internal object ToResponse(this EFilingRequest request) {
      return new {
        uid = request.UID,
        name = request.GetEmpiriaType().DisplayName,
        type = request.GetEmpiriaType().Name,
        typeName = request.GetEmpiriaType().DisplayName,
        requestedBy = request.RequestedBy,
        lastUpdateTime = request.LastUpdateTime,
        postingTime = request.PostingTime,
        status = request.Status,
        statusName = request.StatusName,

        signed = request.ElectronicSign != String.Empty,
        submitted = request.Status == EFilingRequestStatus.Submitted,

        esign = request.ToESignResponse(),

        transaction = ToTransactionResponse(request)
      };
    }


    static private object ToESignResponse(this EFilingRequest filingRequest) {
      if (filingRequest.ElectronicSign == String.Empty) {
        return new { };
      }

      return new {
        hash = filingRequest.UID.ToString().Substring(0, 8).ToUpperInvariant(),
        seal = filingRequest.GetElectronicSeal(),
        sign = filingRequest.ElectronicSign
      };
     }


    static private object ToTransactionResponse(this EFilingRequest filingRequest) {
        return new {
          id = 0,
          uid = "",
          status = "",
          sendTo = "",
          rfc = "",
          receiptNo = "",
          total = 0m,
          presentationDate = "",
        };

//      if (filingRequest.TransactionUID.Length == 0) {
//        return new {
//          id = 0,
//          uid = "",
//          status = "",
//          sendTo = "",
//          rfc = "",
//          receiptNo = "",
//          total = 0m,
//          presentationDate = "",
//        };
//      }


      //var transaction = LRSTransaction.TryParse(filingRequest.TransactionUID, true);

      //return new {
      //  id = transaction.Id,
      //  uid = transaction.UID,
      //  status = transaction.Workflow.CurrentStatus,
      //  sendTo = transaction.ExtensionData.SendTo.Address,
      //  rfc = transaction.ExtensionData.RFC,
      //  receiptNo = transaction.Payments.ReceiptNumbers,
      //  total = transaction.Items.TotalFee.Total,
      //  presentationDate = transaction.PresentationTime
      //};
    }

    #endregion Response models

  }  // class EFilingRequestModels

}  // namespace Empiria.OnePoint.EFiling.WebApi
