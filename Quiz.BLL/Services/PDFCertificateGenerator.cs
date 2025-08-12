using Npgsql.Internal;
using Quiz.Shared.Interfaces;
using Quiz.Shared.Models;
using System.IO;
using System.Reflection.Metadata;

using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Pdf;

namespace Quiz.BLL.Services
{
    public class PDFCertificateGenerator : ICertificateGenerator
    {
        private IResultLoader _resultLoader;

        public PDFCertificateGenerator(IResultLoader resultLoader)
        {
            _resultLoader = resultLoader;
        }

        public void Generate(Guid resultId)
        {

        }
    }
}
