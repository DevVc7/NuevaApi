using Application.Core.Services.Interfaces;
using Application.Materias.Dtos;
using Application.Materias.Dtos.SaveDtos;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Materias.Services.Interfaces
{
    public interface IMateriaServices :  ICurdCoreService<MateriaDto, MateriaCursoSaveDto, int>
    {
        Task<OperationResult<CursoDto>> SaveCursoLeccion(int id, CursoLeccionSaveDto saveDto);
        Task<IReadOnlyList<CursoDto>> FecthByIdMateria(int idMateria);
        Task<IReadOnlyList<LeccionDto>> FecthByIdCurso(int idCurso);

        Task<CursoDto> FecthByIdCu(int idCurso);




    }
}
