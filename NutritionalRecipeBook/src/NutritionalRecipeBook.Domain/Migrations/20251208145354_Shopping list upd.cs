using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionalRecipeBook.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Shoppinglistupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "ShoppingListIngredients");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitOfMeasureId",
                table: "ShoppingListIngredients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 43, 53, 583, DateTimeKind.Utc).AddTicks(2812));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 33, 53, 583, DateTimeKind.Utc).AddTicks(2829));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 23, 53, 583, DateTimeKind.Utc).AddTicks(2835));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 13, 53, 583, DateTimeKind.Utc).AddTicks(2841));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 14, 3, 53, 583, DateTimeKind.Utc).AddTicks(2847));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 53, 53, 583, DateTimeKind.Utc).AddTicks(2854));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 43, 53, 583, DateTimeKind.Utc).AddTicks(2861));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 33, 53, 583, DateTimeKind.Utc).AddTicks(2867));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 23, 53, 583, DateTimeKind.Utc).AddTicks(2872));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 13, 13, 53, 583, DateTimeKind.Utc).AddTicks(2880));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001") },
                column: "UnitOfMeasureId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000002") },
                column: "UnitOfMeasureId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000003") },
                column: "UnitOfMeasureId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("10000000-0000-0000-0000-000000000004") },
                column: "UnitOfMeasureId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("10000000-0000-0000-0000-000000000005") },
                column: "UnitOfMeasureId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("10000000-0000-0000-0000-000000000006") },
                column: "UnitOfMeasureId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("10000000-0000-0000-0000-000000000007") },
                column: "UnitOfMeasureId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("10000000-0000-0000-0000-000000000008") },
                column: "UnitOfMeasureId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("10000000-0000-0000-0000-000000000009") },
                column: "UnitOfMeasureId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("10000000-0000-0000-0000-000000000010") },
                column: "UnitOfMeasureId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "741b53ba-3b67-4d78-bfd3-4105e4c325ef");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "da78904e-bce9-4d76-88c4-005cb6813b7c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "fa8c5201-1560-47f9-9ee4-e867248686b1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "7138f340-076f-4aff-9ef2-2b3eea72cb36");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "8b16a970-3d50-489c-8738-731b3d7a17a0");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "a3dce2dd-25ee-4f01-b1ab-4bdadf8f6bce");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "73725e33-af00-4b83-87f7-969c16f3014a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "e274099a-19ce-430e-8afa-cedadb66197f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "a56983ae-1bc3-48d2-9290-12dd4f3286f3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "8c14456d-6ae3-410e-a764-e9fbccf133e1");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListIngredients_UnitOfMeasureId",
                table: "ShoppingListIngredients",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListIngredients_UnitOfMeasure_UnitOfMeasureId",
                table: "ShoppingListIngredients",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListIngredients_UnitOfMeasure_UnitOfMeasureId",
                table: "ShoppingListIngredients");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingListIngredients_UnitOfMeasureId",
                table: "ShoppingListIngredients");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "ShoppingListIngredients");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ShoppingLists",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "ShoppingListIngredients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 10, 50, 31, 640, DateTimeKind.Utc).AddTicks(8));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 10, 40, 31, 640, DateTimeKind.Utc).AddTicks(17));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 10, 30, 31, 640, DateTimeKind.Utc).AddTicks(19));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 10, 20, 31, 640, DateTimeKind.Utc).AddTicks(21));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 10, 10, 31, 640, DateTimeKind.Utc).AddTicks(54));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 10, 0, 31, 640, DateTimeKind.Utc).AddTicks(57));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 9, 50, 31, 640, DateTimeKind.Utc).AddTicks(60));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 9, 40, 31, 640, DateTimeKind.Utc).AddTicks(62));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 9, 30, 31, 640, DateTimeKind.Utc).AddTicks(63));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 9, 20, 31, 640, DateTimeKind.Utc).AddTicks(67));

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("10000000-0000-0000-0000-000000000001") },
                column: "Unit",
                value: "kg");

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("10000000-0000-0000-0000-000000000002") },
                column: "Unit",
                value: "L");

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("10000000-0000-0000-0000-000000000003") },
                column: "Unit",
                value: "kg");

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("10000000-0000-0000-0000-000000000004") },
                column: "Unit",
                value: "L");

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("10000000-0000-0000-0000-000000000005") },
                column: "Unit",
                value: "kg");

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("10000000-0000-0000-0000-000000000006") },
                column: "Unit",
                value: "L");

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("10000000-0000-0000-0000-000000000007") },
                column: "Unit",
                value: "kg");

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("10000000-0000-0000-0000-000000000008") },
                column: "Unit",
                value: "L");

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("10000000-0000-0000-0000-000000000009") },
                column: "Unit",
                value: "kg");

            migrationBuilder.UpdateData(
                table: "ShoppingListIngredients",
                keyColumns: new[] { "IngredientId", "ShoppingListId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("10000000-0000-0000-0000-000000000010") },
                column: "Unit",
                value: "L");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "Name",
                value: "UserName1's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "Name",
                value: "UserName2's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "Name",
                value: "UserName3's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "Name",
                value: "UserName4's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "Name",
                value: "UserName5's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "Name",
                value: "UserName6's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "Name",
                value: "UserName7's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                column: "Name",
                value: "UserName8's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                column: "Name",
                value: "UserName9's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                column: "Name",
                value: "UserName10's Shopping List");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "30b4923d-7331-4502-9eec-be1e3987c7b0");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "5ec29dc2-3d02-41a9-a211-b5474b9fc5a3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "e0451793-a05e-45f8-bcd6-f37382390bce");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "1f56cc8c-0611-4f4c-9a09-0198dd7bd997");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "ec061913-2c0e-4892-9a7f-97c8d9e56360");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "0d5bfe3d-c273-459f-a5f3-3dbe5cf38d70");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "40c04eb4-35aa-4d91-92cc-f55d83d55d3f");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "60d4cf36-6545-47ca-a212-fabd9a60bbdc");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "096cad25-a735-4ae2-825a-cba8dfcf9843");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "9fa44b04-0b2f-4b99-a362-b00a0090b752");
        }
    }
}
