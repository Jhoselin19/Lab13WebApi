using Lab13WebApi.Models;

namespace Lab13WebApi.Request
{
    public class InvoiceDetailRequest
    {
        public int InvoiceId { get; set; }
        public List<DetailRequest> Details { get; set; }
    }
}
