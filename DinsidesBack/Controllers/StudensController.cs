using Application.Studens.Dtos.Students;
using Application.Studens.Services.Interface;
using Application.Usuarios.Dto;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudensController : ControllerBase
    {
        private readonly IStudentServices _studentServices;

        public StudensController(IStudentServices studentServices)
        {
            _studentServices = studentServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<StudentsDto>>>> Get()
        {

            var response = await _studentServices.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<StudentsDto>>> Get(Guid id)
        {
            var response = await _studentServices.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<StudentsDto>>>> Post([FromBody] StudentsSaveDto request)
        {

            var response = await _studentServices.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<StudentsDto>>>> Put(Guid id, [FromBody] StudentsSaveDto request)
        {

            var response = await _studentServices.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<StudentsDto>>>> Delete(Guid id)
        {

            var response = await _studentServices.DisabledAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }


        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<StudentsMeta<Student>>>> BusquedaPaginado()
        {

            var response = await _studentServices.BusquedaPaginado();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
        

        
    }
}