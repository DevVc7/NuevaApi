using Domain;
using Infraestructure.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IRespuestaUsuarioRepositorio : ICurdCoreRespository<RespuestaUsuario, int>
    {
        Task<RespuestaUsuario?> FindByIdUsuarioAsync(int idUsuario, int idPregunta);
        Task<IReadOnlyList<RespuestaUsuario>> FindAllAsyncMateria(int id);

    }
}
