using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; internal set; }
    }
}
