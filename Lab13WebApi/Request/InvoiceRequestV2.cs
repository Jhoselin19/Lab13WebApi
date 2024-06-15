using Lab13WebApi.Models;

namespace Lab13WebApi.Request
{
    public class InvoiceRequestV2
    {
        public string InvoiceNumber { get; set; }
        public float Total { get; set; }
        public int CustomerId { get; set; }

    }
}
