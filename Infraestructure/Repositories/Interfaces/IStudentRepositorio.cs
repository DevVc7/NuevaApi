﻿using Domain;
using Infraestructure.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories.Interfaces
{
    public interface IStudentRepositorio : ICurdCoreRespository<Student, Guid> 
    {
        Task<StudentsMeta<Student>> BusquedaPaginado();
    }
}
