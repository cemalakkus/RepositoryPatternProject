namespace RepositoryPattern.Application.Dtos
{
    public class ProductUpdateRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Qantity { get; set; }
    }
}
