namespace Lab13WebApi.Models
{
    public class Detail
    {
        public int DetailId { get; set; }
        public int Amount { get; set; }
        public float Price { get; set; }
        public float Subtotal { get; set; }

        //clave foranea de producto
        public Product? Product { get; set; }
        public int ProductId { get; set; }

        //clave foranea de facturas
        public Invoice? Invoice { get; set; }
        public int InvoiceId { get; set; }
    }
}
