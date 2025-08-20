using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.DTO;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.API.Controllers
{
    [Route("api/answer/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AnswerAdminController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;
        private readonly IMapper _mapper;

        public AnswerAdminController(IAnswerService answerService, IQuestionService questionService, IMapper mapper)
        {
            _answerService = answerService;
            _questionService = questionService;
            _mapper = mapper;
        }

        [HttpGet("answer/{id}")]
        public AnswerDTO GetAnswer(Guid id)
        {
            return _mapper.Map<AnswerDTO>(_answerService.GetAnswer(id));
        }

        [HttpGet("answers")]
        public IEnumerable<AnswerDTO> GetAllAnswers(Guid questionId)
        {
            return _answerService.GetAllAnswers(questionId)
                .Select(q => _mapper.Map<AnswerDTO>(q));
        }

        [HttpGet("right_answers")]
        public IEnumerable<AnswerDTO> GetRightAnswers(Guid questionId)
        {
            return _answerService.GetRightAnswers(questionId)
               .Select(q => _mapper.Map<AnswerDTO>(q));
        }

        [HttpPost]
        public bool CreateAnswer([FromBody] AnswerDTO answer)
        {
            Question question = _questionService.GetQuestion(answer.QuestionId);
            return _answerService.CreateAnswer(question, _mapper.Map<Answer>(answer));
        }

        [HttpDelete("answer/{id}")]
        public bool DeleteAnswer(Guid id)
        {
            return _answerService.DeleteAnswer(id);
        }

        [HttpPut]
        public bool UpdateAnswer([FromBody] AnswerDTO answer)
        {
            return _answerService.UpdateAnswer(_mapper.Map<Answer>(answer));
        }
    }
}
