using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.DAL.EF.Reports
{
    internal class LogReportStat
    {
        public int AddedQuestionCount;
        public int DeletedQuestionCount;
        public int UpdatedQuestionCount;

        public int AddedAnswerCount;
        public int DeletedAnswerCount;
        public int UpdatedAnswerCount;

        public LogReportStat()
        {
            AddedQuestionCount = 0;
            DeletedQuestionCount = 0;
            UpdatedQuestionCount = 0;

            AddedAnswerCount = 0;
            DeletedAnswerCount = 0;
            UpdatedAnswerCount = 0;
        }

        public LogReportStat(int addedQuestionCount, int deletedQuestionCount, int updatedQuestionCount, int addedAnswerCount, int deletedAnswerCount, int updatedAnswerCount)
        {
            AddedQuestionCount = addedQuestionCount;
            DeletedQuestionCount = deletedQuestionCount;
            UpdatedQuestionCount = updatedQuestionCount;

            AddedAnswerCount = addedAnswerCount;
            DeletedAnswerCount = deletedAnswerCount;
            UpdatedAnswerCount = updatedAnswerCount;
        }
    }
}
