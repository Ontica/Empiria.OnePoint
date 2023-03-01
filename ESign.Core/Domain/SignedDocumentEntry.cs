using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empiria.OnePoint.ESign {


  public class SignedDocumentEntry : BaseObject {

    #region Constructors and parsers

    protected SignedDocumentEntry() {
      // Required by Empiria Framework.
    }


    static public SignedDocumentEntry Parse(int id) {
      return BaseObject.ParseId<SignedDocumentEntry>(id);
    }


    static public SignedDocumentEntry Parse(string uid) {
      return BaseObject.ParseKey<SignedDocumentEntry>(uid);
    }


    #endregion Constructors and parsers

    #region Properties


    [DataField("TransactionId")]
    public int TransactionId {
      get; internal set;
    }


    [DataField("TransactionUID")]
    public string TransactionUID {
      get; internal set;
    }


    [DataField("DocumentType")]
    public string DocumentType {
      get; internal set;
    }


    [DataField("TransactionType")]
    public string TransactionType {
      get; internal set;
    }


    [DataField("InternalControlNo")]
    public string InternalControlNo {
      get; internal set;
    }


    [DataField("AssignedById")]
    public string AssignedById {
      get; internal set;
    }


    [DataField("AssignedBy")]
    public string AssignedBy {
      get; internal set;
    }


    [DataField("RequestedBy")]
    public string RequestedBy {
      get; internal set;
    }


    [DataField("TransactionStatus")]
    public string TransactionStatus {
      get; internal set;
    }


    [DataField("RecorderOfficeId")]
    public int RecorderOfficeId {
      get; internal set;
    }


    [DataField("PresentationTime")]
    public DateTime PresentationTime {
      get; internal set;
    }



    #endregion Properties

  }
}
