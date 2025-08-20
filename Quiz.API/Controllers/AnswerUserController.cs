using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.DTO;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;
using System.Security.Claims;

namespace Quiz.API.Controllers
{
    [Route("api/answer/user")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class AnswerUserController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly IQuestionService _questionService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AnswerUserController(IAnswerService answerService, IQuestionService questionService, IUserService userService, IMapper mapper)
        {
            _answerService = answerService;
            _questionService = questionService;
            _userService = userService;
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
            return _answerService.CreateAnswer(GetCurrentUser(), question, _mapper.Map<Answer>(answer));
        }

        [HttpDelete("answer/{id}")]
        public bool DeleteAnswer(Guid id)
        {
            return _answerService.DeleteAnswer(GetCurrentUser(), id);
        }

        [HttpPut]
        public bool UpdateAnswer([FromBody] AnswerDTO answer)
        {
            return _answerService.UpdateAnswer(GetCurrentUser(), _mapper.Map<Answer>(answer));
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
