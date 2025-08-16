using Application.Exceptions;
using Application.Preguntas.Dto;
using Application.Preguntas.Services.Interfaces;
using AutoMapper;
using Domain;
using Domain.View;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Numerics;

namespace Application.Questions.Services
{
    public class PreguntaServices : IPreguntaServices
    {
        private readonly IPreguntaRepositorio _questionRepositorio;
        private readonly IMapper _mapper;
        private readonly IMateriaRepositorio _subjectRepositorio;
        private readonly IOpcionRepositorio _opcionRepositorio;
        private readonly IRespuestaUsuarioRepositorio _respuestaUsuarioRepositorio;

        public PreguntaServices(
            IPreguntaRepositorio questionRepositorio, 
            IOpcionRepositorio opcionRepositorio, 
            IMapper mapper, IMateriaRepositorio subjectRepositorio, IRespuestaUsuarioRepositorio respuestaUsuarioRepositorio)
        {
            _questionRepositorio = questionRepositorio;
            _mapper = mapper;
            _subjectRepositorio = subjectRepositorio;
            _opcionRepositorio = opcionRepositorio;
            _respuestaUsuarioRepositorio = respuestaUsuarioRepositorio;
        }

        public async Task<QuestionDataResponse> BusquedaPaginado()
        {
            var response = await _questionRepositorio.BusquedaPaginado();

            return response;
        }

        public async Task<OperationResult<PreguntaDto>> CreateAsync(QuestionRequestDto saveDto)
        {
            var pregunta = _mapper.Map<Pregunta>(saveDto.PreguntaSaveDto);

            pregunta.Estado = true;
            pregunta.CreatedAt = DateTime.Now;

            await _questionRepositorio.SaveAsync(pregunta);

            if (saveDto.OpcionSaveDto != null && saveDto.OpcionSaveDto.Count != 0)
            {
                foreach (var op in saveDto.OpcionSaveDto)
                {
                    OpcionesRpt opcion = _mapper.Map<OpcionesRpt>(op);
                    int question = pregunta.IdPregunta;
                    opcion.IdPregunta = question;
                    opcion.Pregunta = null;

                    await _opcionRepositorio.SaveAsync(opcion);
                }
            }


            return new OperationResult<PreguntaDto>()
            {
                State = true,
                Data = _mapper.Map<PreguntaDto>(pregunta),
                Message = "Pregunta creado con exito"
            };



        }

        public async Task<OperationResult<PreguntaDto>> DisabledAsync(int id)
        {
            var pregunta = await _questionRepositorio.FindByIdAsync(id);

            if (pregunta == null) throw new NotFoundCoreException("Pregunta no encontrado para el id " + id);
            
            pregunta.Estado = !pregunta.Estado;
            pregunta.DeletedAt = DateTime.Now;

            await _questionRepositorio.SaveAsync(pregunta);

            return new OperationResult<PreguntaDto>()
            {
                State = true,
                Data = _mapper.Map<PreguntaDto>(pregunta),
                Message = "Pregunta Eliminad con exito"
            };
        }

        public async Task<OperationResult<PreguntaDto>> EditAsync(int id, QuestionRequestDto saveDto)
        {
            var pregunta = await _questionRepositorio.FindByIdAsync(id);

            if (pregunta == null) throw new NotFoundCoreException("Pregunta no encontrado para el id " + id);

            pregunta.UpdatedAt = DateTime.Now;

            _mapper.Map(saveDto.PreguntaSaveDto, pregunta);

            await _questionRepositorio.SaveAsync(pregunta);

            if (saveDto.OpcionSaveDto != null && saveDto.OpcionSaveDto.Count != 0)
            {
                foreach (var op in saveDto.OpcionSaveDto)
                {
                    
                    var opcion = await _opcionRepositorio.FindByIdAsync(op.IdOpcion);

                    if (opcion != null)
                    {
                        _mapper.Map(op, opcion);

                        await _opcionRepositorio.SaveAsync(opcion);
                    }
                    else
                    {
                        var new_op = _mapper.Map<OpcionesRpt>(op);
                        new_op.IdPregunta = pregunta.IdPregunta;
                        new_op.Pregunta = null;

                        await _opcionRepositorio.SaveAsync(new_op);
                    }
                }
            }

            return new OperationResult<PreguntaDto>()
            {
                State = true,
                Data = _mapper.Map<PreguntaDto>(pregunta),
                Message = "Pregunta Actualizada con exito"
            };
        }

        public async Task<IReadOnlyList<PreguntaDto>> FindAllAsync()
        {
            var response = await _questionRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<PreguntaDto>>(response);
        }

