using Azure.Core;
using Lab13WebApi.Models;
using Lab13WebApi.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lab13WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductCustomController : ControllerBase
    {
        private readonly DemoContext _context;

        public ProductCustomController(DemoContext context)
        {
            _context = context;
        }

        // POST: api/Product
        [HttpPost]
        public void Insert([FromBody] ProductInsertRequest product)
        {
            Product model = new Product();
            model.Name = product.Name;
            model.Price = product.Price;

            _context.Products.Add(model);
            _context.SaveChanges();

        }

        [HttpDelete]
        public void Delete([FromBody] ProductDeleteRequest request)
        {
            var product = _context.Products.Find(request.ProductId);

            _context.Products.Remove(product);
            _context.SaveChanges();

        }

        [HttpPut]
        public void Update([FromBody] ProductUpdateRequest request)
        {
            var product = _context.Products.Find(request.ProductId);

            product.Price = request.Price;
            product.Name = request.Name;

            _context.SaveChanges();
        }

        [HttpDelete]
        public IActionResult DeleteProducts([FromBody] ProductListRequest request)
        {
            if (request == null || request.Products == null || !request.Products.Any())
            {
                return BadRequest("La solicitud no contiene productos válidos para eliminar.");
            }

            try
            {
                // Filtrar los productos a eliminar por nombre y precio
                var productsToDelete = new List<Product>();

                foreach (var productReq in request.Products)
                {
                    var product = _context.Products.FirstOrDefault(p => p.Name == productReq.Name && p.Price == productReq.Price);
                    if (product != null)
                    {
                        productsToDelete.Add(product);
                    }
                }

                if (productsToDelete.Any())
                {
                    _context.Products.RemoveRange(productsToDelete);
                    _context.SaveChanges();
                }

                return Ok(); // Devuelve 200 OK si se eliminaron los productos correctamente
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción y devolver un código de estado 500
                return StatusCode(500, $"Error al eliminar los productos: {ex.Message}");
            }
        }
    }
}