using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.Mappers;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;
using NutritionalRecipeBook.Application.Services.Extensions;

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

        public async Task<Guid?> CreateRecipeAsync(RecipeIngredientDTO? recipeDto)
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
                await this.CheckExistencyAsync(
                    recipeData,
                    _logger,
                    _unitOfWork
                    );

                var recipeEntity = RecipeMapper.ToEntity(recipeData);
                await _unitOfWork.Repository<Recipe, Guid>().InsertAsync(recipeEntity);

                if (recipeDto.Ingredients.Count > 0)
                {
                    await this.ProcessRecipeIngredientsAsync(
                        recipeEntity, 
                        recipeDto.Ingredients,
                        _logger,
                        _unitOfWork,
                        _ingredientService
                        );
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

        public async Task<bool> UpdateRecipeAsync(Guid id, RecipeIngredientDTO? recipeDto)
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

                existingRecipe = RecipeMapper.ToEntity(recipeDto.RecipeDTO);

                if (recipeDto.Ingredients.Count > 0)
                {
                    await this.UpdateRecipeIngredientsAsync(
                        existingRecipe, 
                        recipeDto.Ingredients,
                        _logger,
                        _unitOfWork,
                        _ingredientService
                        );
                }

                await _unitOfWork.Repository<Recipe, Guid>().UpdateAsync(existingRecipe);
                bool isSaved = await _unitOfWork.SaveAsync();

                if (!isSaved)
                {
                    _logger.LogWarning("UpdateRecipeAsync failed: SaveAsync returned false for recipe ID {Id}.", id);
                    return false;
                }

                _logger.LogInformation("Recipe with ID {Id} and ingredients updated successfully.", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating recipe ID {Id}.", id);
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
                await this.CheckExistencyAsync(id, _logger, _unitOfWork);
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
        
        public async Task<RecipeIngredientDTO?> GetRecipeByIdAsync(Guid id)
        {
            try
            {
                await this.CheckExistencyAsync(id, _logger, _unitOfWork);

                var recipeEntity = await _unitOfWork.Repository<Recipe, Guid>().GetByIdAsync(id);
                var recipeDto = RecipeMapper.ToDto(recipeEntity!);

                var recipeIngredients = await this.LoadRecipeIngredientsAsync(
                    id,
                    _unitOfWork,
                    _logger
                    );

                var ingredientAmountDtos = recipeIngredients
                    .Select(ri => new IngredientAmountDTO(
                        new IngredientDTO(
                            ri.Ingredient!.Id,
                            ri.Ingredient.Name,
                            ri.Ingredient.IsLiquid
                        ),
                        ri.Amount,
                        ri.Unit
                    ))
                    .ToList();

                _logger.LogInformation("Retrieved {Count} ingredients for recipe ID {Id}.",
                    ingredientAmountDtos.Count, id);

                return new RecipeIngredientDTO(recipeDto, ingredientAmountDtos);
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
                var recipeDtos = recipeEntities.Select(recipeEntity => RecipeMapper.ToDto(recipeEntity));

                return recipeDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving all recipes.");
                
                return Enumerable.Empty<RecipeDTO>();
            }
        }
        
        public PagedResultDTO<RecipeDTO> GetRecipesAsync(int pageNumber, int pageSize, RecipeFilterDTO filterDto = null)
        {
            try
            {
                var repo = _unitOfWork.Repository<Recipe, Guid>();
                var query =  _unitOfWork.Repository<Recipe, Guid>().GetQueryable();
                _logger.LogInformation("Building query for recipes with search '{Search}', " +
                                       "page {PageNumber}, size {PageSize}, minTime {MinTime}, maxTime {MaxTime}, " +
                                       "minServ {MinServ}, maxServ {MaxServ}.",
                    filterDto.Search, pageNumber, pageSize, filterDto.MinCookingTimeInMin, 
                    filterDto.MaxCookingTimeInMin, filterDto.MinServings, filterDto.MaxServings);

                if (!string.IsNullOrWhiteSpace(filterDto.Search))
                {
                    var search = filterDto.Search.ToLower();

                    query = query.Where(r =>
                        r.Name.ToLower().Contains(search) ||
                        r.Description.ToLower().Contains(search) ||
                        r.Instructions.ToLower().Contains(search) ||
                        r.RecipeIngredients.Any(ri => ri.Ingredient.Name.ToLower().Contains(search))
                    );
                }

                query = repo.GetWhereIf(query, filterDto.MinCookingTimeInMin.HasValue, r => 
                    r.CookingTimeInMin >= filterDto.MinCookingTimeInMin!.Value);
                query = repo.GetWhereIf(query, filterDto.MaxCookingTimeInMin.HasValue, r =>
                    r.CookingTimeInMin <= filterDto.MaxCookingTimeInMin!.Value);
                query = repo.GetWhereIf(query, filterDto.MinServings.HasValue, r =>
                    r.Servings >= filterDto.MinServings!.Value);
                query = repo.GetWhereIf(query, filterDto.MaxServings.HasValue, r =>
                    r.Servings <= filterDto.MaxServings!.Value);
                
                int totalCount = query.Count();

                var recipes = query
                    .OrderBy(r => r.Name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(r => RecipeMapper.ToDto(r))
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
    }
}