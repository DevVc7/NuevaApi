using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Usuarios.Dto
{
    public class LoginDto
    {
        public UserDto? User { get; set; }
        public Token? Tokens { get; set; }
    }

    public class Token
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        

    }
}
