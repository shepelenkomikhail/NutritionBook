namespace NutritionalRecipeBook.Domain.Entities;

public interface IBaseEntity<TId>
{
    TId Id { get; set; }
}