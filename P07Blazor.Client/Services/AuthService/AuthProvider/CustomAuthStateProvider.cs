using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using P05Sklep.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace P07Blazor.Client.Services.AuthService.AuthProvider
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {

        private readonly ILocalStorageService localStorageService;
        private readonly HttpClient httpClient;

        public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient httpClient)
        {
            this.localStorageService = localStorageService;
            this.httpClient = httpClient;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string authToken = await localStorageService.GetItemAsStringAsync("authToken");
            // z local storage pobieramy token 

            var identity = new ClaimsIdentity(); // przechowujemy dane uzytkownika 
            httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(authToken)) // spradzamy czy nie jest pusty 
            {
                // próbujemy go sparsować 
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                    httpClient.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
                }
                catch (Exception)
                {
                    await localStorageService.RemoveItemAsync("authToken"); // usuwamy token z localstorage 
                    identity = new ClaimsIdentity(); // czyscimy obiekt identity 

                }
            }

            var user = new ClaimsPrincipal(identity); // tworzmy uzytkownika na podstawie claims 
            var state = new AuthenticationState(user); // obiekt, który powiadamia apliacje o zmianie stanu autoryzacji 
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1]; // pobranie drugiej czesci tokena jwt 
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
