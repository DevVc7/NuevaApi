using Application.Core.Services.Interfaces;
using Application.Studens.Dtos.Students;
using Domain;

namespace Application.Studens.Services.Interface
{
    public interface IStudentServices : ICurdCoreService<StudentsDto, StudentsSaveDto, Guid>
    {
        Task<StudentsMeta<Student>> BusquedaPaginado();
    }
}
