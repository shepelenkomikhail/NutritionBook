using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionalRecipeBook.Domain.Migrations
{
    /// <inheritdoc />
    public partial class NutrientIngredientupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "IngredientAmountPer100G",
                table: "NutrientIngredients",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 16, 23, 31, 107, DateTimeKind.Utc).AddTicks(3746));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 16, 13, 31, 107, DateTimeKind.Utc).AddTicks(3764));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 16, 3, 31, 107, DateTimeKind.Utc).AddTicks(3769));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 15, 53, 31, 107, DateTimeKind.Utc).AddTicks(3772));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 15, 43, 31, 107, DateTimeKind.Utc).AddTicks(3776));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 15, 33, 31, 107, DateTimeKind.Utc).AddTicks(3780));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 15, 23, 31, 107, DateTimeKind.Utc).AddTicks(3784));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 15, 13, 31, 107, DateTimeKind.Utc).AddTicks(3787));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 15, 3, 31, 107, DateTimeKind.Utc).AddTicks(3790));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 14, 53, 31, 107, DateTimeKind.Utc).AddTicks(3796));

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("30000000-0000-0000-0000-000000000001") },
                column: "IngredientAmountPer100G",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("30000000-0000-0000-0000-000000000002") },
                column: "IngredientAmountPer100G",
                value: 11m);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("30000000-0000-0000-0000-000000000003") },
                column: "IngredientAmountPer100G",
                value: 12m);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("30000000-0000-0000-0000-000000000004") },
                column: "IngredientAmountPer100G",
                value: 13m);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("30000000-0000-0000-0000-000000000005") },
                column: "IngredientAmountPer100G",
                value: 14m);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("30000000-0000-0000-0000-000000000006") },
                column: "IngredientAmountPer100G",
                value: 15m);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("30000000-0000-0000-0000-000000000007") },
                column: "IngredientAmountPer100G",
                value: 16m);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("30000000-0000-0000-0000-000000000008") },
                column: "IngredientAmountPer100G",
                value: 17m);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("30000000-0000-0000-0000-000000000009") },
                column: "IngredientAmountPer100G",
                value: 18m);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("30000000-0000-0000-0000-000000000010") },
                column: "IngredientAmountPer100G",
                value: 19m);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "c5f07ab0-4e65-46c6-986c-5ccbdbef1614");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "b8b83a98-b1da-4a3a-86e5-0b016db108b6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "70a447f9-31eb-4573-869d-a6ee14355b6e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "bb67cee7-2de6-4f4b-b17c-f5fe62fede31");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "c7c8eb46-7179-4d34-8ac6-678e718c2bd7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "c07306c4-69e3-45a5-8d8d-9c0ef0bc02dc");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "985fe937-254a-4cfa-aed3-689a55061327");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "5a37b036-b266-484f-b35a-63e58a80b658");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "bebebc06-00e9-4c3e-9272-d0764ef9ffe9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "b43a98d1-9c4e-488f-822a-4ac352161942");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IngredientAmountPer100G",
                table: "NutrientIngredients",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 9, 34, 27, 895, DateTimeKind.Utc).AddTicks(5592));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 9, 24, 27, 895, DateTimeKind.Utc).AddTicks(5607));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 9, 14, 27, 895, DateTimeKind.Utc).AddTicks(5613));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 9, 4, 27, 895, DateTimeKind.Utc).AddTicks(5618));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 8, 54, 27, 895, DateTimeKind.Utc).AddTicks(5623));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 8, 44, 27, 895, DateTimeKind.Utc).AddTicks(5629));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 8, 34, 27, 895, DateTimeKind.Utc).AddTicks(5634));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 8, 24, 27, 895, DateTimeKind.Utc).AddTicks(5640));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 8, 14, 27, 895, DateTimeKind.Utc).AddTicks(5645));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 5, 8, 4, 27, 895, DateTimeKind.Utc).AddTicks(5651));

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("30000000-0000-0000-0000-000000000001") },
                column: "IngredientAmountPer100G",
                value: 10);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("30000000-0000-0000-0000-000000000002") },
                column: "IngredientAmountPer100G",
                value: 11);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("30000000-0000-0000-0000-000000000003") },
                column: "IngredientAmountPer100G",
                value: 12);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("30000000-0000-0000-0000-000000000004") },
                column: "IngredientAmountPer100G",
                value: 13);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("30000000-0000-0000-0000-000000000005") },
                column: "IngredientAmountPer100G",
                value: 14);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("30000000-0000-0000-0000-000000000006") },
                column: "IngredientAmountPer100G",
                value: 15);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("30000000-0000-0000-0000-000000000007") },
                column: "IngredientAmountPer100G",
                value: 16);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("30000000-0000-0000-0000-000000000008") },
                column: "IngredientAmountPer100G",
                value: 17);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("30000000-0000-0000-0000-000000000009") },
                column: "IngredientAmountPer100G",
                value: 18);

            migrationBuilder.UpdateData(
                table: "NutrientIngredients",
                keyColumns: new[] { "IngredientId", "NutrientId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("30000000-0000-0000-0000-000000000010") },
                column: "IngredientAmountPer100G",
                value: 19);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "3aa2c8ed-a0f6-4d11-9975-2e14ef8d9b85");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "08ca3848-57d4-42ce-91a5-35f461ee65a4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "4f083dae-bb7e-4550-857e-7165ccdb5592");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "10c19165-e03b-42d5-ae01-7c0103968527");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "09c6ef8e-19e0-4395-b1b4-7c4e0e0c792b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "20619977-b29e-4ae5-8645-e4c24763c9c4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "c91206a4-c985-493d-a3de-acf411674768");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "e8ffbfb2-ea14-43c4-97dd-a9e9aefdf43b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "8eefed4a-1988-4ead-8741-735f059231ab");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "432b5468-6067-43d4-9063-8d1aca733a52");
        }
    }
}
