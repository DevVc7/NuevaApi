using Application.Exceptions;
using Application.Questions.Dto;
using Application.Questions.Services.Interfaces;
using Application.Studens.Dtos.Students;
using AutoMapper;
using Domain;
using Domain.View;
using Infraestructure.Repositories;
using Infraestructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Application.Questions.Services
{
    public class QuestionServices : IQuestionServices
    {
        private readonly IQuestionRepositorio _questionRepositorio;
        private readonly IMapper _mapper;   
        private readonly ISubjectRepositorio _subjectRepositorio;

        public QuestionServices(IQuestionRepositorio questionRepositorio, IMapper mapper, ISubjectRepositorio subjectRepositorio)
        {
            _questionRepositorio = questionRepositorio;
            _mapper = mapper;
            _subjectRepositorio = subjectRepositorio;
        }

        public async Task<QuestionDataResponse> BusquedaPaginado()
        {
            var response = await _questionRepositorio.BusquedaPaginado();

            return response;
        }

        public async Task<OperationResult<QuestionDto>> CreateAsync(QuestionSaveDto saveDto)
        {
            var question = _mapper.Map<Question>(saveDto);

            var sub_question = await _subjectRepositorio.GetSubjectsName(saveDto.Subject);

            question.CreatedAt = DateTime.Now;
            question.UpdatedAt = DateTime.Now;
            question.SubjectId = sub_question.Id;
            question.SubcategoryId = null;

            await _questionRepositorio.SaveAsync(question);

            return new OperationResult<QuestionDto>()
            {
                State = true,
                Data = _mapper.Map<QuestionDto>(question),
                Message = "Question creado con exito"
            };



        }

        public Task<OperationResult<QuestionDto>> DisabledAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult<QuestionDto>> EditAsync(Guid id, QuestionSaveDto saveDto)
        {
            var question = await _questionRepositorio.FindByIdAsync(id);
            var sub_question = await _subjectRepositorio.GetSubjectsName(saveDto.Subject);
            
            if (question == null) throw new NotFoundCoreException("question no encontrado para el id " + id);

            question.UpdatedAt = DateTime.Now;
            question.SubjectId = sub_question.Id;


            _mapper.Map(saveDto, question);

            await _questionRepositorio.SaveAsync(question);

            return new OperationResult<QuestionDto>()
            {
                State = true,
                Data = _mapper.Map<QuestionDto>(question),
                Message = "Question actualizado con exito"
            };
        }

        public async Task<IReadOnlyList<QuestionDto>> FindAllAsync()
        {
            var response = await _questionRepositorio.FindAllAsync();

            return _mapper.Map<IReadOnlyList<QuestionDto>>(response);
        }

        public async Task<QuestionDto> FindByIdAsync(Guid id)
        {
            var response = await _questionRepositorio.FindByIdAsync(id);

            return _mapper.Map<QuestionDto>(response);
        }
    }
}
