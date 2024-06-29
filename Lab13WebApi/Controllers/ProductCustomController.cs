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

        [HttpPost]
        public IActionResult DeleteProduct([FromBody] ProductDeleteRequest request)
        {
            var product = _context.Products.Find(request.ProductId);
            if (product == null)
            {
                return NotFound(new { message = "Product not found" });
            }

            product.Activo = false;
            _context.SaveChanges();

            return Ok(new { message = "Product deactivated successfully" });
        }

        [HttpPut]
        public void Update([FromBody] ProductUpdateRequest request)
        {
            var product = _context.Products.Find(request.ProductId);

            product.Price = request.Price;
            product.Name = request.Name;

            _context.SaveChanges();
        }

        [HttpPost]
        public IActionResult DeleteProductsList([FromBody] DeleteListProductsRequest request)
        {
            // Verificar si la lista de IDs está vacía
            if (request.ProductId == null || !request.ProductId.Any())
            {
                return BadRequest(new { message = "No product IDs provided" });
            }

            // Buscar los productos que coincidan con los IDs proporcionados
            var products = _context.Products.Where(p => request.ProductId.Contains(p.ProductId)).ToList();

            // Verificar si se encontraron productos
            if (!products.Any())
            {
                return NotFound(new { message = "No products found for the given IDs" });
            }

            // Actualizar el campo Activo a false para cada producto encontrado
            foreach (var product in products)
            {
                product.Activo = false;
            }

            // Guardar los cambios en la base de datos
            _context.SaveChanges();

            return Ok(new { message = "Products deactivated successfully" });
        }
    }
}