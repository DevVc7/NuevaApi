using Application.Questions.Dto;
using Application.Questions.Services.Interfaces;
using Application.Studens.Dtos.Students;
using Domain;
using Domain.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DinsidesBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionServices _questionServices;
        public QuestionController(IQuestionServices questionServices)
        {
            _questionServices = questionServices;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<QuestionDto>>>> Get()
        {

            var response = await _questionServices.FindAllAsync();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<QuestionDto>>> Get(Guid id)
        {
            var response = await _questionServices.FindByIdAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<QuestionDto>>>> Post([FromBody] QuestionSaveDto request)
        {

            var response = await _questionServices.CreateAsync(request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<QuestionDto>>>> Put(Guid id, [FromBody] QuestionSaveDto request)
        {

            var response = await _questionServices.EditAsync(id, request);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<QuestionDto>>>> Delete(Guid id)
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
    }
}
