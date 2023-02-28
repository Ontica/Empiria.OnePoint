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
      get; set;
    }


    [DataField("TrackId")]
    public int TrackId {
      get; set;
    }


    [DataField("TransactionUID")]
    public string TransactionUID {
      get; set;
    }


    [DataField("RequestedBy")]
    public string RequestedBy {
      get; set;
    }


    [DataField("NextTransactionStatus")]
    public string NextTransactionStatus {
      get; set;
    }



    #endregion Properties

  }
}
