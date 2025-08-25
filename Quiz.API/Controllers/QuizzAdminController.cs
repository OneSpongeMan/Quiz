using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.DTO;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.API.Controllers
{
    [Route("api/admin/quiz")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class QuizzAdminController : ControllerBase
    {
        private readonly IQuizzService _quizzService;
        private readonly IMapper _mapper;

        public QuizzAdminController(IQuizzService quizzService, IMapper mapper)
        {
            _quizzService = quizzService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public QuizzDTO GetQuizz(Guid id)
        {
            return _mapper.Map<QuizzDTO>(_quizzService.GetQuizz(id));
        }

        [HttpGet]
        public IEnumerable<QuizzDTO> GetAllQuizzes()
        {
            return _quizzService.GetAllQuizzes()
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpGet("sorted")]
        public IEnumerable<QuizzDTO> GetAllQuizzesSorted()
        {
            return _quizzService.GetAllQuizzesSorted()
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpGet("name:{name}")]
        public IEnumerable<QuizzDTO> GetQuizzesByName(string name)
        {
            return _quizzService.GetQuizzByName(name)
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpGet("tagged:{tags}")]
        public IEnumerable<QuizzDTO> GetAllQuizzesByTags(ICollection<string> tags)
        {
            return _quizzService.GetAllQuizzesByTags(tags)
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpGet("{authorId}_authored")]
        public IEnumerable<QuizzDTO> GetAllQuizzesByAuthor(string authorId)
        {
            return _quizzService.GetAllQuizzesByAuthor(authorId)
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpGet("{userId}_completed")]
        public IEnumerable<QuizzDTO> GetAllQuizzesUserCompleted(string userId)
        {
            return _quizzService.GetAllQuizzesUserCompleted(userId)
                .Select(q => _mapper.Map<QuizzDTO>(q));
        }

        [HttpPost("start/user_{userId}:{id}")]
        public bool StartQuizz(Guid id, string userId)
        {
            return _quizzService.StartQuizz(id, userId);
        }

        [HttpPut("end/user_{userId}:{id}")]
        public bool EmdQuizz(Guid id, string userId)
        {
            return _quizzService.EndQuizz(id, userId);
        }

        [HttpPut("end-forced/quiz_result:{resultId}")]
        public void CompleteQuizIfTimeExpired(Guid resultId)
        {
            _quizzService.CompleteQuizIfTimeExpired(resultId);
        }

        [HttpPost]
        public bool CreateQuizz([FromBody] QuizzDTO quizz)
        {
            return _quizzService.CreateQuizz(_mapper.Map<Quizz>(quizz));
        }

        [HttpDelete("{id}")]
        public bool DeleteQuizz(Guid id)
        {
            return _quizzService.DeleteQuizz(id);
        }

        [HttpPut]
        public bool UpdateQuizz([FromBody] QuizzDTO quizz)
        {
            return _quizzService.UpdateQuizz(_mapper.Map<Quizz>(quizz));
        }
    }
}
