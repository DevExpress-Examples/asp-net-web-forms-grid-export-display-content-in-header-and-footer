using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Linq;

[DataObject(true)]
public class InvoiceDataContext {
    private List<Invoice> Invoices {
        get {
            if (HttpContext.Current.Session["Invoices"] == null) {
                List<Invoice> list = new List<Invoice>();

                list.Add(new Invoice(0, "Invoice1", 10.0m));
                list.Add(new Invoice(1, "Invoice2", 15.0m));
                list.Add(new Invoice(2, "Invoice3", 20.0m));
                HttpContext.Current.Session["Invoices"] = list;
                return list;
            } else
                return (List<Invoice>)HttpContext.Current.Session["Invoices"];
        }
        set {
            HttpContext.Current.Session["Invoices"] = value;
        }
    }

    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public List<Invoice> GetInvoices() {
        return Invoices;
    }
}