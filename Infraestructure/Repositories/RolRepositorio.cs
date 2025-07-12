using Domain;
using Infraestructure.Contexts;
using Infraestructure.Core.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repositories
{
    public class RolRepositorio : CurdCoreRespository<Rol, int>, IRolRepositorio
    {
        private readonly ApplicationDbContext _dbContext;
        public RolRepositorio(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<PaginadoResponse<Rol>> BusquedaPaginado(PaginationRequest dto)
        {
            var contex = _dbContext.Set<Rol>().AsQueryable();

            if (string.IsNullOrWhiteSpace(dto.Sort)) dto.Sort = "createAt.desc";

            if (!string.IsNullOrWhiteSpace(dto.Sort))
            {
                var ColumnsOrder = dto.Sort.Split(".");
                
                var column = ColumnsOrder[0];
                var order = ColumnsOrder[1];

                contex = column switch
                {
                    "name" => order == "desc" ? contex.OrderByDescending(p => p.Name) : contex.OrderBy(p => p.Name),
                    "status" => order == "desc" ? contex.OrderByDescending(p => p.Status) : contex.OrderBy(p => p.Status),
                    "createAt" => order == "desc" ? contex.OrderByDescending(p => p.CreateAt) : contex.OrderBy(p => p.CreateAt),
                };

            }


            if (dto.Filters != null && dto.Filters.Length > 0)
            {
                foreach (var filter in dto.Filters)
                {
                    var id_value = filter.Split(":");

                    var id = id_value[0];
                    var value = id_value[1];

                    if (id == "status") {
                        if (value == "activo") contex = contex.Where(p => p.Status == true);
                        if (value == "inactivo") contex =  contex.Where(p => p.Status == false);
                    } 
                    else if (id == "name") contex = contex.Where(p => p.Name.Contains(value));

                }
            }

            
            var data = await contex.Skip(dto.Page * dto.Limit).Take(dto.Limit).ToListAsync();
            var total = await contex.CountAsync();

            var meta = new Meta
            {
                Total = total,
                Page = dto.Page,
                LastPage = (int)Math.Ceiling((double)total / dto.Limit)
            };


            PaginadoResponse<Rol> response = new(data, meta);

            return response;
        }

    }
}
