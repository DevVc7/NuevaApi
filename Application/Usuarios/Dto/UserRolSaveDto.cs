using Application.Roles.Dtos;
using Domain;

namespace Application.Usuarios.Dto
{
    public class UserRolSaveDto 
    {
        public RolSaveDto? Rol { get; set; }
        public UserSaveDto? User { get; set; }
    }
}
