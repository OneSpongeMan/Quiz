using Quiz.Shared.Models;

namespace Quiz.Shared.Interfaces
{
    public interface ICertificateGenerator
    {
        byte[] Generate(Guid resultId);
    }
}
