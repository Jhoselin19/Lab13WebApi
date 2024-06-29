using Microsoft.EntityFrameworkCore;

namespace Lab13WebApi.Models
{
    public class DemoContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Detail> Details { get; set; }
        //cadena de conexion
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=LAPTOP-19VR9K1O\\SQLEXPRESS;Initial Catalog=StoreBD; User id=user01; Pwd=123456;Trusted_Connection=True;TrustServerCertificate=True");

        }
    }
}
