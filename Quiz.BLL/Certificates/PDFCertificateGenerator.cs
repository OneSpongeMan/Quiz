using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
//using System.IO;
//using System.Reflection.Metadata;

using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Npgsql.Internal;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;
using System.Drawing;
//using static System.Formats.Asn1.AsnWriter;

namespace Quiz.BLL.Certificates
{
    public class PDFCertificateGenerator : ICertificateGenerator
    {
        private IResultService _resultService;
        private IAnswerService _answerService;

        public PDFCertificateGenerator(IResultService resultService, IAnswerService answerService)
        {
            _resultService = resultService;
            _answerService = answerService;
        }

        public byte[] Generate(Result result)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = new PdfWriter(memoryStream);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                var font = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
                var font_plain = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);

                document.Add(new Paragraph($"Результаты викторины {result.Quizz.Name}")
                    .SetFont(font)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetMarginBottom(20));

                document.Add(new Paragraph()
                   .Add(new Text("Пользователь: "))
                   .Add(new Text($"{result.User.UserName}"))
                   .SetFont(font_plain)
                   .SetTextAlignment(TextAlignment.RIGHT)
                   .SetFontSize(14)
                   .SetMarginBottom(10));

                document.Add(new Paragraph()
                    .Add(new Text("Дата прохождения: "))
                    .Add(new Text($"{result.Start.ToString("dd.MM.yyyy")}"))
                    .SetFont(font_plain)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(14)
                    .SetMarginBottom(10));

                var duration = result.Finish - result.Start;
                document.Add(new Paragraph()
                    .Add(new Text("Время прохождения: "))
                    .Add(new Text($"{ duration?.TotalMinutes.ToString("F2") ?? "неизвестно"}"))
                    .SetFont(font_plain)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(14)
                    .SetMarginBottom(20));

                document.Add(new Paragraph("Результаты")
                    .SetFont(font)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetMarginBottom(15));

                var quizRightAnswers = 0;
                var quizTotalPoints = 0;
                foreach ( var question in result.Quizz.Questions)
                {
                    quizRightAnswers += _answerService.GetRightAnswers(question).Count;
                    quizTotalPoints += _answerService.GetRightAnswers(question).Sum(q => q.Score);
                }
                var scorePercentage = Math.Round((double)result.ScoredPoints 
                    / quizTotalPoints * 100);

                document.Add(new Paragraph()
                    .Add(new Text("Вы дали "))
                    .Add(new Text($"{result.RightAnswers} правильных ответов из {quizRightAnswers} возможных"))
                    .SetFont(font_plain)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetMarginBottom(15));

                document.Add(new Paragraph()
                    .Add(new Text($"Викторина пройдена на {scorePercentage}%"))
                    .SetFont(font_plain)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetMarginBottom(30));

                if (scorePercentage > 50)
                {
                    document.Add(new Paragraph()
                    .Add(new Text("Хороший результат, так держать!"))
                    .SetFont(font_plain)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetMarginBottom(15));
                }
                else
                {
                    document.Add(new Paragraph()
                    .Add(new Text("Можете постараться еще!"))
                    .SetFont(font_plain)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(24)
                    .SetMarginBottom(15));
                }

                document.Close();
                return memoryStream.ToArray();
            }            
        }
    }
}
