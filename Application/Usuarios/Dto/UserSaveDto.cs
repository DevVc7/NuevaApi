using Domain;

namespace Application.Usuarios.Dto
{
    public class UserSaveDto
    {
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Password { get; set; }
        public string? NroDocumento { get; set; }
    }
}
