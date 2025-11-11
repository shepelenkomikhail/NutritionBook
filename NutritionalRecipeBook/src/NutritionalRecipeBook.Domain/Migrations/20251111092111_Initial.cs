using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NutritionalRecipeBook.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsLiquid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Nutrients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nutrients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: false),
                    CookingTimeInMin = table.Column<int>(type: "int", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NutrientIngredients",
                columns: table => new
                {
                    NutrientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientAmountPer100G = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutrientIngredients", x => new { x.NutrientId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_NutrientIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NutrientIngredients_Nutrients_NutrientId",
                        column: x => x.NutrientId,
                        principalTable: "Nutrients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => new { x.RecipeId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRecipes",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    IsOwner = table.Column<bool>(type: "bit", nullable: false),
                    IsFavourite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRecipes", x => new { x.UserId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_UserRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRecipes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListIngredients",
                columns: table => new
                {
                    ShoppingListId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListIngredients", x => new { x.ShoppingListId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_ShoppingListIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingListIngredients_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "IsLiquid", "Name" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), false, "Ingredient 1" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), true, "Ingredient 2" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), false, "Ingredient 3" },
                    { new Guid("20000000-0000-0000-0000-000000000004"), true, "Ingredient 4" },
                    { new Guid("20000000-0000-0000-0000-000000000005"), false, "Ingredient 5" },
                    { new Guid("20000000-0000-0000-0000-000000000006"), true, "Ingredient 6" },
                    { new Guid("20000000-0000-0000-0000-000000000007"), false, "Ingredient 7" },
                    { new Guid("20000000-0000-0000-0000-000000000008"), true, "Ingredient 8" },
                    { new Guid("20000000-0000-0000-0000-000000000009"), false, "Ingredient 9" },
                    { new Guid("20000000-0000-0000-0000-000000000010"), true, "Ingredient 10" }
                });

            migrationBuilder.InsertData(
                table: "Nutrients",
                columns: new[] { "Id", "Name", "Unit" },
                values: new object[,]
                {
                    { new Guid("30000000-0000-0000-0000-000000000001"), "Protein", "g" },
                    { new Guid("30000000-0000-0000-0000-000000000002"), "Carbohydrate", "g" },
                    { new Guid("30000000-0000-0000-0000-000000000003"), "Fat", "g" },
                    { new Guid("30000000-0000-0000-0000-000000000004"), "Fiber", "g" },
                    { new Guid("30000000-0000-0000-0000-000000000005"), "Sugar", "g" },
                    { new Guid("30000000-0000-0000-0000-000000000006"), "Sodium", "mg" },
                    { new Guid("30000000-0000-0000-0000-000000000007"), "Vitamin C", "mg" },
                    { new Guid("30000000-0000-0000-0000-000000000008"), "Iron", "mg" },
                    { new Guid("30000000-0000-0000-0000-000000000009"), "Calcium", "mg" },
                    { new Guid("30000000-0000-0000-0000-000000000010"), "Potassium", "mg" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "CookingTimeInMin", "Description", "Instructions", "Name", "Servings" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), 20, "Description for recipe 1.", "Instructions for recipe 1.", "Recipe 1", 3 },
                    { new Guid("40000000-0000-0000-0000-000000000002"), 25, "Description for recipe 2.", "Instructions for recipe 2.", "Recipe 2", 4 },
                    { new Guid("40000000-0000-0000-0000-000000000003"), 30, "Description for recipe 3.", "Instructions for recipe 3.", "Recipe 3", 2 },
                    { new Guid("40000000-0000-0000-0000-000000000004"), 35, "Description for recipe 4.", "Instructions for recipe 4.", "Recipe 4", 3 },
                    { new Guid("40000000-0000-0000-0000-000000000005"), 40, "Description for recipe 5.", "Instructions for recipe 5.", "Recipe 5", 4 },
                    { new Guid("40000000-0000-0000-0000-000000000006"), 45, "Description for recipe 6.", "Instructions for recipe 6.", "Recipe 6", 2 },
                    { new Guid("40000000-0000-0000-0000-000000000007"), 50, "Description for recipe 7.", "Instructions for recipe 7.", "Recipe 7", 3 },
                    { new Guid("40000000-0000-0000-0000-000000000008"), 55, "Description for recipe 8.", "Instructions for recipe 8.", "Recipe 8", 4 },
                    { new Guid("40000000-0000-0000-0000-000000000009"), 60, "Description for recipe 9.", "Instructions for recipe 9.", "Recipe 9", 2 },
                    { new Guid("40000000-0000-0000-0000-000000000010"), 65, "Description for recipe 10.", "Instructions for recipe 10.", "Recipe 10", 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Username" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "User1" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "User2" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "User3" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "User4" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "User5" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "User6" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "User7" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "User8" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "User9" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "User10" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedAt", "RecipeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000001"), "Comment 1 by User1 on Recipe 1", new DateTime(2025, 11, 11, 9, 11, 11, 371, DateTimeKind.Utc).AddTicks(9948), new Guid("40000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("50000000-0000-0000-0000-000000000002"), "Comment 2 by User2 on Recipe 2", new DateTime(2025, 11, 11, 9, 1, 11, 371, DateTimeKind.Utc).AddTicks(9965), new Guid("40000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("50000000-0000-0000-0000-000000000003"), "Comment 3 by User3 on Recipe 3", new DateTime(2025, 11, 11, 8, 51, 11, 371, DateTimeKind.Utc).AddTicks(9970), new Guid("40000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), "Comment 4 by User4 on Recipe 4", new DateTime(2025, 11, 11, 8, 41, 11, 371, DateTimeKind.Utc).AddTicks(9974), new Guid("40000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), "Comment 5 by User5 on Recipe 5", new DateTime(2025, 11, 11, 8, 31, 11, 371, DateTimeKind.Utc).AddTicks(9979), new Guid("40000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), "Comment 6 by User6 on Recipe 6", new DateTime(2025, 11, 11, 8, 21, 11, 371, DateTimeKind.Utc).AddTicks(9985), new Guid("40000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("50000000-0000-0000-0000-000000000007"), "Comment 7 by User7 on Recipe 7", new DateTime(2025, 11, 11, 8, 11, 11, 371, DateTimeKind.Utc).AddTicks(9990), new Guid("40000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("50000000-0000-0000-0000-000000000008"), "Comment 8 by User8 on Recipe 8", new DateTime(2025, 11, 11, 8, 1, 11, 371, DateTimeKind.Utc).AddTicks(9995), new Guid("40000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("50000000-0000-0000-0000-000000000009"), "Comment 9 by User9 on Recipe 9", new DateTime(2025, 11, 11, 7, 51, 11, 372, DateTimeKind.Utc), new Guid("40000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("50000000-0000-0000-0000-000000000010"), "Comment 10 by User10 on Recipe 10", new DateTime(2025, 11, 11, 7, 41, 11, 372, DateTimeKind.Utc).AddTicks(6), new Guid("40000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000010") }
                });

            migrationBuilder.InsertData(
                table: "NutrientIngredients",
                columns: new[] { "IngredientId", "NutrientId", "IngredientAmountPer100G" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("30000000-0000-0000-0000-000000000001"), 10 },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("30000000-0000-0000-0000-000000000002"), 11 },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("30000000-0000-0000-0000-000000000003"), 12 },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("30000000-0000-0000-0000-000000000004"), 13 },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("30000000-0000-0000-0000-000000000005"), 14 },
                    { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("30000000-0000-0000-0000-000000000006"), 15 },
                    { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("30000000-0000-0000-0000-000000000007"), 16 },
                    { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("30000000-0000-0000-0000-000000000008"), 17 },
                    { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("30000000-0000-0000-0000-000000000009"), 18 },
                    { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("30000000-0000-0000-0000-000000000010"), 19 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "IngredientId", "RecipeId", "Amount", "Unit" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000001"), 50m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000001"), 75m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000001"), 100m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000002"), 50m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000002"), 75m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000002"), 100m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000003"), 50m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000003"), 75m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000003"), 100m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000004"), 50m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000004"), 75m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000004"), 100m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000005"), 50m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000005"), 75m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000005"), 100m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000006"), 50m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000006"), 75m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000006"), 100m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000007"), 50m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000007"), 75m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000007"), 100m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000008"), 50m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000008"), 75m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000008"), 100m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000009"), 100m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000009"), 50m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000009"), 75m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000010"), 75m, "g" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000010"), 100m, "ml" },
                    { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000010"), 50m, "ml" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingLists",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "User1's Shopping List", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "User2's Shopping List", new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "User3's Shopping List", new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "User4's Shopping List", new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "User5's Shopping List", new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "User6's Shopping List", new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "User7's Shopping List", new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "User8's Shopping List", new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("10000000-0000-0000-0000-000000000009"), "User9's Shopping List", new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("10000000-0000-0000-0000-000000000010"), "User10's Shopping List", new Guid("00000000-0000-0000-0000-000000000010") }
                });

            migrationBuilder.InsertData(
                table: "UserRecipes",
                columns: new[] { "RecipeId", "UserId", "IsFavourite", "IsOwner", "Rating" },
                values: new object[,]
                {
                    { new Guid("40000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), true, true, 1 },
                    { new Guid("40000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), false, false, 2 },
                    { new Guid("40000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000003"), false, true, 3 },
                    { new Guid("40000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000004"), true, false, 4 },
                    { new Guid("40000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000005"), false, true, 5 },
                    { new Guid("40000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000006"), false, false, 1 },
                    { new Guid("40000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000007"), true, true, 2 },
                    { new Guid("40000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), false, false, 3 },
                    { new Guid("40000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000009"), false, true, 4 },
                    { new Guid("40000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000010"), true, false, 5 }
                });

            migrationBuilder.InsertData(
                table: "ShoppingListIngredients",
                columns: new[] { "IngredientId", "ShoppingListId", "Amount", "Unit" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001"), 1m, "kg" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000002"), 2m, "L" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000003"), 3m, "kg" },
                    { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("10000000-0000-0000-0000-000000000004"), 4m, "L" },
                    { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("10000000-0000-0000-0000-000000000005"), 5m, "kg" },
                    { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("10000000-0000-0000-0000-000000000006"), 6m, "L" },
                    { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("10000000-0000-0000-0000-000000000007"), 7m, "kg" },
                    { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("10000000-0000-0000-0000-000000000008"), 8m, "L" },
                    { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("10000000-0000-0000-0000-000000000009"), 9m, "kg" },
                    { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("10000000-0000-0000-0000-000000000010"), 10m, "L" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CreatedAt",
                table: "Comments",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_RecipeId",
                table: "Comments",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Name",
                table: "Ingredients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NutrientIngredients_IngredientId",
                table: "NutrientIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Nutrients_Name",
                table: "Nutrients",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_IngredientId",
                table: "RecipeIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListIngredients_IngredientId",
                table: "ShoppingListIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_UserId",
                table: "ShoppingLists",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRecipes_RecipeId",
                table: "UserRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "NutrientIngredients");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "ShoppingListIngredients");

            migrationBuilder.DropTable(
                name: "UserRecipes");

            migrationBuilder.DropTable(
                name: "Nutrients");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
