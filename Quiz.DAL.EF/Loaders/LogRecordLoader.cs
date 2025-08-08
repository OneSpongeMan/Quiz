using Microsoft.EntityFrameworkCore;
using Quiz.DAL.EF.Reports;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Loaders
{
    public class LogRecordLoader : ILogRecordLoader
    {
        private ApplicationContext _applicationContext;

        public LogRecordLoader(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public bool GetLogRecord(DateTime dateStart, DateTime dateEnd, string authorName = "")
        {
            var logRecords = _applicationContext.LogRecords
                .Include(q => q.User)
                .ToList();
            var logReport = new LogReport(logRecords, dateStart, dateEnd, authorName);
            
            Thread receivingLog = new Thread(logReport.Generate);
            receivingLog.IsBackground = true;
            receivingLog.Start();

            return true;
        }
    }
}
