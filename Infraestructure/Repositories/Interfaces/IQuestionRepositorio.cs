using Domain;
using Domain.View;
using Infraestructure.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IQuestionRepositorio : ICurdCoreRespository<Question, Guid>
    {
        Task<QuestionDataResponse> BusquedaPaginado();
    }
}
