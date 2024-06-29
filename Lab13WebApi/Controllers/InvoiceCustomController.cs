using Lab13WebApi.Models;
using Lab13WebApi.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public InvoiceCustomController(DemoContext context)
        {
            _context = context;
        }

        // POST: api/Invoices
        [HttpPost]
        public void Insert(InvoiceRequestV2 invoice)
        {
            Invoice model = new Invoice();
            model.InvoiceNumber = invoice.InvoiceNumber;
            model.Total = invoice.Total;
            model.CustomerId = invoice.CustomerId;
            model.Date = DateTime.Now;

            _context.Invoices.Add(model);
            _context.SaveChanges();

        }

        [HttpPost]
        public IActionResult InsertInvoiceDetail([FromBody] InvoiceDetailRequest request)
        {
            var invoice = _context.Invoices.Find(request.InvoiceId);
            if (invoice == null)
            {
                return NotFound(new { message = "Invoice not found" });
            }

            foreach (var detail in request.Details)
            {
                Detail model = new Detail
                {
                    Amount = detail.Amount,
                    Price = detail.Price,
                    Subtotal = detail.Subtotal,
                    ProductId = detail.ProductId,
                    InvoiceId = request.InvoiceId
                };
                _context.Details.Add(model);
            }

            _context.SaveChanges();

            return Ok(new { message = "Details added successfully" });
        }

        [HttpPost]
        public IActionResult InsertCustomerInvoices([FromBody] CustomerInvoceRequest request)
        {
            var customer = _context.Customers.Find(request.CustomerId);
            if (customer == null)
            {
                return NotFound(new { message = "Customer not found" });
            }

            foreach (var invoiceRequest in request.Invoices)
            {
                Invoice model = new Invoice
                {
                    InvoiceNumber = invoiceRequest.InvoiceNumber,
                    Total = invoiceRequest.Total,
                    Date = invoiceRequest.Date ?? DateTime.Now,
                    CustomerId = request.CustomerId
                };
                _context.Invoices.Add(model);
            }

            _context.SaveChanges();

            return Ok(new { message = "Invoices added successfully" });
        }
    }
}