using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MicroServiceProject.ProductService.App.Validators
{
    public interface ICheckProductInOrders
    {
        Task<bool> IsProductInOrders(int productId);
    }
    public class CheckProductInOrders : ICheckProductInOrders
    {
        private readonly HttpClient _httpClient;
        private const string OrderServiceBaseUrl = "http://localhost:5003/api/order"; // OrderService'in taban URL'si

        public CheckProductInOrders(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> IsProductInOrders(int productId)
        {
            var response = await _httpClient.GetAsync($"{OrderServiceBaseUrl}/product/{productId}/exists");
            response.EnsureSuccessStatusCode(); // Hata durumlarında exception fırlatır

            var isInOrders = await response.Content.ReadAsStringAsync();
            return bool.Parse(isInOrders); // true veya false döner
        }
    }
}