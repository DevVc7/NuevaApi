using Application.Usuarios.Dto;

namespace Application.Studens.Dtos.Students
{
    public class StudentsSaveDto
    {
        public string? Grade { get; set; }
        public int Progress { get; set; }
        public string? LastActive { get; set; }
        public string? Code { get; set; }
        public string? Note { get; set; }
        public UserSaveDto? User { get; set; }
    }
}
