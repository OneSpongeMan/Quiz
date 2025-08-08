using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface ILogRecordLoader
    {
        bool GetLogRecord(DateTime dateStart, DateTime dateEnd, string authorName = "");
    }
}
