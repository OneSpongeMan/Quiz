using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.DTO;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;
using System.Security.Claims;

namespace Quiz.API.Controllers
{
    [Route("api/quiz/user")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class QuizzUserController : ControllerBase
    {
        private readonly IQuizzService _quizzService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public QuizzUserController(IQuizzService quizzService, IUserService userService, IMapper mapper)
        {
            _quizzService = quizzService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("quiz/{id}")]
        public QuizzDTO GetQuizz(Guid id)
        {
            return _mapper.Map<QuizzDTO>(_quizzService.GetQuizz(id));
        }

        [HttpGet("quizzes")]
        public IEnumerable<QuizzDTO> GetAllQuizzes()
        {
            return _quizzService.GetAllQuizzes()
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpGet("quizzes/sorted")]
        public IEnumerable<QuizzDTO> GetAllQuizzesSorted()
        {
            return _quizzService.GetAllQuizzesSorted()
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpGet("quizzes/name:{name}")]
        public IEnumerable<QuizzDTO> GetQuizzesByName(string name)
        {
            return _quizzService.GetQuizzByName(name)
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpGet("quizzes/tagged")]
        public IEnumerable<QuizzDTO> GetAllQuizzesByTags(ICollection<string> tags)
        {
            return _quizzService.GetAllQuizzesByTags(tags)
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpGet("quizzes/{authorId}_authored")]
        public IEnumerable<QuizzDTO> GetAllQuizzesByAuthor(string authorId)
        {
            return _quizzService.GetAllQuizzesByAuthor(authorId)
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpGet("quizzes/{userId}_completed")]
        public IEnumerable<QuizzDTO> GetAllQuizzesUserCompleted(string userId)
        {
            return _quizzService.GetAllQuizzesUserCompleted(userId)
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpPost("quiz/start/user_{userId}:{id}")]
        public bool StartQuizz(Guid id, string userId)
        {
            return _quizzService.StartQuizz(id, userId);
        }

        [HttpPut("quiz/end/user_{userId}:{id}")]
        public bool EmdQuizz(Guid id, string userId)
        {
            return _quizzService.EndQuizz(id, userId);
        }

        [HttpPut("quiz/end/quiz_result:{resultId}")]
        public void CompleteQuizIfTimeExpired(Guid resultId)
        {
            _quizzService.CompleteQuizIfTimeExpired(resultId);
        }

        [HttpPost]
        public bool CreateQuizz([FromBody] QuizzDTO quizz)
        {
            return _quizzService.CreateQuizz(GetCurrentUser(), _mapper.Map<Quizz>(quizz));
        }

        [HttpDelete("quiz/{id}")]
        public bool DeleteQuizz(Guid id)
        {
            return _quizzService.DeleteQuizz(GetCurrentUser(), id);
        }

        [HttpPut]
        public bool UpdateQuizz([FromBody] QuizzDTO quizz)
        {
            return _quizzService.UpdateQuizz(GetCurrentUser(), _mapper.Map<Quizz>(quizz));
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
