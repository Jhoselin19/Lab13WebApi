using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab13WebApi.Models;
using Lab13WebApi.Request;

namespace Lab13WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly DemoContext _context;

        public InvoiceController(DemoContext context)
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


        // POST: api/Invoice/FilterInvoices
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Invoice>>> FilterInvoices([FromBody] InvoiceRequest request)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }

            var invoices = await _context.Invoices
                .Include(i => i.Customer)
                .Where(i => string.IsNullOrEmpty(request.FirstName) || i.Customer.FirstName.Contains(request.FirstName))
                .OrderByDescending(i => i.Customer.FirstName)
                .ToListAsync();

            return invoices;
        }

        // POST: api/Invoice/GetInvoicesByNumber
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByNumber([FromBody] InoviceRequestV3 request)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }

            var invoices = await _context.Invoices
                .Include(i => i.Customer)
                .Where(i => i.InvoiceNumber == request.InvoiceNumber)
                .OrderBy(i => i.Customer.FirstName)
                .ThenBy(i => i.InvoiceNumber)
                .ToListAsync();

            return Ok(invoices);
        }

        // POST: api/Invoice/GetInvoicesByDateRange
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByDateRange([FromBody] InvoiceRequestV4 request)
        {
            if (_context.Invoices == null)
            {
                return NotFound();
            }

            var invoices = await _context.Invoices
                .Include(i => i.Customer)
                .Where(i => i.Date >= request.Date)
                .OrderBy(i => i.Date)
                .ThenBy(i => i.InvoiceNumber)
                .ToListAsync();

            return Ok(invoices);
        }

    }
}
