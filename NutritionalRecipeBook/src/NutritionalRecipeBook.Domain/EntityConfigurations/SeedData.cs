using NutritionalRecipeBook.Domain.Entities;
using NutritionalRecipeBook.Domain.ConnectionTables;

public static class SeedData
{
    // --- USERS ---
    public static List<User> GetUsers()
    {
        var users = new List<User>();
        for (int i = 1; i <= 10; i++)
        {
            users.Add(new User
            {
                Id = Guid.Parse($"00000000-0000-0000-0000-0000000000{i:D2}"),
                Username = $"User{i}"
            });
        }
        return users;
    }

    // --- SHOPPING LISTS ---
    public static List<ShoppingList> GetShoppingLists(List<User> users)
    {
        var lists = new List<ShoppingList>();
        foreach (var user in users)
        {
            lists.Add(new ShoppingList
            {
                Id = Guid.Parse($"10000000-0000-0000-0000-0000000000{users.IndexOf(user) + 1:D2}"),
                Name = $"{user.Username}'s Shopping List",
                UserId = user.Id
            });
        }
        return lists;
    }

    // --- INGREDIENTS ---
    public static List<Ingredient> GetIngredients()
    {
        var ingredients = new List<Ingredient>();
        for (int i = 1; i <= 10; i++)
        {
            ingredients.Add(new Ingredient
            {
                Id = Guid.Parse($"20000000-0000-0000-0000-0000000000{i:D2}"),
                Name = $"Ingredient {i}",
                IsLiquid = i % 2 == 0
            });
        }
        return ingredients;
    }

    // --- NUTRIENTS ---
    public static List<Nutrient> GetNutrients()
    {
        string[] names = { "Protein", "Carbohydrate", "Fat", "Fiber", "Sugar", "Sodium", "Vitamin C", "Iron", "Calcium", "Potassium" };
        var nutrients = new List<Nutrient>();
        for (int i = 0; i < names.Length; i++)
        {
            nutrients.Add(new Nutrient
            {
                Id = Guid.Parse($"30000000-0000-0000-0000-0000000000{i + 1:D2}"),
                Name = names[i],
                Unit = i < 5 ? "g" : "mg"
            });
        }
        return nutrients;
    }

    // --- RECIPES ---
    public static List<Recipe> GetRecipes()
    {
        var recipes = new List<Recipe>();
        for (int i = 1; i <= 10; i++)
        {
            recipes.Add(new Recipe
            {
                Id = Guid.Parse($"40000000-0000-0000-0000-0000000000{i:D2}"),
                Name = $"Recipe {i}",
                Description = $"Description for recipe {i}.",
                Instructions = $"Instructions for recipe {i}.",
                CookingTimeInMin = 15 + i * 5,
                Servings = 2 + (i % 3)
            });
        }
        return recipes;
    }

    // --- COMMENTS ---
    public static List<Comment> GetComments(List<User> users, List<Recipe> recipes)
    {
        var comments = new List<Comment>();
        for (int i = 1; i <= 10; i++)
        {
            var user = users[(i - 1) % users.Count];
            var recipe = recipes[(i - 1) % recipes.Count];
            comments.Add(new Comment
            {
                Id = Guid.Parse($"50000000-0000-0000-0000-0000000000{i:D2}"),
                Content = $"Comment {i} by {user.Username} on {recipe.Name}",
                CreatedAt = DateTime.UtcNow.AddMinutes(-i * 10),
                UserId = user.Id,
                RecipeId = recipe.Id
            });
        }
        return comments;
    }

    // --- RECIPE INGREDIENTS ---
    public static List<RecipeIngredient> GetRecipeIngredients(List<Recipe> recipes, List<Ingredient> ingredients)
    {
        var list = new List<RecipeIngredient>();
        for (int i = 0; i < recipes.Count; i++)
        {
            var recipe = recipes[i];
            for (int j = 0; j < 3; j++)
            {
                var ingredient = ingredients[(i + j) % ingredients.Count];
                list.Add(new RecipeIngredient
                {
                    RecipeId = recipe.Id,
                    IngredientId = ingredient.Id,
                    Amount = 50 + j * 25,
                    Unit = ingredient.IsLiquid ? "ml" : "g"
                });
            }
        }
        return list;
    }

    // --- NUTRIENT INGREDIENTS ---
    public static List<NutrientIngredient> GetNutrientIngredients(List<Nutrient> nutrients, List<Ingredient> ingredients)
    {
        var list = new List<NutrientIngredient>();
        for (int i = 0; i < ingredients.Count; i++)
        {
            var ingredient = ingredients[i];
            var nutrient = nutrients[i % nutrients.Count];
            list.Add(new NutrientIngredient
            {
                NutrientId = nutrient.Id,
                IngredientId = ingredient.Id,
                IngredientAmountPer100G = 10 + i
            });
        }
        return list;
    }

    // --- SHOPPING LIST INGREDIENTS ---
    public static List<ShoppingListIngredient> GetShoppingListIngredients(List<ShoppingList> shoppingLists, List<Ingredient> ingredients)
    {
        var list = new List<ShoppingListIngredient>();
        for (int i = 0; i < shoppingLists.Count; i++)
        {
            var sl = shoppingLists[i];
            var ingredient = ingredients[i % ingredients.Count];
            list.Add(new ShoppingListIngredient
            {
                ShoppingListId = sl.Id,
                IngredientId = ingredient.Id,
                Amount = 1 + i,
                Unit = ingredient.IsLiquid ? "L" : "kg"
            });
        }
        return list;
    }

    // --- USER RECIPES ---
    public static List<UserRecipe> GetUserRecipes(List<User> users, List<Recipe> recipes)
    {
        var list = new List<UserRecipe>();
        for (int i = 0; i < users.Count; i++)
        {
            var user = users[i];
            var recipe = recipes[i % recipes.Count];
            list.Add(new UserRecipe
            {
                UserId = user.Id,
                RecipeId = recipe.Id,
                Rating = (i % 5) + 1,
                IsOwner = i % 2 == 0,
                IsFavourite = i % 3 == 0
            });
        }
        return list;
    }
}