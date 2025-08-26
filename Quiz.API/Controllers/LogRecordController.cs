using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quiz.Shared.Interfaces;

namespace Quiz.API.Controllers
{
    [Route("api/log")]
    [ApiController]
    [Authorize(Roles = "Admin, Manager")]
    public class LogRecordController : ControllerBase
    {
        private readonly ILogRecordService _logRecordService;

        public LogRecordController(ILogRecordService logRecordService)
        {
            _logRecordService = logRecordService;
        }

        [HttpGet("start-date/{start}/end-date/{end}")]
        public bool GetNote(DateTime start, DateTime end)
        {
            return _logRecordService.GetLogRecord(start, end);
        }

        [HttpGet("start-date/{start}/end-date/{end}/user-name/{name}")]
        public bool GetNote(DateTime start, DateTime end, string name)
        {
            return _logRecordService.GetLogRecord(start, end, name);
        }
    }
}
