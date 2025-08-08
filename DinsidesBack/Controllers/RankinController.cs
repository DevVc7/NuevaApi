using Application.Estudiant.Dtos.Students;
using Application.Studens.Services.Interface;
using Domain;
using Domain.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RankinController : ControllerBase
    {
        private readonly IStudentServices _studentServices;

        public RankinController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<EstudianteDto>>>> Get()
        {

            var response = await _studentServices.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<EstudianteDto>>> Get(int id)
        {
            var response = await _studentServices.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EstudianteDto>>>> Post([FromBody] EstudianteSaveDto request)
        {

            var response = await _studentServices.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EstudianteDto>>>> Put(int id, [FromBody] EstudianteSaveDto request)
        {

            var response = await _studentServices.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<EstudianteDto>>>> Delete(int id)
        {

            var response = await _studentServices.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<StudentsMeta<Estudiante>>>> BusquedaPaginado()
        {

            var response = await _studentServices.BusquedaPaginado();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
