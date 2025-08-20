using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.DTO;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.API.Controllers
{
    [Route("api/question/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class QuestionAdminController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IQuizzService _quizzService;
        private readonly IMapper _mapper;

        public QuestionAdminController(IQuestionService questionService, IQuizzService quizzService, IMapper mapper)
        {
            _questionService = questionService;
            _quizzService = quizzService;
            _mapper = mapper;
        }

        [HttpGet("question/{id}")]
        public QuestionDTO GetQuestion(Guid id)
        {
            return _mapper.Map<QuestionDTO>(_questionService.GetAllQuestions(id));
        }

        [HttpGet("questions")]
        public IEnumerable<QuestionDTO> GetQuestions(Guid quizzId)
        {
            return _questionService.GetAllQuestions(quizzId)
                .Select(q => _mapper.Map<QuestionDTO>(q));
        }

        [HttpPost]
        public bool CreateQuestion([FromBody] QuestionDTO question)
        {
            Quizz quizz = _quizzService.GetQuizz(question.QuizzId);
            return _questionService.CreateQuestion(quizz, _mapper.Map<Question>(question));
        }

        [HttpDelete("question/{id}")]
        public bool DeleteQuestion(Guid id)
        {
            return _questionService.DeleteQuestion(id);
        }

        [HttpPut]
        public bool UpdateQuestion([FromBody] QuestionDTO question)
        {
            return _questionService.UpdateQuestion(_mapper.Map<Question>(question));
        }
    }
}
