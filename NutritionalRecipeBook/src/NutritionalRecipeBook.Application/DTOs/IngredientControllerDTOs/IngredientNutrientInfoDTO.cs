namespace NutritionalRecipeBook.Application.DTOs.IngredientControllerDTOs;

public record IngredientNutrientInfoDTO(
    IngredientNutrientApiDTO IngredientNutrientApiDTO,
    UnitOfMeasureDTO UnitOfMeasureDTO
);