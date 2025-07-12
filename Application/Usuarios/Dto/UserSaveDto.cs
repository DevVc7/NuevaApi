using Domain;

namespace Application.Usuarios.Dto
{
    public class UserSaveDto
    {
        public string? Email { get; set; } 
        public string? Name { get; set; }
        public string? Password { get; set; } 
        public string? Rol { get; set; }
    }
}
