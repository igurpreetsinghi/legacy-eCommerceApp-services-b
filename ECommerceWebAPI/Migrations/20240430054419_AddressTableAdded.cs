using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddressTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address1",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "City",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "PINCode",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "State",
                table: "tbl_User");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "tbl_User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tbl_Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PINCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Address", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_User_AddressId",
                table: "tbl_User",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_User_tbl_Address_AddressId",
                table: "tbl_User",
                column: "AddressId",
                principalTable: "tbl_Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_User_tbl_Address_AddressId",
                table: "tbl_User");

            migrationBuilder.DropTable(
                name: "tbl_Address");

            migrationBuilder.DropIndex(
                name: "IX_tbl_User_AddressId",
                table: "tbl_User");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "tbl_User");

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "tbl_User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PINCode",
                table: "tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "tbl_User",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "tbl_User",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
