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

        [HttpPost("question")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<PreguntaDto>>>> GetQuestion([FromBody]  PreguntaView view)
        {
            var response = await _questionServices.FindAllQuestionMateria(view);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }


        [HttpPost("respuesta")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<OperationResult<RespuestaUsuarioDto>>>> postRespues([FromBody] RespuestaUsuarioSaveDto view)
        {
            var response = await _questionServices.SaveRespuesta(view);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();

        }

        [HttpGet("respuesta/all/{id}")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<RespuestaUsuarioDto>>>> AllRespuesta(int id)
        {

            var response = await _questionServices.FinPreguntaAsync(id);

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }

        // --- ENDPOINTS ADAPTATIVOS ---

        [HttpGet("iniciar-sesion-adaptativa")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<PreguntaDto>>> IniciarSesionAdaptativa(int idUsuario, int idCurso)
        {
            // servicio para manejar el 'cold start' y obtener la primera pregunta.
            // La lógica es: si el usuario no tiene historial en este curso, le devuelve la primera pregunta
            // Si ya tiene historial, le devuelve la siguiente pregunta según su último avance.
            var nextQuestion = await _questionServices.GetFirstAdaptiveQuestionAsync(idUsuario, idCurso);

            if (nextQuestion != null) return TypedResults.Ok(nextQuestion);

            return TypedResults.BadRequest();
        }

        [HttpPost("siguiente-pregunta-adaptativa")]
        [AllowAnonymous]
        public async Task<IResult> PostRespuesta([FromBody] RespuestaUsuarioSaveDto respuesta)
        {
            var saveResult = await _questionServices.SaveRespuesta(respuesta);
            if (saveResult == null) return TypedResults.BadRequest("Error al procesar la respuesta.");

            var nextQuestion = await _questionServices.GetNextAdaptiveQuestionAsync(respuesta.IdUsuario, respuesta.IdPregunta);

            // Si nextQuestion es null, significa que el usuario ha completado todas las preguntas.
            // Devolvemos un 204 No Content, una señal clara para el frontend de que el test ha finalizado.
            if (nextQuestion == null)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.Ok(nextQuestion);
        }

        [HttpPost("reiniciar/{idUsuario}/{idCurso}")]
        [AllowAnonymous]
        public async Task<IResult> ReiniciarTestAdaptativo(int idUsuario, int idCurso)
        {
            var result = await _questionServices.ResetAdaptiveTestAsync(idUsuario, idCurso);

            if (result)
            {
                return TypedResults.Ok(new { message = "El progreso del curso ha sido reiniciado." });
            }

            return TypedResults.BadRequest("No se pudo reiniciar el progreso. Es posible que el usuario no tuviera respuestas en este curso.");
        }


        [HttpGet("progresso")]
        [AllowAnonymous]
        public async Task<Results<BadRequest, Ok<IReadOnlyList<ProgresoStudentRespuesta>>>> ProgresoStudentRespuesta()
        {
            var response = await _questionServices.ProgresoStudentRespuesta();

            if (response != null) return TypedResults.Ok(response);

            return TypedResults.BadRequest();
        }
    }
}
