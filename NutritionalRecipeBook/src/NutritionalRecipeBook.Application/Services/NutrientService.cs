using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;

namespace NutritionalRecipeBook.Application.Services;

public class NutrientService: INutrientService
{
    private readonly ILogger<NutrientService> _logger;
    private readonly HttpClient _httpClient;
    
    public NutrientService(ILogger<NutrientService> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }
    
    public async Task<IEnumerable<IngredientNutrientApiDTO>> GetAllNutrientsAsync()
    {
        var url = new Uri("/nutrients", UriKind.Relative);
        
        _logger.LogInformation("Fetching all nutrients from {Url}", url);
        
        var items = await _httpClient.GetFromJsonAsync<List<IngredientNutrientApiDTO>>(url) 
                    ?? new List<IngredientNutrientApiDTO>();
        
        return items;
    }

    public async Task<IEnumerable<IngredientNutrientApiDTO>> SearchNutrientsAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Array.Empty<IngredientNutrientApiDTO>();
        }
       
        var url = new Uri($"/search?query={Uri.EscapeDataString(query)}", UriKind.Relative);
        _logger.LogInformation("Searching nutrients with query '{Query}' at {Url}", query, url);
        
        var items = await _httpClient.GetFromJsonAsync<List<IngredientNutrientApiDTO>>(url) 
                    ?? new List<IngredientNutrientApiDTO>();
        
        return items;
    }
}