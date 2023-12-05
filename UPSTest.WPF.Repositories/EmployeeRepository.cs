using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UPSTest.WPF.Repositories.Models;


namespace UPSTest.WPF.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HttpClient httpClient;
        private const string ApiUrl = "https://gorest.co.in/public/v2/users";
        public EmployeeRepository(string apiToken)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiToken}");
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            string url = ApiUrl;

            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                List<Employee> result = JsonConvert.DeserializeObject<List<Employee>>(content);
                return result;
            }

            return null;
        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            string url = ApiUrl;

            string json = JsonConvert.SerializeObject(employee);
            Console.WriteLine($"Request Payload: {json}");

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Employee>(result);
            }
            else
            {
                string errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error Content: {errorContent}");
            }

            return null;
        }

        public async Task<Employee> UpdateEmployeeAsync(int id, Employee employee)
        {
            string url = $"{ApiUrl}/{id}";

            string json = JsonConvert.SerializeObject(employee);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Employee>(result);
            }

            return null;
        }

        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            string url = $"{ApiUrl}/{id}";

            HttpResponseMessage response = await httpClient.DeleteAsync(url);

            return response.IsSuccessStatusCode;
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            string url = $"{ApiUrl}/{id}";

            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Employee>(content);
            }

            return null;
        }

        public async Task<List<Employee>> SearchEmployeesAsync(string searchName)
        {
            string url = $"{ApiUrl}?name={searchName}";

            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string result = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Employee>>(result);
            }

            return null;
        }
    }

}
