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
            if (request == null || request.Details == null || !request.Details.Any())
            {
                return BadRequest("La solicitud no contiene detalles de factura válidos para insertar.");
            }

            try
            {
                var invoice = _context.Invoices.Find(request.InvoiceId);

                if (invoice == null)
                {
                    return NotFound($"Factura con Id {request.InvoiceId} no encontrada.");
                }

                foreach (var detail in request.Details)
                {
                    // Crear un nuevo detalle de factura y asociarlo a la factura
                    var newDetail = new Detail
                    {
                        InvoiceId = request.InvoiceId,
                        Price = detail.Price,
                        Amount = detail.Amount,
                        Subtotal = detail.Subtotal,
                        ProductId = detail.ProductId,

                    };

                    _context.Details.Add(newDetail);
                }

                _context.SaveChanges();

                return Ok(); 
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Error al insertar los detalles de factura: {ex.Message}");
            }
        }
    }
}