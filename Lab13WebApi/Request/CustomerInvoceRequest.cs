using Lab13WebApi.Models;

namespace Lab13WebApi.Request
{
    public class CustomerInvoceRequest
    {
        public int CustomerId { get; set; }
        public List<InvoiceListRequest> Invoices { get; set; }
    }
}
