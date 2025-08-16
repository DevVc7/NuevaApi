using Application.Preguntas.Services.Interfaces;
using Application.Preguntas.Dto;
using Domain;
using Domain.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PreguntaController : ControllerBase
    {
        private readonly IPreguntaServices _questionServices;
        public PreguntaController(IPreguntaServices questionServices)
        {
            _questionServices = questionServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<PreguntaDto>>>> Get()
        {

            var response = await _questionServices.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PreguntaDto>>> Get(int id)
        {
            var response = await _questionServices.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PreguntaDto>>>> Post([FromBody] QuestionRequestDto request)
        {

            var response = await _questionServices.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PreguntaDto>>>> Put(int id, [FromBody] QuestionRequestDto request)
        {

            var response = await _questionServices.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<PreguntaDto>>>> Delete(int id)
        {

            var response = await _questionServices.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<QuestionDataResponse>>> BusquedaPaginado()
        {

            var response = await _questionServices.BusquedaPaginado();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("materia/{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<PreguntaDto>>>> GetMateria(int id)
        {
            var response = await _questionServices.FindAllMateriaAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }
    }
}
