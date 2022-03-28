using API.Models;
using API.ViewModel;
using Client.Base;
using Client.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public EmployeeRepository(Address address, string request = "Employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.link)
            };
            //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }

        public HttpStatusCode Register(RegisterVM entity)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.link + "Accounts/Register", content).Result;
            return result.StatusCode;
        }

        public async Task<List<GetMasterData>> GetAllProfile()
        {
            List<GetMasterData> entities = new List<GetMasterData>();

            using (var response = await httpClient.GetAsync(address.link + request + "EmployeeMasterData/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<GetMasterData>>(apiResponse);
            }

            return entities;
        }

        public async Task<List<University>> GetAllUniversity()
        {
            List<University> entities = new List<University>();

            using (var response = await httpClient.GetAsync(address.link + "Universities/"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entities = JsonConvert.DeserializeObject<List<University>>(apiResponse);
            }

            return entities;
        }


        //public async Task<ChartVM> GetChartGender()
        //{
        //    ChartVM entities = null;

        //    using (var response = await httpClient.GetAsync(address.link + request + "GenderCount/"))
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        entities = JsonConvert.DeserializeObject<ChartVM>(apiResponse);
        //    }

        //    return entities;
        //}

        //public async Task<ChartVM> GetChartUniversity()
        //{
        //    ChartVM entities = null;

        //    using (var response = await httpClient.GetAsync(address.link + request + "UniversityCount/"))
        //    {
        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        entities = JsonConvert.DeserializeObject<ChartVM>(apiResponse);
        //    }

        //    return entities;
        //}
    }
}