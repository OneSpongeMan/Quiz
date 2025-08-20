using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.API.DTO;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;
using System.Security.Claims;

namespace Quiz.API.Controllers
{
    [Route("api/result")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ResultController(IResultService resultService, IUserService userService, IMapper mapper)
        {
            _resultService = resultService;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ResultDTO GetResult(Guid id)
        {
            return _mapper.Map<ResultDTO>(_resultService.GetResult(id));
        }

        [HttpGet("quiz")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<ResultDTO> GetQuizzResults(Guid quizzId)
        {
            return _resultService.GetQuizzResults(quizzId)
                .Select(q => _mapper.Map<ResultDTO>(q));
        }

        [HttpGet("user")]
        [Authorize(Roles = "Admin, User")]
        public IEnumerable<ResultDTO> GetUserResults()
        {

            return _resultService.GetUserResults(GetCurrentUser().Id)
                .Select(q => _mapper.Map<ResultDTO>(q));
        }

        [HttpPut]
        public void ScoringPoints(List<Guid> answerIds, Guid questionId)
        {
            _resultService.ScoringPoints(answerIds, questionId, GetCurrentUser().Id);
        }

        [HttpGet]
        public bool IsResultValid(Guid id)
        {
            return _resultService.IsResultValid(id);
        }

        [HttpPost]
        public bool CreateResult([FromBody] ResultDTO result)
        {
            return _resultService.CreateResult(_mapper.Map<Result>(result));
        }

        [HttpDelete("{id}")]
        public bool DeleteAnswer(Guid id)
        {
            return _resultService.DeleteResult(id);
        }

        [HttpPut]
        public bool UpdateResult([FromBody] ResultDTO result)
        {
            return _resultService.UpdateResult(_mapper.Map<Result>(result));
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
