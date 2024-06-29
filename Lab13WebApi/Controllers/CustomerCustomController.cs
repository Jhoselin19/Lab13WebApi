using Lab13WebApi.Models;
using Lab13WebApi.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab13WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public CustomerCustomController(DemoContext context)
        {
            _context = context;
        }

        // POST: api/Customer
        [Authorize]
        [HttpPost]
        public IActionResult Insert([FromBody] CustomerInsertRequest customer)
        {
            Customer model = new Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DocumentNumber = customer.DocumentNumber
            };

            _context.Customers.Add(model);
            _context.SaveChanges();

            return Ok();
        }

        [Authorize]
        [HttpPost]
        public IActionResult DeleteCustomer([FromBody] CustomerDeleteRequest request)
        {
            var customer = _context.Customers.Find(request.CustomerId);
            if (customer == null)
            {
                return NotFound(new { message = "Customer not found" });
            }

            customer.Activo = false;
            _context.SaveChanges();

            return Ok(new { message = "Customer deactivated successfully" });
        }

        [Authorize]
        [HttpPut]
        public IActionResult Update([FromBody] CustomerUpdateRequest request)
        {
            var customer = _context.Customers.Find(request.CustomerId);
            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found");
            }

            customer.DocumentNumber = request.DocumentNumber;
            customer.Email = request.Email;

            _context.SaveChanges();

            return Ok();
        }
    }
}
