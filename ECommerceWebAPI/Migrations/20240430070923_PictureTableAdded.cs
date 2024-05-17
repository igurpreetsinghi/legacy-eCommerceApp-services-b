using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class PictureTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_OrderItem_tbl_Product_ProductId",
                table: "tbl_OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_OrderItem_tbl_User_UserId",
                table: "tbl_OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ShoppingCart_tbl_Product_ProductId",
                table: "tbl_ShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ShoppingCart_tbl_User_UserId",
                table: "tbl_ShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_User_tbl_Address_AddressId",
                table: "tbl_User");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_User_Role_Intermidate_tbl_Role_RoleId",
                table: "tbl_User_Role_Intermidate");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_User_Role_Intermidate_tbl_User_UserId",
                table: "tbl_User_Role_Intermidate");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "tbl_Product",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "tbl_Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "tbl_Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "tbl_Pictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Pictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Pictures_tbl_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_Product",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Product_CategoryId",
                table: "tbl_Product",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Pictures_ProductId",
                table: "tbl_Pictures",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_OrderItem_tbl_Product_ProductId",
                table: "tbl_OrderItem",
                column: "ProductId",
                principalTable: "tbl_Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_OrderItem_tbl_User_UserId",
                table: "tbl_OrderItem",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Product_tbl_Category_CategoryId",
                table: "tbl_Product",
                column: "CategoryId",
                principalTable: "tbl_Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ShoppingCart_tbl_Product_ProductId",
                table: "tbl_ShoppingCart",
                column: "ProductId",
                principalTable: "tbl_Product",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ShoppingCart_tbl_User_UserId",
                table: "tbl_ShoppingCart",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_User_tbl_Address_AddressId",
                table: "tbl_User",
                column: "AddressId",
                principalTable: "tbl_Address",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_User_Role_Intermidate_tbl_Role_RoleId",
                table: "tbl_User_Role_Intermidate",
                column: "RoleId",
                principalTable: "tbl_Role",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_User_Role_Intermidate_tbl_User_UserId",
                table: "tbl_User_Role_Intermidate",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_OrderItem_tbl_Product_ProductId",
                table: "tbl_OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_OrderItem_tbl_User_UserId",
                table: "tbl_OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Product_tbl_Category_CategoryId",
                table: "tbl_Product");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ShoppingCart_tbl_Product_ProductId",
                table: "tbl_ShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_ShoppingCart_tbl_User_UserId",
                table: "tbl_ShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_User_tbl_Address_AddressId",
                table: "tbl_User");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_User_Role_Intermidate_tbl_Role_RoleId",
                table: "tbl_User_Role_Intermidate");

            migrationBuilder.DropForeignKey(
                name: "FK_tbl_User_Role_Intermidate_tbl_User_UserId",
                table: "tbl_User_Role_Intermidate");

            migrationBuilder.DropTable(
                name: "tbl_Pictures");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Product_CategoryId",
                table: "tbl_Product");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "tbl_Product");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "tbl_Product");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryId",
                table: "tbl_Product",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_OrderItem_tbl_Product_ProductId",
                table: "tbl_OrderItem",
                column: "ProductId",
                principalTable: "tbl_Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_OrderItem_tbl_User_UserId",
                table: "tbl_OrderItem",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ShoppingCart_tbl_Product_ProductId",
                table: "tbl_ShoppingCart",
                column: "ProductId",
                principalTable: "tbl_Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_ShoppingCart_tbl_User_UserId",
                table: "tbl_ShoppingCart",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_User_tbl_Address_AddressId",
                table: "tbl_User",
                column: "AddressId",
                principalTable: "tbl_Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_User_Role_Intermidate_tbl_Role_RoleId",
                table: "tbl_User_Role_Intermidate",
                column: "RoleId",
                principalTable: "tbl_Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_User_Role_Intermidate_tbl_User_UserId",
                table: "tbl_User_Role_Intermidate",
                column: "UserId",
                principalTable: "tbl_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
