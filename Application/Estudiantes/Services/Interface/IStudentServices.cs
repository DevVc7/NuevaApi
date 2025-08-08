using Application.Core.Services.Interfaces;
using Application.Estudiant.Dtos.Students;
using Domain;
using Domain.View;

namespace Application.Studens.Services.Interface
{
    public interface IStudentServices : ICurdCoreService<EstudianteDto, EstudianteSaveDto, int>
    {
        Task<StudentsMeta<Estudiante>> BusquedaPaginado();
    }
}
