/* Empiria OnePoint ******************************************************************************************
*                                                                                                            *
*  Module   : Electronic Filing Services                 Component : Domain Layer                            *
*  Assembly : Empiria.OnePoint.EFiling.dll               Pattern   : Information Holder                      *
*  Type     : EFilingTransaction                         License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Represents an electronic filing request typically submitted to a government agency.            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

using Empiria.Json;

namespace Empiria.OnePoint.EFiling {

  public class EFilingTransaction {

    #region Constructors and parsers

    private EFilingTransaction() {
      // Required by Empiria Framework.
    }


    internal EFilingTransaction(string uid) {
      this.UID = uid;
    }


    #endregion Constructors and parsers

    #region Public properties

    //[DataField("FilingRequestId")]
    public EFilingRequest Request {
      get;
      private set;
    }


    [DataField("TransactionUID")]
    public string UID {
      get;
      private set;
    }


    [DataField("TransactionExtData")]
    protected internal JsonObject ExtensionData {
      get;
      private set;
    } = new JsonObject();


    [DataField("TransactionDataLastUpdate")]
    public DateTime LastUpdate {
      get;
      private set;
    } = ExecutionServer.DateMinValue;


    [DataField("TransactionStatus")]
    public string Status {
      get;
      private set;
    }


    public DateTime PresentationTime {
      get {
        return this.ExtensionData.Get("transactionData/presentationTime", ExecutionServer.DateMaxValue);
      }
      private set {
        this.ExtensionData.SetIf("transactionData/presentationTime", value,
                                 value != ExecutionServer.DateMaxValue);
      }
    }


    public FixedList<EFilingDocument> OutputDocuments {
      get {
        return this.ExtensionData.GetList<EFilingDocument>("outputDocuments", false)
                                 .ToFixedList();
      }
      set {
        var list = value.ConvertAll(x => x.ToJson().ToObject());

        this.ExtensionData.SetIf("outputDocuments", list, list.Count > 0);
      }
    }


    #endregion Public properties


    #region Public methods


    internal async Task Synchronize(EFilingExternalServicesInteractor servicesHandler) {
      IFilingTransaction updatedData;

      if (servicesHandler.FilingRequest.Status == EFilingRequestStatus.OnPayment) {
        updatedData = await servicesHandler.UpdateExternalTransaction();
      } else {
        updatedData = await servicesHandler.GetTransaction();
      }

      this.Update(updatedData);

      var documents = await servicesHandler.GetOutputDocuments();

      this.OutputDocuments = documents;
    }


    private void Update(IFilingTransaction updatedData) {
      this.PresentationTime = updatedData.PresentationTime;
      this.Status = updatedData.StatusName;
      this.LastUpdate = DateTime.Now;
    }


    #endregion Public methods


  }  // class EFilingTransaction

}  // namespace Empiria.OnePoint.EFiling
