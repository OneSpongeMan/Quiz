using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.DTO;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;
using System.Security.Claims;

namespace Quiz.API.Controllers
{
    [Route("api/user/quiz/{quizzId}")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class QuestionUserController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IQuizzService _quizzService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public QuestionUserController(IQuestionService questionService, IQuizzService quizzService, IUserService userService, IMapper mapper)
        {
            _questionService = questionService;
            _quizzService = quizzService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("question/{id}")]
        public QuestionDTO GetQuestion(Guid id)
        {
            return _mapper.Map<QuestionDTO>(_questionService.GetQuestion(id));
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
            return _questionService.CreateQuestion(GetCurrentUser(), quizz, _mapper.Map<Question>(question));
        }

        [HttpDelete("question/{id}")]
        public bool DeleteQuestion(Guid id)
        {
            return _questionService.DeleteQuestion(GetCurrentUser(), id);
        }

        [HttpPut]
        public bool UpdateQuestion([FromBody] QuestionDTO question)
        {
            return _questionService.UpdateQuestion(GetCurrentUser(), _mapper.Map<Question>(question));
        }

        private User GetCurrentUser()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = _userService.GetUser(currentUserID);
            return user;
        }
    }
}
