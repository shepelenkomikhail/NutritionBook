using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs.RecipeControllerDTOs;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.DTOs.Mappers;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Domain.ConnectionTables;
using NutritionalRecipeBook.Infrastructure.Contracts;
using NutritionalRecipeBook.Application.Services.Extensions;
using NutritionalRecipeBook.Application.Services.Helpers;

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

        public async Task<Guid?> CreateRecipeAsync(RecipeIngredientDTO? recipeDto, Guid userId)
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

                var userRecipe = new UserRecipe
                {
                    UserId = userId,
                    RecipeId = recipeEntity.Id,
                    IsOwner = true,
                    IsFavourite = false,
                    Rating = 0
                };
                await _unitOfWork.Repository<UserRecipe, Guid>().InsertAsync(userRecipe);

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

                bool isSaved = await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "CreateRecipeAsync");
                if (!isSaved)
                {
                    _logger.LogWarning("CreateRecipeAsync failed: SaveAsync returned false.");
                    
                    return null;
                }

                _logger.LogInformation("Recipe '{Name}' created successfully with ID {Id}. Linked to user {UserId}.",
                    recipeEntity.Name, recipeEntity.Id, userId);
               
                return recipeEntity.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating recipe '{Name}'.", 
                    recipeDto.RecipeDTO.Name);
               
                return null;
            }
        }

        public async Task<bool> UpdateRecipeAsync(Guid id, RecipeIngredientDTO? recipeDto, Guid userId)
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
                
                var userRecipe = await _unitOfWork.Repository<UserRecipe, Guid>()
                    .GetSingleOrDefaultAsync(ur => ur.UserId == userId && ur.RecipeId == id);
                
                if (userRecipe is null || !userRecipe.IsOwner)
                {
                    _logger.LogWarning("User {UserId} is not authorized to update recipe with ID {RecipeId}.", userId, id);
                
                    return false;
                }
                
                var updated = recipeDto.RecipeDTO;
                existingRecipe.Name = updated.Name?.Trim() ?? existingRecipe.Name;
                existingRecipe.Description = updated.Description?.Trim() ?? string.Empty;
                existingRecipe.Instructions = updated.Instructions?.Trim() ?? string.Empty;
                existingRecipe.CookingTimeInMin = updated.CookingTimeInMin;
                existingRecipe.Servings = updated.Servings;
                existingRecipe.ImageUrl = updated.ImageUrl;

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
                
                _logger.LogInformation("Recipe with ID {Id} and ingredients updated successfully.", id);
                
                return await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "UpdateRecipeAsync");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating recipe ID {Id}.", id);
                
                return false;
            }
        }

        public async Task<string?> UploadImageAsync(Stream? fileStream, string originalFileName, string webRootPath)
        {
            if (fileStream == null || string.IsNullOrWhiteSpace(originalFileName))
            {
                _logger.LogWarning("UploadImageAsync: Invalid file input.");
                
                return null;
            }

            try
            {
                var ext = Path.GetExtension(originalFileName).ToLowerInvariant();
                var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp" };
                
                if (Array.IndexOf(allowed, ext) < 0)
                {
                    _logger.LogWarning("UploadImageAsync: Unsupported file extension {Ext}.", ext);
                   
                    return null;
                }

                if (string.IsNullOrWhiteSpace(webRootPath))
                {
                    webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                }

                var imagesPath = Path.Combine(webRootPath, "images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var newFileName = $"{Guid.NewGuid():N}{ext}";
                var fullPath = Path.Combine(imagesPath, newFileName);

                if (fileStream.CanSeek)
                {
                    fileStream.Seek(0, SeekOrigin.Begin);
                }

                await using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await fileStream.CopyToAsync(fs);
                }

                var apiUrl = $"/api/recipes/image/{newFileName}";
                
                return apiUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UploadImageAsync: Error saving file {Name}.", originalFileName);
                
                return null;
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
        
        public async Task<bool> DeleteRecipeAsync(Guid id, Guid userId)
        {
            try
            {
                await this.CheckExistencyAsync(id, _logger, _unitOfWork);

                var userRecipe = await _unitOfWork.Repository<UserRecipe, Guid>()
                    .GetSingleOrDefaultAsync(ur => ur.UserId == userId && ur.RecipeId == id);
                if (userRecipe is null || !userRecipe.IsOwner)
                {
                    _logger.LogWarning("User {UserId} is not authorized to delete recipe with ID {RecipeId}.", userId, id);
                    return false;
                }
                await _unitOfWork.Repository<Recipe, Guid>().DeleteAsync(id);

                _logger.LogInformation("Recipe with ID {Id} deleted successfully.", id);
                
                return await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "DeleteRecipeAsync");
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
                var query =  _unitOfWork.Repository<Recipe, Guid>().GetQueryable();
                _logger.LogInformation("Building query for recipes with search '{Search}', " +
                                       "page {PageNumber}, size {PageSize}, minTime {MinTime}, maxTime {MaxTime}, " +
                                       "minServ {MinServ}, maxServ {MaxServ}.",
                    filterDto.Search, pageNumber, pageSize, filterDto.MinCookingTimeInMin, 
                    filterDto.MaxCookingTimeInMin, filterDto.MinServings, filterDto.MaxServings);
                
                query = query.ApplyFilter(filterDto, _unitOfWork.Repository<Recipe, Guid>());
                
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

        public PagedResultDTO<RecipeDTO> GetRecipesForUserAsync(int pageNumber, int pageSize, Guid? userId, RecipeFilterDTO? filterDto = null)
        {
            try
            {
                var query = _unitOfWork.Repository<Recipe, Guid>().GetQueryable()
                    .Where(r => r.UserRecipes.Any(ur => ur.UserId == userId));
                
                query = query.ApplyFilter(filterDto, _unitOfWork.Repository<Recipe, Guid>());
                
                var totalCount = query.Count();
                
                var recipes = query
                    .OrderBy(r => r.Name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(r => RecipeMapper.ToDto(r))
                    .ToList();
                
                _logger.LogInformation("Retrieved {Count} recipes for page {PageNumber} with page size {PageSize} for user ID {UserId}.",
                    totalCount, pageNumber, pageSize, userId);

                return new PagedResultDTO<RecipeDTO>(recipes, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving filtered/paged recipes.");
                
                return new PagedResultDTO<RecipeDTO>(new List<RecipeDTO>(), 0, pageNumber, pageSize);
            }
        }

        public async Task<bool> MarkFavoriteRecipeAsync(Guid? recipeId, Guid userId)
        {
            if (recipeId == null)
            {
                _logger.LogWarning("Recipe ID is null.");
                
                return false;
            }
            
            var connections = (await _unitOfWork.Repository<UserRecipe, Guid>()
                    .GetWhereAsync(ur => ur.UserId == userId && ur.RecipeId == recipeId))
                .ToList();

            if (connections.Count == 0)
            {
                await _unitOfWork.Repository<UserRecipe, Guid>().InsertAsync(new UserRecipe
                {
                    RecipeId = recipeId.Value,
                    UserId = userId,
                    IsFavourite = true
                });
                
                _logger.LogInformation("Recipe with id {RecipeId} is marked as favorite for user {UserId} (new connection).",
                    recipeId, userId);
            }
            else
            {
                foreach (var ur in connections)
                {
                    if (ur.IsFavourite) continue;
                    
                    ur.IsFavourite = true;
                    await _unitOfWork.Repository<UserRecipe, Guid>().UpdateAsync(ur);
                }
                
                _logger.LogInformation(
                    "Updated {Count} user-recipe connections to favorite for recipe {RecipeId} and user {UserId}.",
                    connections.Count, recipeId, userId);
            }

            return await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "MarkFavoriteRecipeAsync");
        }

        public PagedResultDTO<RecipeDTO> GetFavoriteRecipesAsync(Guid userId, int pageNumber, int pageSize, RecipeFilterDTO? filterDto = null)
        {
            try
            {
                var query = _unitOfWork.Repository<Recipe, Guid>().GetQueryable()
                    .Where(r => r.UserRecipes.Any(ur => ur.UserId == userId && ur.IsFavourite));
                
                query = query.ApplyFilter(filterDto, _unitOfWork.Repository<Recipe, Guid>());

                var totalCount = query.Count();

                var recipes = query
                    .OrderBy(r => r.Name)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(r => RecipeMapper.ToDto(r))
                    .ToList();

                _logger.LogInformation("Retrieved {Count} favorite recipes for user {UserId} on page {PageNumber} size {PageSize}.",
                    totalCount, userId, pageNumber, pageSize);

                return new PagedResultDTO<RecipeDTO>(recipes, totalCount, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting favorite recipes for user {UserId}.", userId);
                return new PagedResultDTO<RecipeDTO>(new List<RecipeDTO>(), 0, pageNumber, pageSize);
            }
        }
    }
}