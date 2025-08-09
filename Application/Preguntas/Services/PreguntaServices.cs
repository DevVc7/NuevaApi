using Application.Exceptions;
using Application.Preguntas.Dto;
using Application.Preguntas.Services.Interfaces;
using AutoMapper;
using Domain;
using Domain.View;
using Infraestructure.Repositories.Interfaces;
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

        public PreguntaServices(IPreguntaRepositorio questionRepositorio, IOpcionRepositorio opcionRepositorio, IMapper mapper, IMateriaRepositorio subjectRepositorio)
        {
            _questionRepositorio = questionRepositorio;
            _mapper = mapper;
            _subjectRepositorio = subjectRepositorio;
            _opcionRepositorio = opcionRepositorio;
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
                Message = "Pregunta Eliminada con exito"
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

        public async Task<PreguntaDto> FindByIdAsync(int id)
        {
            var response = await _questionRepositorio.FindByIdAsync(id);

            return _mapper.Map<PreguntaDto>(response);
        }
    }
}
