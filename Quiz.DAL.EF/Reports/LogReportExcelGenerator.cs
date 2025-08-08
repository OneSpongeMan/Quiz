using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace Quiz.DAL.EF.Reports
{
    internal class LogReportExcelGenerator
    {
        public LogReportExcelGenerator() { }


        public byte[] Generate(LogReportInfo reportInfo)
        {
            var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Report");

            sheet.Cells[2, 2].Value = "Отчёт";
            sheet.Cells[3, 2].Value = "Статистика по записям";
            sheet.Cells[4, 2].Value = "С:";
            sheet.Cells[4, 3].Style.Numberformat.Format = "yyyy-mm-dd";
            sheet.Cells[4, 3].Value = reportInfo.DateStart;
            sheet.Cells[5, 2].Value = "По:";
            sheet.Cells[5, 3].Style.Numberformat.Format = "yyyy-mm-dd";
            sheet.Cells[5, 3].Value = reportInfo.DateEnd;

            sheet.Cells[7, 2].Value = "Создано вопросов:";
            sheet.Cells[7, 3].Value = reportInfo.ReportStat.AddedQuestionCount;
            sheet.Cells[8, 2].Value = "Удалено вопросов:";
            sheet.Cells[8, 3].Value = reportInfo.ReportStat.DeletedQuestionCount;
            sheet.Cells[9, 2].Value = "Изменено вопросов:";
            sheet.Cells[9, 3].Value = reportInfo.ReportStat.UpdatedQuestionCount;

            sheet.Cells[11, 2].Value = "Создано ответов:";
            sheet.Cells[11, 3].Value = reportInfo.ReportStat.AddedAnswerCount;
            sheet.Cells[12, 2].Value = "Удалено ответов:";
            sheet.Cells[12, 3].Value = reportInfo.ReportStat.DeletedAnswerCount;
            sheet.Cells[13, 2].Value = "Изменено ответов:";
            sheet.Cells[13, 3].Value = reportInfo.ReportStat.UpdatedAnswerCount;

            sheet.Cells[15, 2, 15, 4].LoadFromArrays(new object[][] { new[] { "Пользователь", "Создано", "Удалено", "Обновлено" } });
            var row = 16;
            var column = 2;
            foreach (var stat in reportInfo.UsersStat)
            {
                sheet.Cells[row, column].Value = stat.Key;
                sheet.Cells[row, column + 1].Value = stat.Value.AddedQuestionCount;
                sheet.Cells[row, column + 2].Value = stat.Value.DeletedQuestionCount;
                sheet.Cells[row, column + 3].Value = stat.Value.UpdatedQuestionCount;
                row++;
            }
            sheet.Cells[1, 1, row, column + 3].AutoFitColumns();

            ExcelPieChart pieChart = sheet.Drawings.AddChart("pieChart", eChartType.Pie) as ExcelPieChart;
            pieChart.Series.Add(ExcelRange.GetAddress(7, 3, 9, 3), ExcelRange.GetAddress(7, 2, 9, 2));
            pieChart.Legend.Position = eLegendPosition.Bottom;
            pieChart.DataLabel.ShowPercent = true;
            pieChart.SetSize(300, 300);
            pieChart.SetPosition(2, 0, 6, 0);

            return package.GetAsByteArray();
        }
    }
}
