using Application.Materias.Dtos;
using Application.Materias.Dtos.SaveDtos;
using Application.Materias.Services.Interfaces;
using Application.Preguntas.Dto;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly IMateriaServices _materiaServices;

        public MateriaController(IMateriaServices materiaServices)
        {
            _materiaServices = materiaServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<MateriaDto>>>> Get()
        {

            var response = await _materiaServices.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<MateriaDto>>> Get(int id)
        {
            var response = await _materiaServices.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MateriaDto>>>> Post([FromBody] MateriaCursoSaveDto request)
        {

            var response = await _materiaServices.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MateriaDto>>>> Put(int id, [FromBody] MateriaCursoSaveDto request)
        {

            var response = await _materiaServices.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<MateriaDto>>>> Delete(int id)
        {

            var response = await _materiaServices.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


    }
}
