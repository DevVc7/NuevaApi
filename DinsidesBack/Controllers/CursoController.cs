using Application.Materias.Dtos;
using Application.Materias.Dtos.SaveDtos;
using Application.Materias.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController : ControllerBase
    {
        private readonly IMateriaServices _materiaServices;

        public CursoController(IMateriaServices materiaServices)
        {
            _materiaServices = materiaServices;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<CursoDto>>> Get(int id)
        {
            var response = await _materiaServices.FecthByIdCu(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpGet("materia/{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<CursoDto>>>> MateriaGet(int id)
        {
            var response = await _materiaServices.FecthByIdMateria(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpGet("leccion/{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<LeccionDto>>>> leccionGet(int id)
        {
            var response = await _materiaServices.FecthByIdCurso(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<CursoDto>>>> Post(int id, [FromBody] CursoLeccionSaveDto request)
        {

            var response = await _materiaServices.SaveCursoLeccion(id,request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
