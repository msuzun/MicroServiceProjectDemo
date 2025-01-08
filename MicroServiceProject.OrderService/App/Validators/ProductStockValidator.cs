namespace MicroServiceProject.OrderService.App.Validators
{
    public interface IProductStockValidator
    {
        Task<int> GetProductStock(int productId);
    }
    public class ProductStockValidator : IProductStockValidator
    {
        private readonly HttpClient _httpClient;
        private const string ProductServiceBaseUrl = "http://localhost:5001/api/product"; // ProductService taban URL'si
        public ProductStockValidator(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<int> GetProductStock(int productId)
        {
            var response = await _httpClient.GetAsync($"{ProductServiceBaseUrl}/{productId}/stock");
            response.EnsureSuccessStatusCode(); // Hata durumunda exception fırlatır

            var stock = await response.Content.ReadAsStringAsync();
            return int.Parse(stock); // Stok bilgisini döner
        }
    }
}
