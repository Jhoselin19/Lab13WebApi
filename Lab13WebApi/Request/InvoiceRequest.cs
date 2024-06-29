using Lab13WebApi.Models;

namespace Lab13WebApi.Request
{
    public class InvoiceRequest
    {
        public string InvoiceNumber { get; set; }
        public float Total { get; set; }
        public DateTime? Date { get; set; }
    }
}
