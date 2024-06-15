namespace Lab13WebApi.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public Nullable<bool> Activo { get; set; } = true;
    }
}
