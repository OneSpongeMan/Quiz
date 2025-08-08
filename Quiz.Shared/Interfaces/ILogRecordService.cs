using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface ILogRecordService
    {
        bool GetLogRecord(DateTime dateStart, DateTime dateEnd, string authorName = "");
    }
}
