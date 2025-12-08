using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionalRecipeBook.Domain.Migrations
{
    /// <inheritdoc />
    public partial class Moveratingtocommententity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserRecipes");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2025, 12, 8, 10, 50, 31, 640, DateTimeKind.Utc).AddTicks(8), 2 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2025, 12, 8, 10, 40, 31, 640, DateTimeKind.Utc).AddTicks(17), 3 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2025, 12, 8, 10, 30, 31, 640, DateTimeKind.Utc).AddTicks(19), 4 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2025, 12, 8, 10, 20, 31, 640, DateTimeKind.Utc).AddTicks(21), 5 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2025, 12, 8, 10, 10, 31, 640, DateTimeKind.Utc).AddTicks(54), 1 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2025, 12, 8, 10, 0, 31, 640, DateTimeKind.Utc).AddTicks(57), 2 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2025, 12, 8, 9, 50, 31, 640, DateTimeKind.Utc).AddTicks(60), 3 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2025, 12, 8, 9, 40, 31, 640, DateTimeKind.Utc).AddTicks(62), 4 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2025, 12, 8, 9, 30, 31, 640, DateTimeKind.Utc).AddTicks(63), 5 });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                columns: new[] { "CreatedAt", "Rating" },
                values: new object[] { new DateTime(2025, 12, 8, 9, 20, 31, 640, DateTimeKind.Utc).AddTicks(67), 1 });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "UserRecipes",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 9, 52, 37, 201, DateTimeKind.Utc).AddTicks(3049));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 9, 42, 37, 201, DateTimeKind.Utc).AddTicks(3060));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 9, 32, 37, 201, DateTimeKind.Utc).AddTicks(3062));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 9, 22, 37, 201, DateTimeKind.Utc).AddTicks(3064));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 9, 12, 37, 201, DateTimeKind.Utc).AddTicks(3066));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 9, 2, 37, 201, DateTimeKind.Utc).AddTicks(3068));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 8, 52, 37, 201, DateTimeKind.Utc).AddTicks(3112));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 8, 42, 37, 201, DateTimeKind.Utc).AddTicks(3114));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 8, 32, 37, 201, DateTimeKind.Utc).AddTicks(3116));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 8, 8, 22, 37, 201, DateTimeKind.Utc).AddTicks(3119));

            migrationBuilder.UpdateData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000000"),
                column: "Rating",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000001"),
                column: "Rating",
                value: 2);

            migrationBuilder.UpdateData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000002"),
                column: "Rating",
                value: 3);

            migrationBuilder.UpdateData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000003"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000004"),
                column: "Rating",
                value: 5);

            migrationBuilder.UpdateData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000005"),
                column: "Rating",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000006"),
                column: "Rating",
                value: 2);

            migrationBuilder.UpdateData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000007"),
                column: "Rating",
                value: 3);

            migrationBuilder.UpdateData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000008"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyValue: new Guid("70000000-0000-0000-0000-000000000009"),
                column: "Rating",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "88c11d77-60e8-4658-9da4-da8d3ad6b975");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "b08776e5-d0d1-496d-8d15-ffcc8b995bd3");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "9d7abf02-bfba-46d9-828b-93004e3e7074");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "af272fe2-c229-4f29-a76c-07a289cbd34a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "47e5f851-f4d4-4b85-bb4a-dd883108a687");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "66c4e151-048c-4bcf-b9ca-c541689b96bc");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "58fe3604-82e2-449c-8e75-959f0ad6aec8");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "4a504007-e395-44fc-a1e9-26c3a7a13065");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "1c290d38-2b9d-4c29-9141-609a9c9bb421");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "0a5a33f6-d32b-4151-9957-0b398cc8459f");
        }
    }
}
