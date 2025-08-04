using Quiz.Shared.Models;
using System.Diagnostics;

namespace Quiz.DAL.EF.Reports
{
    internal class LogReport
    {
        private readonly List<LogRecord> _logRecords;
        private FileService _fileService;
        private LogReportExcelGenerator _excelGenerator;
        private readonly string _filePath = $"C:\\{Environment.UserName}\\Downloads\\LogReport.xlsx";

        public DateTime _dateStart;
        public DateTime _dateEnd;
        public string _authorName;

        public LogReport(List<LogRecord> logRecords, FileService fileService, DateTime dateStart, DateTime dateEnd, string authorName)
        {
            _logRecords = logRecords;
            _fileService = fileService;
            _dateStart = dateStart;
            _dateEnd = dateEnd;
            _authorName = authorName;
        }

        public void Generate()
        {
            var filteredRecords = new List<LogRecord>();

            if (_authorName != null)
            {
                filteredRecords = _logRecords
                    .Where(e => e.CreatedDate > _dateStart && e.CreatedDate < _dateEnd && e.User.UserName == _authorName)
                    .ToList();
            }
            else
            {
                filteredRecords = _logRecords
                    .Where(e => e.CreatedDate > _dateStart && e.CreatedDate < _dateEnd)
                    .ToList();
            }

            var reportInfo = new LogReportInfo(_dateStart, _dateEnd, filteredRecords);
            var excelReport = _excelGenerator.Generate(reportInfo);
            _fileService.Write(_filePath, excelReport);
        }
    }
}
