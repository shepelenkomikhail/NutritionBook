using Microsoft.Extensions.Logging;
using NutritionalRecipeBook.Application.Contracts;
using NutritionalRecipeBook.Application.DTOs;
using NutritionalRecipeBook.Application.Services.Helpers;
using NutritionalRecipeBook.Domain.ConnectionTables;
using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Infrastructure.Contracts;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace NutritionalRecipeBook.Application.Services;

public class ShoppingListService: IShoppingListService
{
    private readonly ILogger<ShoppingListService> _logger;
    private readonly IUnitOfWork _unitOfWork;
    
    public ShoppingListService(ILogger<ShoppingListService> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> AddItemsToShoppingListAsync(ShoppingListDTO? newShoppingList, Guid userId)
    {
        if (newShoppingList == null)
        {
            _logger.LogWarning("ShoppingListDTO is null.");
            
            return false;
        }

        if (newShoppingList?.IngredientUnitOfMeasures == null)
        {
            _logger.LogWarning("Ingredient list is null for user {UserId}", userId);

            return false;
        }

        try
        {
            var existingList = await _unitOfWork.Repository<ShoppingList, Guid>()
                .GetSingleOrDefaultAsync(sl => sl.UserId == userId);
            if (existingList == null)
            {
                var insertSucceeded = await _unitOfWork.Repository<ShoppingList, Guid>()
                    .InsertAsync(new ShoppingList { UserId = userId });

                if (!insertSucceeded)
                {
                    _logger.LogWarning("Failed to create new shopping list for user {UserId}", userId);
                    
                    return false;
                }

                await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "Create shopping list");

                existingList = await _unitOfWork.Repository<ShoppingList, Guid>()
                    .GetSingleOrDefaultAsync(sl => sl.UserId == userId);

                if (existingList == null)
                {
                    _logger.LogError("Failed to reload newly created shopping list for user {UserId}", userId);
                    
                    return false;
                }
            }

            var shoppingListId = existingList.Id;

            var existingIngredients = 
                (await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>()
                    .GetWhereAsync(sli => sli.ShoppingListId == shoppingListId))
                .GroupBy(x => x.IngredientId)
                .Select(g => g.FirstOrDefault())
                .Where(x => x != null)
                .ToDictionary(x => x!.IngredientId, x => x!);

            foreach (var ingredientDto in newShoppingList.IngredientUnitOfMeasures)
            {
                if (ingredientDto?.Ingredient == null)
                {
                    _logger.LogWarning("IngredientDTO is null in shopping list for user {UserId}", userId);
                    
                    continue;
                }

                var ingredientEntity = await _unitOfWork
                    .Repository<Ingredient, Guid>()
                    .GetSingleOrDefaultAsync(i => i.Name == ingredientDto.Ingredient.Name);

                var uomEntity = await _unitOfWork
                    .Repository<UnitOfMeasure, Guid>()
                    .GetSingleOrDefaultAsync(uom => uom.Name == ingredientDto.UnitOfMeasure);

                if (ingredientEntity == null || uomEntity == null)
                {
                    _logger.LogWarning("Ingredient {Ingredient} or UoM {Uom} not found",
                        ingredientDto.Ingredient.Name, ingredientDto.UnitOfMeasure);
                    continue;
                }

                if (existingIngredients.TryGetValue(ingredientEntity.Id, out var existingSli))
                {
                    existingSli.Amount += ingredientDto.Amount;
                    
                    await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>().UpdateAsync(existingSli);
                }
                else
                {
                    await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>()
                        .InsertAsync(new ShoppingListIngredient
                        {
                            IngredientId = ingredientEntity.Id,
                            ShoppingListId = shoppingListId,
                            Amount = ingredientDto.Amount,
                            UnitOfMeasureId = uomEntity.Id
                        });
                }
            }

            var result = await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "Update shopping list");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Exception occurred while creating/updating shopping list for user {UserId}", userId);
           
            return false;
        }
    }

    public async Task<ShoppingListDTO?> UpdateShoppingListAsync(ShoppingListDTO? updatedShoppingList, Guid userId)
    {
        if (updatedShoppingList == null)
        {
            _logger.LogWarning("ShoppingListDTO is null for user {UserId}", userId);
            
            return null;
        }

        try
        {
            var shoppingList = await GetShoppingListEntityByUserIdAsync(userId);
            
            var sliRepo = _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>();
            var existingItems = await sliRepo.GetWhereAsync(sli => sli.ShoppingListId == shoppingList.Id);

            var existingByIngredientId = existingItems
                .ToDictionary(x => x.IngredientId, x => x);

            var ingredientNames = updatedShoppingList.IngredientUnitOfMeasures
                .Where(i => i.Ingredient != null)
                .Select(i => i.Ingredient!.Name)
                .Distinct()
                .ToList();

            var uomNames = updatedShoppingList.IngredientUnitOfMeasures
                .Where(i => !string.IsNullOrWhiteSpace(i.UnitOfMeasure))
                .Select(i => i.UnitOfMeasure)
                .Distinct()
                .ToList();

            var ingredients = await _unitOfWork.Repository<Ingredient, Guid>()
                .GetWhereAsync(i => ingredientNames.Contains(i.Name));
            var uoms = await _unitOfWork.Repository<UnitOfMeasure, Guid>()
                .GetWhereAsync(u => uomNames.Contains(u.Name));

            var ingredientByName = ingredients.ToDictionary(x => x.Name, x => x);
            var uomByName = uoms.ToLookup(x => x.Name, x => x);

            var incomingIngredientIds = new HashSet<Guid>();

            foreach (var dto in updatedShoppingList.IngredientUnitOfMeasures)
            {
                if (dto.Ingredient == null)
                {
                    _logger.LogWarning("IngredientDTO is null in shopping list update for user {UserId}", userId);
                    
                    continue;
                }

                if (!ingredientByName.TryGetValue(dto.Ingredient.Name, out var ingredientEntity))
                {
                    _logger.LogWarning("Ingredient {IngredientName} not found in DB", dto.Ingredient.Name);
                    
                    continue;
                }
                
                var units = uomByName[dto.UnitOfMeasure];

                if (!units.Any())
                {
                    _logger.LogWarning("UnitOfMeasure {UomName} not found in DB", dto.UnitOfMeasure);
                    
                    continue;
                }
 
                var uomEntity = units.First();
                
                incomingIngredientIds.Add(ingredientEntity.Id);

                if (existingByIngredientId.TryGetValue(ingredientEntity.Id, out var existingSli))
                {
                    existingSli.Amount = dto.Amount;
                    existingSli.IsBought = dto.IsBought;

                    await sliRepo.UpdateAsync(existingSli);
                }
                else
                {
                    var newSli = new ShoppingListIngredient
                    {
                        ShoppingListId = shoppingList.Id,
                        IngredientId = ingredientEntity.Id,
                        UnitOfMeasureId = uomEntity.Id,
                        Amount = dto.Amount,
                        IsBought = dto.IsBought
                    };

                    await sliRepo.InsertAsync(newSli);
                }
            }

            foreach (var existing in existingByIngredientId.Values)
            {
                if (!incomingIngredientIds.Contains(existing.IngredientId))
                {
                    await sliRepo.DeleteAsync(existing);
                }
            }

            var saveResult = await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "Full update shopping list");
            if (!saveResult)
            {
                _logger.LogWarning("Failed to persist shopping list update for user {UserId}", userId);
                
                return null;
            }

            var result = await GetShoppingListAsync(userId);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred while fully updating shopping list for user {UserId}", userId);
            
            return null;
        }
    }

    public async Task<(byte[] buffer, string ContentType)?> GetShoppingListFileAsync(Guid userId)
    {
        var shoppingList = await GetShoppingListAsync(userId);

        if (shoppingList == null)
        {
            _logger.LogInformation("No shopping list found for user {UserId} when generating PDF", userId);
           
            return null;
        }

        var items = shoppingList.IngredientUnitOfMeasures;

        try
        {
            var buffer = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("Shopping List")
                                .FontSize(20)
                                .SemiBold()
                                .FontColor(Colors.Blue.Medium);

                            col.Item().Text($"Generated: {DateTime.UtcNow:yyyy-MM-dd HH:mm} UTC")
                                .FontSize(9)
                                .FontColor(Colors.Grey.Darken1);
                        });

                        row.ConstantItem(80)
                            .AlignRight()
                            .AlignMiddle()
                            .Text("Nutritional Recipe Book")
                            .FontSize(10)
                            .FontColor(Colors.Grey.Darken2);
                    });

                    page.Content().PaddingVertical(10).Column(column =>
                    {
                        column.Spacing(10);

                        if (!items.Any())
                        {
                            column.Item().Text("Your shopping list is currently empty.")
                                .FontSize(12)
                                .FontColor(Colors.Grey.Darken1);
                            return;
                        }

                        column.Item().Text("Items")
                            .FontSize(14)
                            .SemiBold()
                            .FontColor(Colors.Grey.Darken3);

                        column.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten2);

                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(25);
                                columns.RelativeColumn(4);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(HeaderCell).Text("#");
                                header.Cell().Element(HeaderCell).Text("Ingredient");
                                header.Cell().Element(HeaderCell).Text("Amount");
                                header.Cell().Element(HeaderCell).Text("Unit");
                                header.Cell().Element(HeaderCell).Text("Status");
                            });

                            var orderedItems = items
                                .Where(i => i?.Ingredient != null)
                                .OrderBy(i => i.IsBought)
                                .ThenBy(i => i.Ingredient!.Name)
                                .ToList();

                            var index = 1;
                            foreach (var item in orderedItems)
                            {
                                var background = item.IsBought ? Colors.Grey.Lighten4 : Colors.White;
                                var statusText = item.IsBought ? "Bought" : "Pending";

                                table.Cell().Element(c => BodyCell(c, background)).Text(index.ToString());
                                table.Cell().Element(c => BodyCell(c, background)).Text(item.Ingredient!.Name);
                                table.Cell().Element(c => BodyCell(c, background))
                                    .AlignRight().Text(item.Amount.ToString("0.##"));
                                table.Cell().Element(c => BodyCell(c, background)).
                                    Text(string.IsNullOrWhiteSpace(item.UnitOfMeasure) ? "-" : item.UnitOfMeasure);
                                table.Cell().Element(c => BodyCell(c, background)).Text(statusText);

                                index++;
                            }
                        });
                    });

                    page.Footer().Row(row =>
                    {
                        row.RelativeItem().AlignLeft().Text(x =>
                        {
                            x.Span("Nutritional Recipe Book")
                                .FontSize(9)
                                .FontColor(Colors.Grey.Darken1);
                        });

                        row.RelativeItem().AlignRight().Text(x =>
                        {
                            x.Span("Page ")
                                .FontSize(9)
                                .FontColor(Colors.Grey.Darken1);

                            x.CurrentPageNumber()
                                .FontSize(9)
                                .FontColor(Colors.Grey.Darken1);

                            x.Span(" / ")
                                .FontSize(9)
                                .FontColor(Colors.Grey.Darken1);

                            x.TotalPages()
                                .FontSize(9)
                                .FontColor(Colors.Grey.Darken1);
                        });
                    });

                });
            }).GeneratePdf();

            return (buffer, "application/pdf");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, 
                "Exception occurred while generating shopping list PDF for user {UserId}", userId);
            
            return null;
        }
    }

    private static IContainer HeaderCell(IContainer container)
    {
        return container
            .PaddingVertical(4)
            .PaddingHorizontal(6)
            .Background(Colors.Grey.Lighten3)
            .BorderBottom(1)
            .BorderColor(Colors.Grey.Lighten2)
            .DefaultTextStyle(x => x.SemiBold().FontSize(10).FontColor(Colors.Grey.Darken4));
    }

    private static IContainer BodyCell(IContainer container, string backgroundColor)
    {
        return container
            .PaddingVertical(3)
            .PaddingHorizontal(6)
            .Background(backgroundColor)
            .DefaultTextStyle(x => x.FontSize(10).FontColor(Colors.Grey.Darken3));
    }

    public async Task<bool> DeleteItemFromShoppingListAsync(Guid? ingredientId, Guid userId)
    {
        if (ingredientId == null)
        {
            _logger.LogWarning("IngredientId is null for user {UserId}", userId);
            
            return false;
        }

        try
        {
            var shoppingList = await GetShoppingListEntityByUserIdAsync(userId);
            var shoppingListIngredient = await GetShoppingListIngredientEntityAsync(shoppingList.Id, ingredientId.Value);
            
            await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>().DeleteAsync(shoppingListIngredient);
        }
        catch (Exception e)
        {
            _logger.LogError(e, 
                "Exception occurred while deleting item from shopping list for user {UserId}", userId);
            
            return false;
        }
        
        var result = await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "Deleting item from shopping list");

        return result;
    }

    public async Task<bool> ClearShoppingListAsync(Guid userId)
    {
        try
        {
            var shoppingList = await GetShoppingListEntityByUserIdAsync(userId);
            
            await _unitOfWork.Repository<ShoppingList, Guid>().DeleteAsync(shoppingList.Id);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occurred while clearing shopping list for user {UserId}", userId);
            
            return false;
        }
        
        var result = await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "Clear shopping list");

        return result;
    }

    public async Task<bool> UpdateItemIsBoughtStatusAsync(Guid userId, Guid? ingredientId, bool? isBought)
    {
        if(ingredientId == null || isBought == null)
        {
            _logger.LogWarning("IngredientId or isBought parameter is null for user {UserId}", userId);
            
            return false;
        }

        try
        {
            var shoppingList = await GetShoppingListEntityByUserIdAsync(userId);
            var shoppingListIngredient = await GetShoppingListIngredientEntityAsync(shoppingList.Id, ingredientId.Value);
            
            shoppingListIngredient.IsBought = isBought.Value;
            
            await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>().UpdateAsync(shoppingListIngredient);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occurred while update item status shopping list for user {UserId}", userId);
            
            return false;
        }
        
        var result = await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "Update item IsBought status");

        return result;
    }

    public async Task<bool> UpdateAllItemsIsBoughtStatusAsync(Guid userId, bool? isBought)
    {
        if(isBought == null)
        {
            _logger.LogWarning("isBought parameter is null for user {UserId}", userId);
            
            return false;
        }

        try
        {
            var shoppingList = await GetShoppingListEntityByUserIdAsync(userId);

            var shoppingListIngredients = await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>()
                .GetWhereAsync(sli => sli.ShoppingListId == shoppingList.Id);

            foreach (var sli in shoppingListIngredients)
            {
                sli.IsBought = isBought.Value;
                
                await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>().UpdateAsync(sli);
            }

            var result = await PersistenceHelper.TrySaveAsync(_unitOfWork, _logger, "Update all items IsBought status");

            return result;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occurred while updating all statuses in" +
                                " shopping list for user {UserId}", userId);
            
            return false;
        }
    }

    public async Task<ShoppingListDTO?> GetShoppingListAsync(Guid userId)
    {
        try
        {
            var shoppingList = await _unitOfWork.Repository<ShoppingList, Guid>()
                .GetSingleOrDefaultAsync(sl => sl.UserId == userId);

            if (shoppingList is null)
            {
                _logger.LogInformation("No shopping list found for user {UserId}", userId);
                return null;
            }

            var shoppingListIngredients = await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>()
                .GetWhereAsync(sli => sli.ShoppingListId == shoppingList.Id);

            IEnumerable<ShoppingListIngredient> listIngredients = shoppingListIngredients as ShoppingListIngredient[] 
                                                                  ?? shoppingListIngredients.ToArray();
            
            if (!listIngredients.Any())
            {
                return new ShoppingListDTO(shoppingList.Id, userId, Array.Empty<IngredientUnitOfMeasureDTO>());
            }

            var ingredientIds = listIngredients
                .Select(i => i.IngredientId)
                .Distinct()
                .ToList();
            var unitOfMeasureIds = listIngredients
                .Select(i => i.UnitOfMeasureId)
                .Distinct()
                .ToList();

            var ingredients = await _unitOfWork.Repository<Ingredient, Guid>()
                .GetWhereAsync(i => ingredientIds.Contains(i.Id));

            var uoms = await _unitOfWork.Repository<UnitOfMeasure, Guid>()
                .GetWhereAsync(u => unitOfMeasureIds.Contains(u.Id));

            var ingredientLookup = ingredients.ToDictionary(x => x.Id);
            var uomDictionary = uoms.ToDictionary(x => x.Id);

            var dtoList = new List<IngredientUnitOfMeasureDTO>();
            foreach (var sli in listIngredients)
            {
                if (!ingredientLookup.TryGetValue(sli.IngredientId, out var ing))
                {
                    _logger.LogWarning("Ingredient {IngredientId} referenced in shopping list " +
                                       "{ShoppingListId} not found in DB; skipping.",
                        sli.IngredientId, shoppingList.Id);
                    continue;
                }

                if (!uomDictionary.TryGetValue(sli.UnitOfMeasureId, out var uom))
                {
                    _logger.LogWarning("UnitOfMeasure {UnitOfMeasureId} referenced in shopping list" +
                                       " {ShoppingListId} not found in DB; skipping.",
                        sli.UnitOfMeasureId, shoppingList.Id);
                    continue;
                }

                dtoList.Add(new IngredientUnitOfMeasureDTO(
                    new IngredientDTO(ing.Id, ing.Name, ing.IsLiquid),
                    uom.Name,
                    sli.Amount,
                    sli.IsBought
                ));
            }

            var ingredientDtos = dtoList.ToArray();

            return new ShoppingListDTO(shoppingList.Id, userId, ingredientDtos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve shopping list for user {UserId}", userId);
            
            return null;
        }
    }
    
    private async Task<ShoppingList> GetShoppingListEntityByUserIdAsync(Guid userId)
    {
        var shoppingList = await _unitOfWork.Repository<ShoppingList, Guid>()
            .GetSingleOrDefaultAsync(sl => sl.UserId == userId);

        if (shoppingList == null)
        {
            _logger.LogWarning("No shopping list found for user {UserId}", userId);

            throw new InvalidOperationException($"No shopping list found for user {userId}");
        }
        
        return shoppingList;
    }
    
    private async Task<ShoppingListIngredient> GetShoppingListIngredientEntityAsync(Guid shoppingListId, Guid ingredientId)
    {
        var shoppingListIngredient = await _unitOfWork.Repository<ShoppingListIngredient, (Guid, Guid)>()
            .GetSingleOrDefaultAsync(sli => sli.ShoppingListId == shoppingListId && sli.IngredientId == ingredientId);

        if (shoppingListIngredient == null)
        {
            _logger.LogWarning("No shopping list ingredient found for ingredient " +
                               "{IngredientId} in shopping list {ShoppingListId}", ingredientId, shoppingListId);

            throw new InvalidOperationException(
                $"No shopping list ingredient found for ingredient {ingredientId} in shopping list {shoppingListId}");
        }
        
        return shoppingListIngredient;
    }
}