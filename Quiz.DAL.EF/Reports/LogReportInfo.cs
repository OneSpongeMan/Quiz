using Quiz.Shared.Models;

namespace Quiz.DAL.EF.Reports
{
    internal class LogReportInfo
    {
        public DateTime DateStart;
        public DateTime DateEnd;
        public LogReportStat ReportStat;
        public Dictionary<string, LogReportStat> UsersStat;

        public LogReportInfo(DateTime dateStart, DateTime dateEnd, List<LogRecord> logRecords)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
            ReportStat = new LogReportStat();
            UsersStat = new Dictionary<string, LogReportStat>();
            ComposeStats(logRecords);
        }

        private void ComposeStats(List<LogRecord> logRecords)
        {
            foreach (var record in logRecords)
            {
                if (!UsersStat.ContainsKey(record.User.UserName))
                {
                    UsersStat.Add(record.User.UserName, new LogReportStat());
                }

                var logParts = record.LogSummary.Split(new char[] { '[', ']', ':', '\n', ' ' }, StringSplitOptions.RemoveEmptyEntries);

                switch (logParts[0]) // Action Type: Create, Update, etc.
                {
                    case "Create" when logParts[1] == "Question": // Correspondace between action's type and entity's type
                        //UsersStat[record.User.UserName].AddedQuestionCount++;
                        //ReportStat.AddedQuestionCount++;
                        UsersStat[record.User.UserName].AddedQuestionCount++;
                        ReportStat.AddedQuestionCount++;
                        break;
                    case "Create" when logParts[1] == "Answer":
                        UsersStat[record.User.UserName].AddedAnswerCount++;
                        ReportStat.AddedAnswerCount++;
                        break;

                    case "Update" when logParts[1] == "Question":
                        //UsersStat[record.User.UserName].UpdatedQuestionCount++;
                        //ReportStat.UpdatedQuestionCount++;
                        UsersStat[record.User.UserName].UpdatedQuestionCount++;
                        ReportStat.UpdatedQuestionCount++;
                        break;
                    case "Update" when logParts[1] == "Answer":
                        UsersStat[record.User.UserName].UpdatedAnswerCount++;
                        ReportStat.UpdatedAnswerCount++;
                        break;

                    case "Delete" when logParts[1] == "Question":
                        //UsersStat[record.User.UserName].DeletedQuestionCount++;
                        //ReportStat.DeletedQuestionCount++;
                        UsersStat[record.User.UserName].DeletedQuestionCount++;
                        ReportStat.DeletedQuestionCount++;
                        break;
                    case "Delete" when logParts[1] == "Answer":
                        UsersStat[record.User.UserName].DeletedAnswerCount++;
                        ReportStat.DeletedAnswerCount++;
                        break;
                }
            }
        }
    }
}
