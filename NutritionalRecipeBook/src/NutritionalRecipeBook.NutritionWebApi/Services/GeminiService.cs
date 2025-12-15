using NutritionalRecipeBook.NutritionWebApi.Contracts;
using Google.GenAI;
using Google.GenAI.Types;

namespace NutritionalRecipeBook.NutritionWebApi.Services;

public class GeminiService : IGeminiService
{
    private readonly string _apiKey;
    private readonly Client _client;

    public GeminiService(string apiKey)
    {
        _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
        _client = new Client(apiKey: _apiKey);
    }

    public async Task<string> GetNutritionDataAsync(string prompt)
    {
        if (string.IsNullOrWhiteSpace(prompt))
            throw new ArgumentException("Prompt must be provided", nameof(prompt));

        var config = new GenerateContentConfig
        {
            ResponseMimeType = "application/json",
            Temperature = 0.1f
        };

        var response = await _client.Models.GenerateContentAsync(
            model: "gemini-2.5-flash", 
            contents: prompt,
            config: config
        );

        var text = response?.Candidates?[0]?.Content?.Parts?[0]?.Text;

        return text ?? string.Empty;
    }
}