using ResumeGenerator.Models;

namespace ResumeGenerator.Interfaces
{
    public interface IOpenAiService
    {
       Task<string> GenerateResumeAsync(ResumeRequest resumeRequest);
    }
}
