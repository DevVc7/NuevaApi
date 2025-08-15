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
        public readonly ILeccionRepositorio _leccionRepositorio;
        public readonly IMapper _mapper;

        public MateriaService(ILeccionRepositorio leccionRepositorio,ICursoRepositorio cursoRepositorio, IMateriaRepositorio materiaRepositorio, IMapper mapper)
        {
            _cursoRepositorio = cursoRepositorio;
            _materiaRepositorio = materiaRepositorio;
            _leccionRepositorio = leccionRepositorio;
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
                    curs.Materia = null;
                    curs.IdCurso = null;

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

            if (materia == null) return new OperationResult<MateriaDto>() { Data = _mapper.Map<MateriaDto>(materia), Message = "Nombre de Materia ya esta Inscrito", State = false };

            materia.Estado = !materia.Estado;
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
                        
                        new_curso.IdCurso = null;
                        new_curso.IdMateria = materia.IdMateria;
                        new_curso.CreatedAt = DateTime.Now;
                        new_curso.Estado = true;
                        new_curso.Materia = null;

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

        public async Task<CursoDto> FecthByIdCu(int idCurso)
        {
            var response = await _cursoRepositorio.FindByIdAsync(idCurso);

            return _mapper.Map<CursoDto>(response);
        }

        public async Task<IReadOnlyList<LeccionDto>> FecthByIdCurso(int idCurso)
        {
            var response = await _leccionRepositorio.FindByIdCursoAsync(idCurso);

            return _mapper.Map<IReadOnlyList<LeccionDto>>(response);
        }

        public async Task<IReadOnlyList<CursoDto>> FecthByIdMateria(int idMateria)
        {
            var response = await _cursoRepositorio.FindByIdMateriaAsync(idMateria);

            return _mapper.Map<IReadOnlyList<CursoDto>>(response);

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

        public async Task<OperationResult<CursoDto>> SaveCursoLeccion(int id, CursoLeccionSaveDto saveDto)
        {
            var curso = await _cursoRepositorio.FindByIdAsync(id);

            if (curso == null) return new OperationResult<CursoDto>() { Data = _mapper.Map<CursoDto>(curso), Message = "No hay Registro con ese Id", State = false };

            curso.UpdatedAt = DateTime.Now;
            curso.Estado = true;

            _mapper.Map(saveDto.CursoSaveDto, curso);

            await _cursoRepositorio.SaveAsync(curso);


            if (saveDto.LeccionSaveDtoList != null && saveDto.LeccionSaveDtoList.Count != 0)
            {
                foreach (var leccion in saveDto.LeccionSaveDtoList)
                {
                    var cl = await _leccionRepositorio.FindByIdAsync(leccion.IdLeccion);

                    if (cl != null)
                    {
                        _mapper.Map(leccion, cl);
                        await _leccionRepositorio.SaveAsync(cl);
                    }
                    else
                    {
                        var new_curso = _mapper.Map<Leccion>(leccion);

                        new_curso.IdLeccion = null;
                        new_curso.IdCurso = curso.IdCurso;
                        new_curso.CreatedAt = DateTime.Now;
                        new_curso.Estado = true;
                        new_curso.Curso = null;

                        await _leccionRepositorio.SaveAsync(new_curso);
                    }
                }
            }

            return new OperationResult<CursoDto>()
            {
                State = true,
                Data = _mapper.Map<CursoDto>(curso),
                Message = "curso creado con exito"
            };
        }
    }
}
