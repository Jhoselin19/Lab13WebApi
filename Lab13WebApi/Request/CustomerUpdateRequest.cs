using Microsoft.Extensions.Primitives;

namespace Lab13WebApi.Request
{
    public class CustomerUpdateRequest
    {
        public int CustomerId { get; set; }
        public string DocumentNumber { get; set; }
        public string Email {  get; set; }
    }
}
