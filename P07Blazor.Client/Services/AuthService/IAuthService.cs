using P05Sklep.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P07Blazor.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceReponse<string>> Login(UserLogin user);
    }
}
