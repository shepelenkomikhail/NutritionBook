using System.Text.Json.Serialization;
using NutritionalRecipeBook.NutritionWebApi.Models;

namespace NutritionalRecipeBook.NutritionWebApi.Context;

[JsonSerializable(typeof(Nutrient[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}