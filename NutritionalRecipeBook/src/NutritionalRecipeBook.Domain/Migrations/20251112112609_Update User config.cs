using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutritionalRecipeBook.Domain.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserconfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "Users",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 1 by UserName1 on Recipe 1", new DateTime(2025, 11, 12, 11, 16, 8, 730, DateTimeKind.Utc).AddTicks(8641) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 2 by UserName2 on Recipe 2", new DateTime(2025, 11, 12, 11, 6, 8, 730, DateTimeKind.Utc).AddTicks(8651) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 3 by UserName3 on Recipe 3", new DateTime(2025, 11, 12, 10, 56, 8, 730, DateTimeKind.Utc).AddTicks(8653) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 4 by UserName4 on Recipe 4", new DateTime(2025, 11, 12, 10, 46, 8, 730, DateTimeKind.Utc).AddTicks(8655) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 5 by UserName5 on Recipe 5", new DateTime(2025, 11, 12, 10, 36, 8, 730, DateTimeKind.Utc).AddTicks(8657) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 6 by UserName6 on Recipe 6", new DateTime(2025, 11, 12, 10, 26, 8, 730, DateTimeKind.Utc).AddTicks(8659) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 7 by UserName7 on Recipe 7", new DateTime(2025, 11, 12, 10, 16, 8, 730, DateTimeKind.Utc).AddTicks(8661) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 8 by UserName8 on Recipe 8", new DateTime(2025, 11, 12, 10, 6, 8, 730, DateTimeKind.Utc).AddTicks(8662) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 9 by UserName9 on Recipe 9", new DateTime(2025, 11, 12, 9, 56, 8, 730, DateTimeKind.Utc).AddTicks(8664) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 10 by UserName10 on Recipe 10", new DateTime(2025, 11, 12, 9, 46, 8, 730, DateTimeKind.Utc).AddTicks(8667) });

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
                columns: new[] { "ConcurrencyStamp", "Name", "Surname", "UserName" },
                values: new object[] { "944b6d9b-5858-4443-91dc-f5a5988dcd07", "Name1", "Surname1", "UserName1" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "Name", "Surname", "UserName" },
                values: new object[] { "fabeb2eb-ff4c-49ab-a645-3b869b44ebaa", "Name2", "Surname2", "UserName2" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "Name", "Surname", "UserName" },
                values: new object[] { "1588ce45-cddc-4bbd-9747-499e9e563ed4", "Name3", "Surname3", "UserName3" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "ConcurrencyStamp", "Name", "Surname", "UserName" },
                values: new object[] { "30073aca-572b-4d12-b6a4-cf5eb7b4a319", "Name4", "Surname4", "UserName4" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "ConcurrencyStamp", "Name", "Surname", "UserName" },
                values: new object[] { "d3d7eded-f882-46b6-9250-dd8b30ed0529", "Name5", "Surname5", "UserName5" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "ConcurrencyStamp", "Name", "Surname", "UserName" },
                values: new object[] { "59b9bceb-0e83-48ff-b346-ae0c32e3ec25", "Name6", "Surname6", "UserName6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "ConcurrencyStamp", "Name", "Surname", "UserName" },
                values: new object[] { "b02ddcdb-f7e9-4488-9ba4-3693da48ccc4", "Name7", "Surname7", "UserName7" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "ConcurrencyStamp", "Name", "Surname", "UserName" },
                values: new object[] { "57cd54cf-4089-4b03-acd6-8a047d84f9c4", "Name8", "Surname8", "UserName8" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "ConcurrencyStamp", "Name", "Surname", "UserName" },
                values: new object[] { "46f3e402-93f3-45e0-9d6e-f6826ca338d4", "Name9", "Surname9", "UserName9" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "ConcurrencyStamp", "Name", "Surname", "UserName" },
                values: new object[] { "dfe6ad1a-ccf3-42b1-a723-5530bd8d5514", "Name10", "Surname10", "UserName10" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000001"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 1 by User1 on Recipe 1", new DateTime(2025, 11, 11, 9, 11, 11, 371, DateTimeKind.Utc).AddTicks(9948) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000002"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 2 by User2 on Recipe 2", new DateTime(2025, 11, 11, 9, 1, 11, 371, DateTimeKind.Utc).AddTicks(9965) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000003"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 3 by User3 on Recipe 3", new DateTime(2025, 11, 11, 8, 51, 11, 371, DateTimeKind.Utc).AddTicks(9970) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000004"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 4 by User4 on Recipe 4", new DateTime(2025, 11, 11, 8, 41, 11, 371, DateTimeKind.Utc).AddTicks(9974) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000005"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 5 by User5 on Recipe 5", new DateTime(2025, 11, 11, 8, 31, 11, 371, DateTimeKind.Utc).AddTicks(9979) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000006"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 6 by User6 on Recipe 6", new DateTime(2025, 11, 11, 8, 21, 11, 371, DateTimeKind.Utc).AddTicks(9985) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000007"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 7 by User7 on Recipe 7", new DateTime(2025, 11, 11, 8, 11, 11, 371, DateTimeKind.Utc).AddTicks(9990) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000008"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 8 by User8 on Recipe 8", new DateTime(2025, 11, 11, 8, 1, 11, 371, DateTimeKind.Utc).AddTicks(9995) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000009"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 9 by User9 on Recipe 9", new DateTime(2025, 11, 11, 7, 51, 11, 372, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: new Guid("50000000-0000-0000-0000-000000000010"),
                columns: new[] { "Content", "CreatedAt" },
                values: new object[] { "Comment 10 by User10 on Recipe 10", new DateTime(2025, 11, 11, 7, 41, 11, 372, DateTimeKind.Utc).AddTicks(6) });

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"),
                column: "Name",
                value: "User1's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"),
                column: "Name",
                value: "User2's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"),
                column: "Name",
                value: "User3's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"),
                column: "Name",
                value: "User4's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"),
                column: "Name",
                value: "User5's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000006"),
                column: "Name",
                value: "User6's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000007"),
                column: "Name",
                value: "User7's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000008"),
                column: "Name",
                value: "User8's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000009"),
                column: "Name",
                value: "User9's Shopping List");

            migrationBuilder.UpdateData(
                table: "ShoppingLists",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000010"),
                column: "Name",
                value: "User10's Shopping List");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"),
                columns: new[] { "ConcurrencyStamp", "UserName" },
                values: new object[] { null, null });
        }
    }
}
