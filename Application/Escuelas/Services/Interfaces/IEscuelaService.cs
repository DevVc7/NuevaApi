using Application.Core.Services.Interfaces;
using Application.Escuelas.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Escuelas.Services.Interfaces
{
    public interface IEscuelaService : ICurdCoreService<EscuelaDto, EscuelaSaveDto, int>
    {
    }
}
