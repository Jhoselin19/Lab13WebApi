namespace Lab13WebApi.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }

        public Nullable<bool> Activo { get; set; } = true;
    }
}
