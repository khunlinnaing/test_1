using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using frontend.Models;
using Newtonsoft.Json;
namespace frontend
{
    public class EmployeeController
    {
        private readonly HttpClient _httpClient;

        public EmployeeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginatedData<Customer>> GetCustomersAsync(int page)
        {
            var response = await _httpClient.GetAsync($"/api/employees?page={page}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<PaginatedData<Customer>>();
            return content;
        }

        public async Task<Customer> GetCustomerAsync(int id)
        {
            var response = await _httpClient.GetAsync($"/api/employees/{id}");
            var content = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<Customer>(content);
            return customer;
        }

        //  public async Task<HttpResponseMessage> CreateCustomerAsync(Customer customer)
        //  {
        //     var json = JsonConvert.SerializeObject(customer);
        //     var content = new StringContent(json, Encoding.UTF8, "application/json");
        //     await _httpClient.PostAsync("/api/employees", content);
        //  }

        public async Task<HttpResponseMessage> CreateCustomerAsync(Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/employees", content);
            return response;
        }

        public async Task<HttpResponseMessage> UpdateCustomerAsync(int id, Customer customer)
        {
            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response=await _httpClient.PutAsync($"/api/employees/{id}", content);
            return response;
        }

        public async Task DeleteCustomerAsync(int id)
        {
            await _httpClient.DeleteAsync($"/api/employees/{id}");
        }
    }

}