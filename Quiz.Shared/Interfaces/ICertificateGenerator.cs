using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface ICertificateGenerator
    {
        void Generate(Result result);
    }
}