        public async Task<IReadOnlyList<PreguntaDto>> FindAllMateriaAsync(int id)
        {
            var response = await _questionRepositorio.FindAllMateriaAsync(id);

            return _mapper.Map<IReadOnlyList<PreguntaDto>>(response);
        }

        public async Task<IReadOnlyList<PreguntaDto>> FindAllQuestionMateria(PreguntaView view)
        {
            var response = await _questionRepositorio.FindAllQuestionMateria(view);

            return _mapper.Map<IReadOnlyList<PreguntaDto>>(response);
        }

        public async Task<PreguntaDto> FindByIdAsync(int id)
        {
            var response = await _questionRepositorio.FindByIdAsync(id);

            return _mapper.Map<PreguntaDto>(response);
        }

        public async Task<OperationResult<RespuestaUsuarioDto>> SaveRespuesta(RespuestaUsuarioSaveDto saveDto)
        {
            var respuesta = _mapper.Map<RespuestaUsuario>(saveDto);
            
            var valid = await _respuestaUsuarioRepositorio.FindByIdUsuarioAsync(saveDto.IdUsuario, saveDto.IdPregunta);

            if(valid != null)
            {
                return new OperationResult<RespuestaUsuarioDto>()
                {
                    State = true,
                    Data = _mapper.Map<RespuestaUsuarioDto>(respuesta),
                    Message = "Respuesta ya esta  registrada con exito"
                };
            }


            respuesta.FechaRespuesta = DateTime.Now;

            await _respuestaUsuarioRepositorio.SaveAsync(respuesta);

            return new OperationResult<RespuestaUsuarioDto>()
            {
                State = true,
                Data = _mapper.Map<RespuestaUsuarioDto>(respuesta),
                Message = "Respuesta creada con exito"
            };
        }

        public async Task<IReadOnlyList<RespuestaUsuarioDto>> FinPreguntaAsync(int id)
        {
            var response = await _respuestaUsuarioRepositorio.FindAllAsyncMateria(id);

            return _mapper.Map<IReadOnlyList<RespuestaUsuarioDto>>(response);
        }

        // -- 1. Lógica para el "Cold Start" (la primera pregunta) --
        public async Task<PreguntaDto> GetFirstAdaptiveQuestionAsync(int idUsuario, int idCurso)
        {
            var hasHistory = await _respuestaUsuarioRepositorio.HasHistoryAsync(idUsuario, idCurso);

            if (hasHistory)
            {
                var lastAnswer = await _respuestaUsuarioRepositorio.FindLastAnswerAsync(idUsuario, idCurso);

                return await GetNextAdaptiveQuestionAsync(idUsuario, lastAnswer.IdPregunta);
            }
            else
            {
                var firstQuestion = await _questionRepositorio.FindEasiestQuestionAsync(idCurso);

                return _mapper.Map<PreguntaDto>(firstQuestion);
            }
        }

        // -----------------------------------------------------------------
        // 2. El Corazón de la Lógica Adaptativa
        // -----------------------------------------------------------------
        public async Task<PreguntaDto> GetNextAdaptiveQuestionAsync(int idUsuario, int idPreguntaRespondida)
        {
            // Paso 1: Obtener los metadatos de la pregunta que acaba de ser respondida.
            var lastQuestion = await _questionRepositorio.FindByIdAsync(idPreguntaRespondida);

            if (lastQuestion == null) return null;

            // Paso 2: Obtener el resultado de la última respuesta del usuario.
            var lastAnswer = await _respuestaUsuarioRepositorio.FindLastAnswerForQuestionAsync(idUsuario, idPreguntaRespondida);

            // Paso 3: Validar que la pregunta tiene la información necesaria para la lógica adaptativa.
            if (!lastQuestion.IdLeccion.HasValue || string.IsNullOrEmpty(lastQuestion.Dificultad) || !lastQuestion.idCurso.HasValue)
            {
                return null; 
            }

            if (lastAnswer.PuntajeObtenido > 0) 
            {
                var nextQuestion = await _questionRepositorio.FindNextDifficultyAsync(lastQuestion.IdLeccion.Value, lastQuestion.Dificultad);

                if (nextQuestion != null)
                {
                    return _mapper.Map<PreguntaDto>(nextQuestion);
                }
                else
                {
                    var nextLessonQuestion = await _questionRepositorio.FindNextLessonEasiestQuestionAsync(lastQuestion.idCurso.Value, lastQuestion.IdLeccion.Value);
                    return _mapper.Map<PreguntaDto>(nextLessonQuestion);
                }
            }
            else
            {
                var nextQuestion = await _questionRepositorio.FindPreviousDifficultyAsync(lastQuestion.IdLeccion.Value, lastQuestion.Dificultad);

                return _mapper.Map<PreguntaDto>(nextQuestion);
            }
        }
    }
}
