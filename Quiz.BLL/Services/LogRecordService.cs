using Quiz.Shared.Interfaces;

namespace Quiz.BLL.Services
{
    public class LogRecordService : ILogRecordService
    {
        private ILogRecordLoader _loader;

        public LogRecordService(ILogRecordLoader loader)
        {
            _loader = loader;
        }

        public bool GetLogRecord(DateTime dateStart, DateTime dateEnd, string authorName = "")
        {
            return _loader.GetLogRecord(dateStart, dateEnd, authorName);
        }
    }
}
