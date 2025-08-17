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
using System.Collections.Generic;
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
        private readonly IPredictionService _predictionService;

        public PreguntaServices(
            IPreguntaRepositorio questionRepositorio, 
            IOpcionRepositorio opcionRepositorio, 
            IMapper mapper, 
            IMateriaRepositorio subjectRepositorio, 
            IRespuestaUsuarioRepositorio respuestaUsuarioRepositorio,
            IPredictionService predictionService)
        {
            _questionRepositorio = questionRepositorio;
            _mapper = mapper;
            _subjectRepositorio = subjectRepositorio;
            _opcionRepositorio = opcionRepositorio;
            _respuestaUsuarioRepositorio = respuestaUsuarioRepositorio;
            _predictionService = predictionService;
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
            var lastQuestion = await _questionRepositorio.FindByIdAsync(idPreguntaRespondida);
            if (lastQuestion == null || !lastQuestion.idCurso.HasValue) return null;

            var nextQuestionId = await _predictionService.GetNextQuestion(idUsuario, lastQuestion.idCurso.Value);

            if (nextQuestionId == 0) return null;

            var nextQuestion = await _questionRepositorio.FindAdaptiveQuestionByIdAsync(nextQuestionId);
            return _mapper.Map<PreguntaDto>(nextQuestion);
        }

        public async Task<bool> ResetAdaptiveTestAsync(int idUsuario, int idCurso)
        {
            var rowsAffected = await _respuestaUsuarioRepositorio.DeleteByUserIdAndCourseIdAsync(idUsuario, idCurso);
            return rowsAffected > 0;
        }



        public async Task<IReadOnlyList<ProgresoStudentRespuesta>> ProgresoStudentRespuesta()
        {
            var respuestas = await _respuestaUsuarioRepositorio.FindAllAsync();
            var preguntas = await _questionRepositorio.FindAllAsync();

            // total de preguntas
            int totalPreguntas = preguntas.Count;
            int totalMate = preguntas.Count(p => p.IdMateria == 1);
            int totalComu = preguntas.Count(p => p.IdMateria == 2);

            // agrupamos por usuario
            var progresoPorUsuario = respuestas
                .GroupBy(r => r.IdUsuario)
                .Select(g =>
                {
                    var respondidas = g.Select(r => r.IdPregunta).Distinct().ToList();

                    int respondidasTotal = respondidas.Count;
                    int respondidasMate = respondidas.Count(id =>
                        preguntas.FirstOrDefault(p => p.IdPregunta == id)?.IdMateria == 1);
                    int respondidasComu = respondidas.Count(id =>
                        preguntas.FirstOrDefault(p => p.IdPregunta == id)?.IdMateria == 2);

                    return new ProgresoStudentRespuesta
                    {
                        IdUsuario = g.Key,
                        ProgresoTotal = totalPreguntas == 0 ? 0 : (int)Math.Round((decimal)respondidasTotal / totalPreguntas * 100),
                        ProgresoMatematica = totalMate == 0 ? 0 : (int)Math.Round((decimal)respondidasMate / totalMate * 100),
                        ProgresoComunicacion = totalComu == 0 ? 0 : (int)Math.Round((decimal)respondidasComu / totalComu * 100)
                    };
                })
                .ToList();

            return progresoPorUsuario;
        }


    }
}
