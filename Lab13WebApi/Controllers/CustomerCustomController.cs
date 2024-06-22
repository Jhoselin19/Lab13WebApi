using Lab13WebApi.Models;
using Lab13WebApi.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public CustomerCustomController(DemoContext context)
        {
            _context = context;
        }

        // POST: api/Customer
        [HttpPost]
        public void Insert([FromBody] CustomerInsertRequest customer)
        {
            Customer model = new Customer();
            model.FirstName = customer.FirstName;
            model.LastName = customer.LastName;
            model.DocumentNumber = customer.DocumentNumber;

            _context.Customers.Add(model);
            _context.SaveChanges();

        }

        [HttpDelete]
        public void Delete([FromBody] CustomerDeleteRequest request)
        {
            var customer = _context.Customers.Find(request.CustomerId);

            _context.Customers.Remove(customer);
            _context.SaveChanges();

        }

        [HttpPut]
        public void Update([FromBody] CustomerUpdateRequest request)
        {
            var customer = _context.Customers.Find(request.CustomerId);

            customer.DocumentNumber = request.DocumentNumber;
            customer.Email = request.Email;

            _context.SaveChanges();
        }

        [HttpPost("InsertInvoices")]
        public IActionResult InsertInvoices([FromBody] CustomerInvoceRequest request)
        {
            try
            {
                var customer = _context.Customers.Find(request.CustomerId);

                foreach (var invoice in request.Invoices)
                {
                    var newInvoice = new Invoice
                    {
                        CustomerId = request.CustomerId,
                        InvoiceNumber = invoice.InvoiceNumber,
                        Total = invoice.Total,
                        Date = invoice.Date
                    };

                    _context.Invoices.Add(newInvoice);
                }

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al insertar las facturas: {ex.Message}");
            }
        }

    }
}
