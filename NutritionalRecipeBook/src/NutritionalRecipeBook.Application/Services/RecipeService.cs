using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;
using RecipeIngredient = NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs.RecipeIngredient;

namespace NutritionalRecipeBook.Application.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RecipeService> _logger;
        private readonly IIngredientService _ingredientService;

        public RecipeService(IUnitOfWork unitOfWork, ILogger<RecipeService> logger, 
            IIngredientService ingredientService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _ingredientService = ingredientService;
        }

        public async Task<Guid?> CreateRecipeAsync(RecipeIngredient? recipeDto)
        {
            if (recipeDto?.RecipeDTO == null)
            {
                _logger.LogWarning("CreateRecipeAsync failed: RecipeDTO is null.");
                
                return null;
            }

            var recipeData = recipeDto.RecipeDTO;

            if (string.IsNullOrWhiteSpace(recipeData.Name))
            {
                _logger.LogWarning("CreateRecipeAsync failed: Recipe name is required.");
                
                return null;
            }

            try
            {
                await CheckExistencyAsync(recipeData);

                var recipeEntity = new Recipe
                {
                    Name = recipeData.Name.Trim(),
                    Description = recipeData.Description?.Trim() ?? string.Empty,
                    Instructions = recipeData.Instructions?.Trim() ?? string.Empty,
                    CookingTimeInMin = recipeData.CookingTimeInMin,
                    Servings = recipeData.Servings
                };

                await _unitOfWork.Repository<Recipe, Guid>().InsertAsync(recipeEntity);

                if (recipeDto.Ingredients.Count > 0)
                {
                    await ProcessRecipeIngredientsAsync(recipeEntity, recipeDto.Ingredients);
                }

                bool isSaved = await _unitOfWork.SaveAsync();
                if (!isSaved)
                {
                    _logger.LogWarning("CreateRecipeAsync failed: SaveAsync returned false.");
                    
                    return null;
                }

                _logger.LogInformation("Recipe '{Name}' created successfully with ID {Id}.",
                    recipeEntity.Name, recipeEntity.Id);
               
                return recipeEntity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating recipe '{Name}'.", 
                    recipeDto.RecipeDTO.Name);
               
                return null;
            }
        }

        public async Task<bool> UpdateRecipeAsync(Guid id, RecipeIngredient? recipeDto)
        {
            if (recipeDto?.RecipeDTO == null)
            {
                _logger.LogWarning("UpdateRecipeAsync failed: RecipeDTO is null.");
                
                return false;
            }

            try
            {
                var existingRecipe = await _unitOfWork.Repository<Recipe, Guid>().GetByIdAsync(id);
                if (existingRecipe == null)
                {
                    _logger.LogWarning("UpdateRecipeAsync failed: Recipe with ID {Id} not found.", id);
                    
                    return false;
                }

                var recipeData = recipeDto.RecipeDTO;
                existingRecipe.Name = recipeData.Name?.Trim() ?? existingRecipe.Name;
                existingRecipe.Description = recipeData.Description?.Trim() ?? string.Empty;
                existingRecipe.Instructions = recipeData.Instructions?.Trim() ?? string.Empty;
                existingRecipe.CookingTimeInMin = recipeData.CookingTimeInMin;
                existingRecipe.Servings = recipeData.Servings;

                await _unitOfWork.Repository<Recipe, Guid>().UpdateAsync(existingRecipe);

                if (recipeDto.Ingredients.Count > 0)
                {
                    await UpdateRecipeIngredientsAsync(existingRecipe, recipeDto.Ingredients);
                }

                bool isSaved = await _unitOfWork.SaveAsync();
                if (!isSaved)
                {
                    _logger.LogWarning(
                        "UpdateRecipeAsync failed: SaveAsync returned false for recipe ID {Id}.", id);
                   
                    return false;
                }

                _logger.LogInformation("Recipe with ID {Id} and ingredients updated successfully.", id);
               
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An unexpected error occurred while updating recipe ID {Id}.", id);
               
                return false;
            }
        }

        public async Task<Guid?> GetRecipeIdByNameAsync(string name)
        {
            try
            {
                var recipe = await _unitOfWork.Repository<Recipe, Guid>()
                    .GetSingleOrDefaultAsync(r => r.Name.ToLower() == name.ToLower());

                if (recipe == null)
                {
                    _logger.LogWarning("Recipe with name '{RecipeName}' not found.", name);
                    return null;
                }

                return recipe.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "An unexpected error occurred while retrieving recipe name {RecipeName}.", name);
                
                return null;
            }
        }
        
        public async Task<bool> DeleteRecipeAsync(Guid id)
        {
            try
            {
                await CheckExistencyAsync(id);
                await _unitOfWork.Repository<Recipe, Guid>().DeleteAsync(id);

                bool isSaved = await _unitOfWork.SaveAsync();
                if (!isSaved)
                {
                    _logger.LogWarning("DeleteRecipeAsync failed: SaveAsync returned false for recipe ID {Id}.", id);
                    
                    return false;
                }

                _logger.LogInformation("Recipe with ID {Id} deleted successfully.", id);
                
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting recipe ID {Id}.", id);
                
                return false;
            }
        }
        
        public async Task<RecipeDTO?> GetRecipeByIdAsync(Guid id)
        {
            try
            {
                await CheckExistencyAsync(id);
                var recipeEntity = await _unitOfWork.Repository<Recipe, Guid>().GetByIdAsync(id);

                var recipeDto = new RecipeDTO
                (
                    recipeEntity.Id,
                    recipeEntity.Name,
                    recipeEntity.Description,
                    recipeEntity.Instructions,
                    recipeEntity.CookingTimeInMin,
                    recipeEntity.Servings
                );

                return recipeDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving recipe ID {Id}.", id);
                
                return null;
            }
        }
        
        public IEnumerable<RecipeDTO> GetAllRecipesAsync()
        {
            try
            {
                var recipeEntities = _unitOfWork.Repository<Recipe, Guid>().GetAll();
                
                var recipeDtos = recipeEntities.Select(recipeEntity => new RecipeDTO
                (
                    recipeEntity.Id,
                    recipeEntity.Name,
                    recipeEntity.Description,
                    recipeEntity.Instructions,
                    recipeEntity.CookingTimeInMin,
                    recipeEntity.Servings
                ));

                return recipeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving all recipes.");
                
                return Enumerable.Empty<RecipeDTO>();
            }
        }
        
        public PagedResultDTO<RecipeDTO> GetRecipesAsync(
            string? search,
            int pageNumber,
            int pageSize,
            int? minCookingTimeInMin = null,
            int? maxCookingTimeInMin = null,
            int? minServings = null,
            int? maxServings = null)
        {
            try
            {
                var repo = _unitOfWork.Repository<Recipe, Guid>();
                var query =  _unitOfWork.Repository<Recipe, Guid>().GetQueryable();
                _logger.LogInformation("Building query for recipes with search '{Search}', page {PageNumber}, size {PageSize}, minTime {MinTime}, maxTime {MaxTime}, minServ {MinServ}, maxServ {MaxServ}.",
                    search, pageNumber, pageSize, minCookingTimeInMin, maxCookingTimeInMin, minServings, maxServings);

                if (!string.IsNullOrWhiteSpace(search))
                {
                    search = search.ToLower();

                    query = query.Where(r =>
                        r.Name.ToLower().Contains(search) ||
                        r.Description.ToLower().Contains(search) ||
                        r.Instructions.ToLower().Contains(search) ||
                        r.RecipeIngredients.Any(ri => ri.Ingredient.Name.ToLower().Contains(search))
                    );
                }

                query = repo.GetWhereIf(query, minCookingTimeInMin.HasValue, r => r.CookingTimeInMin >= minCookingTimeInMin!.Value);
                query = repo.GetWhereIf(query, maxCookingTimeInMin.HasValue, r => r.CookingTimeInMin <= maxCookingTimeInMin!.Value);
                query = repo.GetWhereIf(query, minServings.HasValue, r => r.Servings >= minServings!.Value);
                query = repo.GetWhereIf(query, maxServings.HasValue, r => r.Servings <= maxServings!.Value);
                
                int totalCount = query.Count();

                var recipes = query
                    .OrderBy(r => r.Name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(r => 
                        new RecipeDTO(r.Id, r.Name, r.Description, r.Instructions, r.CookingTimeInMin, r.Servings))
                    .ToList();
                
                _logger.LogInformation("Retrieved {Count} recipes for page {PageNumber} with page size {PageSize}.",
                    recipes.Count, pageNumber, pageSize);

                return new PagedResultDTO<RecipeDTO>(recipes, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving filtered/paged recipes.");
                
                return new PagedResultDTO<RecipeDTO>(new List<RecipeDTO>(), 0, pageNumber, pageSize);
            }
        }

        private async Task ProcessRecipeIngredientsAsync(Recipe recipeEntity, List<IngredientAmountDTO> ingredientDTOs)
        {
            var uniqueIngredients = ingredientDTOs
                .GroupBy(i => 
                    i.IngredientDTO.Name.Trim(), StringComparer.OrdinalIgnoreCase)
                .Select(g => g.First())
                .ToList();

            foreach (var ingredientAmount in uniqueIngredients)
            {
                await _ingredientService.EnsureIngredientExistsAsync(ingredientAmount.IngredientDTO);

                var ingredientEntity = await _unitOfWork.Repository<Ingredient, Guid>()
                    .GetSingleOrDefaultAsync(i => 
                        i.Name.ToLower() == ingredientAmount.IngredientDTO.Name.Trim().ToLower());

                if (ingredientEntity == null)
                {
                    _logger.LogWarning("Ingredient '{Name}' could not be found after creation attempt.", 
                        ingredientAmount.IngredientDTO.Name);
                   
                    continue;
                }

                var recipeIngredient = new Domain.ConnectionTables.RecipeIngredient
                {
                    RecipeId = recipeEntity.Id,
                    IngredientId = ingredientEntity.Id,
                    Amount = ingredientAmount.Amount,
                    Unit = ingredientAmount.Unit?.Trim() ?? string.Empty
                };

                await _unitOfWork.Repository<Domain.ConnectionTables.RecipeIngredient, (Guid, Guid)>().InsertAsync(recipeIngredient);
            }
        }

        private async Task UpdateRecipeIngredientsAsync(Recipe recipeEntity, List<IngredientAmountDTO> ingredientDtos)
        {
            var currentEntries = 
                await _unitOfWork.Repository<Domain.ConnectionTables.RecipeIngredient, (Guid, Guid)>()
                .GetWhereAsync(ri => ri.RecipeId == recipeEntity.Id);

            var newIngredientNames = ingredientDtos
                .Select(i => i.IngredientDTO.Name.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            var toRemove = currentEntries
                .Where(ri => !newIngredientNames
                    .Contains(ri.Ingredient.Name, StringComparer.OrdinalIgnoreCase))
                .ToList();

            foreach (var removeItem in toRemove)
            {
                await _unitOfWork.Repository<Domain.ConnectionTables.RecipeIngredient, (Guid, Guid)>().DeleteAsync(removeItem);
            }

            foreach (var ingredientAmount in ingredientDtos)
            {
                bool isExists = await _ingredientService.EnsureIngredientExistsAsync(ingredientAmount.IngredientDTO);
                if(!isExists)
                {
                    _logger.LogWarning("UpdateRecipeAsync: Failed to ensure ingredient '{Name}' exists.",
                        ingredientAmount.IngredientDTO.Name);
                    
                    continue;
                }
                
                var ingredientEntityId = await _ingredientService
                    .GetIngredientIdByNameAsync(ingredientAmount.IngredientDTO.Name.Trim().ToLower());

                if (ingredientEntityId == null)
                {
                    _logger.LogWarning(
                        "UpdateRecipeAsync: Ingredient '{Name}' could not be found after creation attempt.",
                        ingredientAmount.IngredientDTO.Name);
                    
                    continue;
                }

                var existingEntry = await _unitOfWork.Repository<Domain.ConnectionTables.RecipeIngredient, (Guid, Guid)>()
                    .GetSingleOrDefaultAsync(ri => 
                        ri.RecipeId == recipeEntity.Id && ri.IngredientId == ingredientEntityId);

                if (existingEntry == null)
                {
                    var newLink = new Domain.ConnectionTables.RecipeIngredient
                    {
                        RecipeId = recipeEntity.Id,
                        IngredientId = ingredientEntityId.Value,
                        Amount = ingredientAmount.Amount,
                        Unit = ingredientAmount.Unit?.Trim() ?? string.Empty
                    };

                    await _unitOfWork.Repository<Domain.ConnectionTables.RecipeIngredient, (Guid, Guid)>().InsertAsync(newLink);
                }
                else
                {
                    existingEntry.Amount = ingredientAmount.Amount;
                    existingEntry.Unit = ingredientAmount.Unit?.Trim() ?? string.Empty;
                    
                    await _unitOfWork.Repository<Domain.ConnectionTables.RecipeIngredient, (Guid, Guid)>().UpdateAsync(existingEntry);
                }
            }
        }
        
        private async Task CheckExistencyAsync(RecipeDTO? recipeDto)
        {
            if (recipeDto == null || string.IsNullOrWhiteSpace(recipeDto.Name))
            {
                _logger.LogWarning("CheckExistencyAsync: recipeDto is null or missing a name.");
                throw new ArgumentException("Recipe data is invalid.");
            }

            string normalizedName = recipeDto.Name.Trim().ToLower();

            var existingRecipe = await _unitOfWork.Repository<Recipe, Guid>()
                .GetSingleOrDefaultAsync(r => r.Name.ToLower() == normalizedName);

            if (existingRecipe != null)
            {
                _logger.LogWarning("CheckExistencyAsync failed: Recipe '{Name}' already exists.", recipeDto.Name);
                
                throw new InvalidOperationException($"Recipe '{recipeDto.Name}' already exists.");
            }
        }
        
        private async Task CheckExistencyAsync(Guid id)
        {
            var existingRecipe = await _unitOfWork.Repository<Recipe, Guid>().GetByIdAsync(id);

            if (existingRecipe == null)
            {
                _logger.LogWarning("CheckExistencyAsync failed: Recipe with ID {Id} not found.", id);
                
                throw new KeyNotFoundException($"Recipe with ID '{id}' does not exist.");
            }
        }
    }
}