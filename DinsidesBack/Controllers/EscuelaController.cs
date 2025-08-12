using Application.Escuelas.Dtos;
using Application.Escuelas.Services.Interfaces;
using Application.Preguntas.Dto;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EscuelaController : ControllerBase
    {
        private readonly IEscuelaService _escuelaService;
        public EscuelaController(IEscuelaService escuelaService)
        {
            _escuelaService = escuelaService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EscuelaDto>>>> Post([FromBody] EscuelaSaveDto request)
        {

            var response = await _escuelaService.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EscuelaDto>>>> Put(int id, [FromBody] EscuelaSaveDto request)
        {

            var response = await _escuelaService.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
