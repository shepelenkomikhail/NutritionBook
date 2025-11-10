namespace NutritionalRecipeBook.Domain.Entities
{
    public class BaseEntity<TId>: IBaseEntity<TId>
    {
        public TId Id { get; set; }
    }
}