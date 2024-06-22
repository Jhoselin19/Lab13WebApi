using Lab13WebApi.Models;

namespace Lab13WebApi.Request
{
    public class InvoiceRequest
    {
        public float Total { get; set; }
        public string InvoiceNumber { get; set; }
        public int CustomerId { get; set; }
    }
}
