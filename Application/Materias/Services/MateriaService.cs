using Application.Materias.Dtos;
using Application.Materias.Dtos.SaveDtos;
using Application.Materias.Services.Interfaces;
using AutoMapper;
using Domain;
using Infraestructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Materias.Services
{
    public class MateriaService : IMateriaServices
    {
        public readonly ICursoRepositorio _cursoRepositorio;
        public readonly IMateriaRepositorio _materiaRepositorio;
        public readonly IMapper _mapper;

        public MateriaService(ICursoRepositorio cursoRepositorio, IMateriaRepositorio materiaRepositorio, IMapper mapper)
        {
            _cursoRepositorio = cursoRepositorio;
            _materiaRepositorio = materiaRepositorio;
            _mapper = mapper;
        }

        public async Task<OperationResult<MateriaDto>> CreateAsync(MateriaCursoSaveDto saveDto)
        {
            var materia = _mapper.Map<Materia>(saveDto.MateriaSaveDto);

            var valid = await _materiaRepositorio.FechtNameMateria(materia.Descripcion.ToLower().Trim());

            if (valid != null) return new OperationResult<MateriaDto>() { Data = _mapper.Map<MateriaDto>(materia), Message = "Nombre de Materia ya esta Inscrito", State = false };

            materia.Estado = true;
            materia.CreatedAt = DateTime.Now;

            await _materiaRepositorio.SaveAsync(materia);


            if (saveDto.CursoSaveDtoList != null && saveDto.CursoSaveDtoList.Count != 0)
            {
                foreach (var curso in saveDto.CursoSaveDtoList)
                {

                    Curso curs = _mapper.Map<Curso>(curso);
                    curs.CreatedAt = DateTime.Now;
                    curs.Estado = true;
                    curs.IdMateria = materia.IdMateria;

                    await _cursoRepositorio.SaveAsync(curs);
                }
            }

            return new OperationResult<MateriaDto>()
            {
                State = true,
                Data = _mapper.Map<MateriaDto>(materia),
                Message = "Materia creado con exito"
            };



        }

        public async Task<OperationResult<MateriaDto>> DisabledAsync(int id)
        {

            var materia = await _materiaRepositorio.FindByIdAsync(id);

            if (materia != null) return new OperationResult<MateriaDto>() { Data = _mapper.Map<MateriaDto>(materia), Message = "Nombre de Materia ya esta Inscrito", State = false };

            materia.Estado = false;
            materia.DeletedAt = DateTime.Now;

            await _materiaRepositorio.SaveAsync(materia);

            return new OperationResult<MateriaDto>()
            {
                State = true,
                Data = _mapper.Map<MateriaDto>(materia),
                Message = "Materia eliminado con exito"
            };
        }

        public async Task<OperationResult<MateriaDto>> EditAsync(int id, MateriaCursoSaveDto saveDto)
        {
            
            var materia = await _materiaRepositorio.FindByIdAsync(id);

            if (materia == null) return new OperationResult<MateriaDto>() { Data = _mapper.Map<MateriaDto>(materia), Message = "No hay Registro con ese Id", State = false };

            materia.UpdatedAt = DateTime.Now;

            _mapper.Map(saveDto.MateriaSaveDto, materia);

            await _materiaRepositorio.SaveAsync(materia);


            if (saveDto.CursoSaveDtoList != null && saveDto.CursoSaveDtoList.Count != 0)
            {
                foreach (var curso in saveDto.CursoSaveDtoList)
                {
                    var cl = await _cursoRepositorio.FindByIdAsync(curso.IdCurso);

                    if (cl != null)
                    {
                        _mapper.Map(curso, cl);
                        await _cursoRepositorio.SaveAsync(cl);
                    }
                    else
                    {
                        var new_curso = _mapper.Map<Curso>(curso);
                        new_curso.CreatedAt = DateTime.Now;
                        new_curso.IdMateria = materia.IdMateria;
                        new_curso.Estado = true;
                        await _cursoRepositorio.SaveAsync(new_curso);
                    }
                }
            }

            return new OperationResult<MateriaDto>()
            {
                State = true,
                Data = _mapper.Map<MateriaDto>(materia),
                Message = "Materia actualizado con exito"
            };
        }

        public async Task<IReadOnlyList<MateriaDto>> FindAllAsync()
        {
           var response = await _materiaRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<MateriaDto>>(response);
        }

        public async Task<MateriaDto> FindByIdAsync(int id)
        {
            var response = await _materiaRepositorio.FindByIdAsync(id);

            return _mapper.Map<MateriaDto>(response);
        }
    }
}
