using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.IngredientControllerDTOs;

namespace NutritionalRecipeBook.Application.Services;

public class NutrientService: INutrientService
{
    private const int MaxQueryLength = 100;
    private readonly ILogger<NutrientService> _logger;
    private readonly HttpClient _httpClient;
    
    public NutrientService(ILogger<NutrientService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<IngredientNutrientApiDTO>> GetAllNutrientsAsync()
    {
        EnsureHttpClientConfigured();
        
        var url = new Uri("/nutrients", UriKind.Relative);
        _logger.LogInformation("Fetching all nutrients from {Url}", url);
        
        var result = await FetchNutrientsAsync(url);
        
        return result;
    }

    public async Task<IEnumerable<IngredientNutrientApiDTO>> SearchNutrientsAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            _logger.LogWarning("Skipping empty nutrient search query");
            
            return Array.Empty<IngredientNutrientApiDTO>();
        }
        query = query.Trim();
        if (query.Length > MaxQueryLength)
        {
            _logger.LogWarning("Search query truncated because it exceeded {MaxLength} characters",
                MaxQueryLength);
            
            query = query[..MaxQueryLength];
        }
        
        EnsureHttpClientConfigured();
        var url = new Uri($"/search?query={Uri.EscapeDataString(query)}", UriKind.Relative);
        
        _logger.LogInformation("Searching nutrients with query '{Query}' at {Url}", query, url);
        
        var result = await FetchNutrientsAsync(url);

        return result;
    }

    private void EnsureHttpClientConfigured()
    {
        if (_httpClient.BaseAddress is null)
        {
            throw new InvalidOperationException(
                "HttpClient BaseAddress must be configured for NutrientService");
        }
    }

    private async Task<IEnumerable<IngredientNutrientApiDTO>> FetchNutrientsAsync(Uri requestUri)
    {
        try
        {
            using var response = await _httpClient.GetAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Nutrients API returned non-success status {StatusCode}", response.StatusCode);
                
                return Array.Empty<IngredientNutrientApiDTO>();
            }
            
            _logger.LogInformation("Nutrients API returned {StatusCode}", response.StatusCode);
            
            var items = await response.Content.ReadFromJsonAsync<List<IngredientNutrientApiDTO>>()
                        ?? new List<IngredientNutrientApiDTO>();
            
            return items.Where(IsValidNutrient).ToArray();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex,
                "HTTP error during nutrient API call to {RequestUri}", requestUri);
        }
        catch (NotSupportedException ex)
        {
            _logger.LogError(ex, 
                "Unsupported media type from nutrient API at {RequestUri}", requestUri);
        }
        catch (System.Text.Json.JsonException ex)
        {
            _logger.LogError(ex,
                "Failed to deserialize nutrient API payload from {RequestUri}", requestUri);
        }
        
        return Array.Empty<IngredientNutrientApiDTO>();
    }

    private static bool IsValidNutrient(IngredientNutrientApiDTO dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Uom))
        {
            return false;
        }
        return dto.Calories >= 0 && dto.Proteins >= 0 && dto.Carbs >= 0 && dto.Fats >= 0;
    }
}