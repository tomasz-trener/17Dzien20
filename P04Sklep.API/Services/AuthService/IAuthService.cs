using P05Sklep.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04Sklep.API.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceReponse<string>> Login(string email, string passwrod);

        Task<ServiceReponse<int>> Register(string email, string passwrod);
    }
}
