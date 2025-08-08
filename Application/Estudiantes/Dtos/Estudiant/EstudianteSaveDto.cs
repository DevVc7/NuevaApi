using Application.Usuarios.Dto;
using Domain;

namespace Application.Estudiant.Dtos.Students
{
    public class EstudianteSaveDto
    {
        public string? CodEstudiante { get; set; }
        public string? Descripcion { get; set; }
        public int IdGrado { get; set; }
        public UserSaveDto? UsuarioSave { get; set; }
    }
}
