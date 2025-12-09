using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionalRecipeBook.Domain.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingListIngredientupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBought",
                table: "ShoppingListIngredients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 8, 8, 15, 854, DateTimeKind.Utc).AddTicks(2703));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 7, 58, 15, 854, DateTimeKind.Utc).AddTicks(2716));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 7, 48, 15, 854, DateTimeKind.Utc).AddTicks(2721));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 7, 38, 15, 854, DateTimeKind.Utc).AddTicks(2726));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 7, 28, 15, 854, DateTimeKind.Utc).AddTicks(2731));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 7, 18, 15, 854, DateTimeKind.Utc).AddTicks(2736));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 7, 8, 15, 854, DateTimeKind.Utc).AddTicks(2741));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 6, 58, 15, 854, DateTimeKind.Utc).AddTicks(2746));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 6, 48, 15, 854, DateTimeKind.Utc).AddTicks(2751));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 9, 6, 38, 15, 854, DateTimeKind.Utc).AddTicks(2757));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001") },
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000002") },
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000003") },
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("10000000-0000-0000-0000-000000000004") },
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("10000000-0000-0000-0000-000000000005") },
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("10000000-0000-0000-0000-000000000006") },
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("10000000-0000-0000-0000-000000000007") },
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("10000000-0000-0000-0000-000000000008") },
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("10000000-0000-0000-0000-000000000009") },
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("10000000-0000-0000-0000-000000000010") },
                column: "IsBought",
                value: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "8a726c2b-e230-4a60-993f-a2dd2b2483d9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "af276e50-535d-435f-9e23-3fd05b6bd1ef");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "61f4162d-df52-45bb-99d8-4c63d1fd3cde");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "638bb929-dddc-40ff-b9ef-28e2445a3ef3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "f0904a85-c4b9-4dea-b520-a658748c445b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "62da842b-cf06-4919-837e-464bb0c51e8d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "09b7b1e0-62df-4efd-a217-fa55f43aa919");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "13dc2c00-b9cd-4c75-8d1b-dc8ed28a4f36");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "ad8b6fac-2bdf-4a6d-b623-174278d8b1ef");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "642fffed-c7be-45bf-83d7-9c385b13c452");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBought",
                table: "ShoppingListIngredients");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 50, 18, 418, DateTimeKind.Utc).AddTicks(6173));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 40, 18, 418, DateTimeKind.Utc).AddTicks(6185));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 30, 18, 418, DateTimeKind.Utc).AddTicks(6187));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 20, 18, 418, DateTimeKind.Utc).AddTicks(6189));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 10, 18, 418, DateTimeKind.Utc).AddTicks(6191));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 0, 18, 418, DateTimeKind.Utc).AddTicks(6194));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 50, 18, 418, DateTimeKind.Utc).AddTicks(6196));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 40, 18, 418, DateTimeKind.Utc).AddTicks(6198));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 30, 18, 418, DateTimeKind.Utc).AddTicks(6200));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 20, 18, 418, DateTimeKind.Utc).AddTicks(6203));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "f3182e5f-dddb-43dc-a96a-f7ca013887d5");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "31026490-9ba2-40f6-bc13-687e6fbaf058");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "d6068c9f-dc81-4ca7-91ab-5f841623a6f0");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "9d865f07-f61f-4d82-9f90-eaa12a04dd8c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "6199b8b0-eb0c-4734-aa67-e72c1473e7fb");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "f1ead1f1-3c6f-4366-bb60-a7ad52539828");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "d5c49e1f-05dd-4af8-adbf-0c255b8938f3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "185a1b8b-6143-45c8-a100-096b0ade111b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "7be30b1a-ff1c-41a8-b0e3-c6dfb5106e09");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "3290bd50-af3b-44ee-b871-09f9b64de538");
        }
    }
}
