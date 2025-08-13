using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Materias.Dtos.SaveDtos
{
    public class MateriaCursoSaveDto
    {
        public MateriaSaveDto? MateriaSaveDto { get; set; }
        public IList<CursoSaveDto>? CursoSaveDtoList { get; set; }
    }
}
