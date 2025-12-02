using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NutritionalRecipeBook.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UserRecipeupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRecipes",
                table: "UserRecipes");

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { new Guid("40000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001") });

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { new Guid("40000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002") });

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { new Guid("40000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000003") });

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { new Guid("40000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000004") });

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { new Guid("40000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000005") });

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { new Guid("40000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000006") });

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { new Guid("40000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000007") });

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { new Guid("40000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008") });

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { new Guid("40000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000009") });

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumns: new[] { "RecipeId", "UserId" },
                keyValues: new object[] { new Guid("40000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000010") });

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "UserRecipes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "UserRecipes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRecipes",
                table: "UserRecipes",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 11, 43, 48, 406, DateTimeKind.Utc).AddTicks(88));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 11, 33, 48, 406, DateTimeKind.Utc).AddTicks(97));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 11, 23, 48, 406, DateTimeKind.Utc).AddTicks(99));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 11, 13, 48, 406, DateTimeKind.Utc).AddTicks(101));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 11, 3, 48, 406, DateTimeKind.Utc).AddTicks(103));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 10, 53, 48, 406, DateTimeKind.Utc).AddTicks(105));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 10, 43, 48, 406, DateTimeKind.Utc).AddTicks(107));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 10, 33, 48, 406, DateTimeKind.Utc).AddTicks(109));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 10, 23, 48, 406, DateTimeKind.Utc).AddTicks(111));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 12, 2, 10, 13, 48, 406, DateTimeKind.Utc).AddTicks(114));

            migrationBuilder.InsertData(
                table: "UserRecipes",
                columns: new[] { "Id", "IsFavourite", "IsOwner", "Rating", "RecipeId", "UserId" },
                values: new object[,]
                {
                    { new Guid("50000000-0000-0000-0000-000000000000"), true, true, 1, new Guid("40000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("50000000-0000-0000-0000-000000000001"), false, false, 2, new Guid("40000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("50000000-0000-0000-0000-000000000002"), false, true, 3, new Guid("40000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("50000000-0000-0000-0000-000000000003"), true, false, 4, new Guid("40000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("50000000-0000-0000-0000-000000000004"), false, true, 5, new Guid("40000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("50000000-0000-0000-0000-000000000005"), false, false, 1, new Guid("40000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("50000000-0000-0000-0000-000000000006"), true, true, 2, new Guid("40000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("50000000-0000-0000-0000-000000000007"), false, false, 3, new Guid("40000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("50000000-0000-0000-0000-000000000008"), false, true, 4, new Guid("40000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("50000000-0000-0000-0000-000000000009"), true, false, 5, new Guid("40000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000010") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "721b821d-023b-467e-975b-83fd7fcf94c6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "4a8b2148-b5b3-43d6-96a1-97486ca1fc01");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "996b91ed-4598-4539-bdce-e78359bb9d10");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "949b54a4-1e3e-4768-99b9-a4cfd1fa2cae");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "6ee08df9-a3ba-4d34-8a7c-1083ea8f3073");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "52e2da30-5dd2-4159-b550-13ad8432e884");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "68a56a9d-dc4e-4bf8-a0d4-bf787c5e93b6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "63d85733-ee20-4b37-8a6f-06fd015b9b4e");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "fa62e378-141d-4e1f-8c5c-b1862e4a89e0");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "e4e85c61-1b4b-46cb-8165-384ec0188cc7");

            migrationBuilder.CreateIndex(
                name: "IX_UserRecipes_UserId",
                table: "UserRecipes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRecipes",
                table: "UserRecipes");

            migrationBuilder.DropIndex(
                name: "IX_UserRecipes_UserId",
                table: "UserRecipes");

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000000"));

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "UserRecipes",
                keyColumn: "Id",
                keyColumnType: "uniqueidentifier",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserRecipes");

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "UserRecipes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRecipes",
                table: "UserRecipes",
                columns: new[] { "UserId", "RecipeId" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 20, 14, 20, 15, 517, DateTimeKind.Utc).AddTicks(6855));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 20, 14, 10, 15, 517, DateTimeKind.Utc).AddTicks(6872));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 20, 14, 0, 15, 517, DateTimeKind.Utc).AddTicks(6878));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 20, 13, 50, 15, 517, DateTimeKind.Utc).AddTicks(6884));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 20, 13, 40, 15, 517, DateTimeKind.Utc).AddTicks(6890));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 20, 13, 30, 15, 517, DateTimeKind.Utc).AddTicks(6897));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 20, 13, 20, 15, 517, DateTimeKind.Utc).AddTicks(6903));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 20, 13, 10, 15, 517, DateTimeKind.Utc).AddTicks(6908));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 20, 13, 0, 15, 517, DateTimeKind.Utc).AddTicks(6967));

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 20, 12, 50, 15, 517, DateTimeKind.Utc).AddTicks(6977));

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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "ConcurrencyStamp",
                value: "e3b6e3cb-0594-4a19-b076-d52be2071db9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                column: "ConcurrencyStamp",
                value: "47306fb7-7a0b-498c-a171-753ec1ea7d07");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                column: "ConcurrencyStamp",
                value: "47053a59-c38a-49b5-8397-1fd4e60615ba");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                column: "ConcurrencyStamp",
                value: "1f9d38c3-75fa-4a76-be25-4ed372dcfbff");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                column: "ConcurrencyStamp",
                value: "d3866fe0-361d-4d64-90b5-e329cc343113");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                column: "ConcurrencyStamp",
                value: "fb313e47-9b63-473d-89ea-5ad9de979287");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                column: "ConcurrencyStamp",
                value: "0b110038-3d2e-45c7-a298-011792156ebc");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                column: "ConcurrencyStamp",
                value: "b8db19e1-1614-4b0b-bf01-95103cc0ec6a");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                column: "ConcurrencyStamp",
                value: "d6caca66-c863-4adc-99b2-03af11c27a5b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                column: "ConcurrencyStamp",
                value: "eaf0b24c-454b-4cc7-973b-52b2b37925a3");
        }
    }
}
