using P05Sklep.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace P07Blazor.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;

        public AuthService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ServiceReponse<string>> Login(UserLogin user)
        {
            var result = await httpClient.PostAsJsonAsync("api/auth/login", user);
            return await result.Content.ReadFromJsonAsync<ServiceReponse<string>>();


        }
    }
}
