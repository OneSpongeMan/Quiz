using Quiz.Shared;
using Quiz.Shared.Models;
using Quiz.Shared.Interfaces;

namespace Quiz.BLL.Certificates
{
    public class PDFCertificateService : IPDFCertificateService
    {
        private readonly IResultService _resultService;
        private ICertificateGenerator _certificateGenerator;
        private FileService _fileService;
        private readonly string _filePath = $"C:\\{Environment.UserName}\\Downloads\\Result_{DateTime.Now}.pdf";
        //private string _filePath;

        private Guid resultId;

        public PDFCertificateService(IResultService resultService, ICertificateGenerator certificateGenerator)
        {
            _resultService = resultService;
            _certificateGenerator = certificateGenerator;
            _fileService = new FileService();
        }

        public void GeneratePDF()
        {
            var result = _resultService.GetResult(resultId);
            //_filePath = $"C:\\{Environment.UserName}\\Downloads\\Result_{result.Quizz.Name}_{DateTime.Now}.pdf";

            var PDFCertificate = _certificateGenerator.Generate(result.Id);
            //_fileService.Write(_filePath, PDFCertificate);

            Thread thread = new Thread(() => _fileService.Write(_filePath, PDFCertificate));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
