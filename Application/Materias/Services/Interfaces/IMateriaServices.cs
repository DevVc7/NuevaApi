using Application.Core.Services.Interfaces;
using Application.Materias.Dtos;
using Application.Materias.Dtos.SaveDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Materias.Services.Interfaces
{
    public interface IMateriaServices :  ICurdCoreService<MateriaDto, MateriaCursoSaveDto, int>
    {
    }
}
