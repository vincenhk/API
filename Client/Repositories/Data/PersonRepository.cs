using API.Models;
using API.ViewModel;
using API.ViewModels;
using BelajarCORS.Base;
using Client.Base;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class PersonRepository : GeneralRepository<Person, int>
    {
        private readonly Address address;
        private readonly HttpClient httpClient;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;

        public PersonRepository(Address address, string request = "Persons/") : base(address, request)
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

        public async Task<List<RegisterVM>> GetAllProfile()
        {
            /// isi codingan kalian disini
        }
    }
}
