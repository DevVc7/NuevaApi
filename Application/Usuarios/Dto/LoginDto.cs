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
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public UserDto? User { get; set; }
    }

}
