using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionalRecipeBook.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitofmeasure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "RecipeIngredients");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitOfMeasureId",
                table: "RecipeIngredients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "UnitOfMeasure",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasure", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 4, 14, 18, 15, 232, DateTimeKind.Utc).AddTicks(2855));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 4, 14, 8, 15, 232, DateTimeKind.Utc).AddTicks(2917));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 4, 13, 58, 15, 232, DateTimeKind.Utc).AddTicks(2923));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 4, 13, 48, 15, 232, DateTimeKind.Utc).AddTicks(2929));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 4, 13, 38, 15, 232, DateTimeKind.Utc).AddTicks(2935));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 4, 13, 28, 15, 232, DateTimeKind.Utc).AddTicks(2941));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 4, 13, 18, 15, 232, DateTimeKind.Utc).AddTicks(2947));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 4, 13, 8, 15, 232, DateTimeKind.Utc).AddTicks(2953));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 4, 12, 58, 15, 232, DateTimeKind.Utc).AddTicks(2958));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 4, 12, 48, 15, 232, DateTimeKind.Utc).AddTicks(2965));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000001") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000001") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000001") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000002") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000002") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000002") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000003") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000003") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000003") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000004") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000004") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000004") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000005") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000005") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000005") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000006") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000006") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000006") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000007") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000007") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000007") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000008") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000008") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000008") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000009") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000009") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000009") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000010") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000010") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000010") },
                column: "UnitOfMeasureId",
                value: new Guid("80000000-0000-0000-0000-000000000003"));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "646e7aa5-d7b3-4208-b0b3-3ba3940019b3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "578269df-1348-4e8e-a7f3-d5aebfad4948");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "edbe35e4-bbc4-4093-942b-16a0551562a1");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "ef93d81d-24a3-4ce6-a89a-9cc4bf1daad7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "96598ac0-7159-473a-b69f-4f831c3ac23d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "55ae4c29-eec4-4f97-be24-a9293af66eab");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "6f33e979-0593-4537-b185-728ee0f01a54");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "87965c0b-1e77-4288-bdce-e9bc98c5a5da");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "0fde4216-8a7b-4523-a7c4-24181742ef82");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "8299a715-3f5d-4575-8233-38fb5a9c79ba");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_UnitOfMeasureId",
                table: "RecipeIngredients",
                column: "UnitOfMeasureId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipeIngredients_UnitOfMeasure_UnitOfMeasureId",
                table: "RecipeIngredients",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipeIngredients_UnitOfMeasure_UnitOfMeasureId",
                table: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "UnitOfMeasure");

            migrationBuilder.DropIndex(
                name: "IX_RecipeIngredients_UnitOfMeasureId",
                table: "RecipeIngredients");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "RecipeIngredients");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "RecipeIngredients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 13, 17, 17, 474, DateTimeKind.Utc).AddTicks(7748));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 13, 7, 17, 474, DateTimeKind.Utc).AddTicks(7763));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 12, 57, 17, 474, DateTimeKind.Utc).AddTicks(7766));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 12, 47, 17, 474, DateTimeKind.Utc).AddTicks(7768));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 12, 37, 17, 474, DateTimeKind.Utc).AddTicks(7770));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 12, 27, 17, 474, DateTimeKind.Utc).AddTicks(7773));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 12, 17, 17, 474, DateTimeKind.Utc).AddTicks(7776));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 12, 7, 17, 474, DateTimeKind.Utc).AddTicks(7777));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 11, 57, 17, 474, DateTimeKind.Utc).AddTicks(7779));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 11, 47, 17, 474, DateTimeKind.Utc).AddTicks(7783));

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000001") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000001") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000001") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000002") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000002") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000002") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000003"), new Guid("40000000-0000-0000-0000-000000000003") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000003") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000003") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000004"), new Guid("40000000-0000-0000-0000-000000000004") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000004") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000004") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000005"), new Guid("40000000-0000-0000-0000-000000000005") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000005") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000005") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000006"), new Guid("40000000-0000-0000-0000-000000000006") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000006") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000006") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000007"), new Guid("40000000-0000-0000-0000-000000000007") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000007") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000007") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000008"), new Guid("40000000-0000-0000-0000-000000000008") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000008") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000008") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000009") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000009"), new Guid("40000000-0000-0000-0000-000000000009") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000009") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000001"), new Guid("40000000-0000-0000-0000-000000000010") },
                column: "Unit",
                value: "g");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000002"), new Guid("40000000-0000-0000-0000-000000000010") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "RecipeIngredients",
                keyColumns: new[] { "IngredientId", "RecipeId" },
                keyValues: new object[] { new Guid("20000000-0000-0000-0000-000000000010"), new Guid("40000000-0000-0000-0000-000000000010") },
                column: "Unit",
                value: "ml");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "ea2be3dc-b123-47a2-92e1-88f56ea9c357");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "3542b7d0-f735-48e7-9c61-fc0e43a67236");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "7349c1f9-0c97-46ae-b129-182fb0c4aed5");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "fc7f2a80-e49f-42b9-abef-82b17f46cd79");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "4ddbb441-7af7-4e54-84aa-e3332469a116");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "c615c49b-5efb-4de1-8a58-6117dbeb5544");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "1508be45-0e17-4f62-b4a5-cc09941a3dba");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "08232b12-86ee-4d05-baed-d62ae06ed5d7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "1729fcb2-eaa5-4ad4-adae-02ea442b5c1c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "0c540f24-68e4-457c-ae08-3b277e5c0ccb");
        }
    }
}
