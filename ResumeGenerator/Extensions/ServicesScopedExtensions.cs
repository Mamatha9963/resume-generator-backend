using ResumeGenerator.Interfaces;
using ResumeGenerator.Services;

namespace ResumeGenerator.Extensions
{
    public static class ServicesScopedExtensions
    {
        public static IServiceCollection AddServiceScopedExtensions(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IOpenAiService, OpenAiService>();
            return serviceCollection;
        }
    }
}
