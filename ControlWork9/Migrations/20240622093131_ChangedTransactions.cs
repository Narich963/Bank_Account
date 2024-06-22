using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ControlWork9.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_AspNetUsers_UserId",
                table: "CompanyUser");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUser_Company_CompanyId",
                table: "CompanyUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyUser",
                table: "CompanyUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Company",
                table: "Company");

            migrationBuilder.RenameTable(
                name: "CompanyUser",
                newName: "CompanyUsers");

            migrationBuilder.RenameTable(
                name: "Company",
                newName: "Companies");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyUser_UserId",
                table: "CompanyUsers",
                newName: "IX_CompanyUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyUser_CompanyId",
                table: "CompanyUsers",
                newName: "IX_CompanyUsers_CompanyId");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Transactions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyUsers",
                table: "CompanyUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Mega" },
                    { 2, "Aknet" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CompanyId",
                table: "Transactions",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUsers_AspNetUsers_UserId",
                table: "CompanyUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUsers_Companies_CompanyId",
                table: "CompanyUsers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Companies_CompanyId",
                table: "Transactions",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUsers_AspNetUsers_UserId",
                table: "CompanyUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyUsers_Companies_CompanyId",
                table: "CompanyUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Companies_CompanyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CompanyId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyUsers",
                table: "CompanyUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "CompanyUsers",
                newName: "CompanyUser");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Company");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyUsers_UserId",
                table: "CompanyUser",
                newName: "IX_CompanyUser_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyUsers_CompanyId",
                table: "CompanyUser",
                newName: "IX_CompanyUser_CompanyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyUser",
                table: "CompanyUser",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Company",
                table: "Company",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_AspNetUsers_UserId",
                table: "CompanyUser",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyUser_Company_CompanyId",
                table: "CompanyUser",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
