using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Backend.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateEncodedToken(ClaimsIdentity identity);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id, List<string> roles);
    }
}
