using Application.Escuelas.Dtos;
using Application.Escuelas.Services.Interfaces;
using Application.Exceptions;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;

namespace Application.Escuelas.Services
{
    public class EscuelaService : IEscuelaService
    {
        public readonly IEscuelaRepositorio _escuelaRepositorio;
        public readonly IMapper _mapper;
        
        public EscuelaService(IEscuelaRepositorio escuelaRepositorio, IMapper mapper)
        {
            _escuelaRepositorio = escuelaRepositorio;
            _mapper = mapper;
        }
        
        public async Task<OperationResult<EscuelaDto>> CreateAsync(EscuelaSaveDto saveDto)
        {
            var escuela = _mapper.Map<Escuela>(saveDto);

            await _escuelaRepositorio.SaveAsync(escuela);

            return new OperationResult<EscuelaDto>()
            {
                Data = _mapper.Map<EscuelaDto>(escuela),
                Message = "Escuela creada con exito",
                State = true
            };
        }

        public Task<OperationResult<EscuelaDto>> DisabledAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<EscuelaDto>> EditAsync(int id, EscuelaSaveDto saveDto)
        {
            var escuela = await _escuelaRepositorio.FindByIdAsync(id);

            if (escuela == null) throw new NotFoundCoreException("Registro no encontrado con el id: " + id); 
            
            _mapper.Map(saveDto , escuela);

            await _escuelaRepositorio.SaveAsync(escuela);

            return new OperationResult<EscuelaDto>()
            {
                Data = _mapper.Map<EscuelaDto>(escuela),
                Message = "Registro Actualizado",
                State = true
            };
        }

        public Task<IReadOnlyList<EscuelaDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EscuelaDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
