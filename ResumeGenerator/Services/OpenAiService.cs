using OpenAI_API;
using OpenAI_API.Completions;
using ResumeGenerator.Interfaces;
using ResumeGenerator.Models;
using System.Text.RegularExpressions;


namespace ResumeGenerator.Services
{
  public class OpenAiService : IOpenAiService
  {
    private readonly OpenAIAPI _api;

    public OpenAiService(IConfiguration config)
    {
       var apiKey = config["OPENAI_API_KEY"];
      _api = new OpenAIAPI(apiKey);
    }
    public async Task<string> GenerateResumeAsync(ResumeRequest request)
    {
      try
      {
        var prompt = $@"
You are a professional resume writer. Based on the details below, generate a complete, clean, professional resume **and return it ONLY as a JSON object** with the following structure:

{{
  ""name"": ""{request.Name}"",
  ""email"": ""{request.Email}"",
  ""phone"": ""{request.Phone}"",
  ""summary"": ""Write 3-4 lines summarizing the candidate's experience, job title, and value they bring."",
  ""skills"": [ {string.Join(", ", request.Skills.Split(',').Select(s => $"\"{s.Trim()}\""))} ],
  ""experience"": [
    {{
      ""jobTitle"": ""{request.JobTitle}"",
      ""company"": ""Company Name"",
      ""location"": ""City, Country"",
      ""duration"": ""{request.Experience} years"",
      ""bullets"": [
        ""Add 1-2 bullet points describing achievements/responsibilities.""
      ]
    }}
  ],
  ""education"": ""{request.Education}"",
  ""achievements"": [ {string.Join(", ", request.Achievements.Split(',').Select(a => $"\"{a.Trim()}\""))} ]
}}

Return ONLY the JSON object without any additional text, markdown, or explanation.
";


        var result = await _api.Completions.CreateCompletionAsync(new CompletionRequest
        {
          Prompt = prompt,
          Model = "gpt-4o-mini",
          MaxTokens = 1000,
          Temperature = 0.7
        });

        return Regex.Replace(result.Completions[0].Text.Trim(), @"^[-]{3,}\s", "");
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }
  }
}
