namespace Lab13WebApi.Request
{
    public class ProductUpdateRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }
}
