namespace MicroServiceProject.OrderService.App.Validators
{
    public interface IProductValidationService
    {
        Task<bool> ValidateProductExists(int productId);
    }
    public class ProductValidationService : IProductValidationService
    {
        private readonly HttpClient _httpClient;
        public ProductValidationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<bool> ValidateProductExists(int productId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5001/api/product/{productId}");
            return response.IsSuccessStatusCode; // Ürün varsa 200 OK döner
        }
    }
}
