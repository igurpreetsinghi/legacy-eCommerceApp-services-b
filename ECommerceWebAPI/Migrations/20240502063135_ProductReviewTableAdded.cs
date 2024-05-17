using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProductReviewTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "tbl_User_Role_Intermidate",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "tbl_User",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "tbl_Product",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "tbl_Pictures",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "tbl_Orders",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "tbl_OrderItem",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UpdateDate",
                table: "tbl_Address",
                newName: "UpdatedDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "tbl_Pictures",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "ProductReviewId",
                table: "tbl_Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "tbl_ProductReview",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProductReview", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_ProductReview_tbl_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ProductReview_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Pictures_ProductReviewId",
                table: "tbl_Pictures",
                column: "ProductReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProductReview_ProductId",
                table: "tbl_ProductReview",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProductReview_UserId",
                table: "tbl_ProductReview",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Pictures_tbl_ProductReview_ProductReviewId",
                table: "tbl_Pictures",
                column: "ProductReviewId",
                principalTable: "tbl_ProductReview",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Pictures_tbl_ProductReview_ProductReviewId",
                table: "tbl_Pictures");

            migrationBuilder.DropTable(
                name: "tbl_ProductReview");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Pictures_ProductReviewId",
                table: "tbl_Pictures");

            migrationBuilder.DropColumn(
                name: "ProductReviewId",
                table: "tbl_Pictures");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "tbl_User_Role_Intermidate",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "tbl_User",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "tbl_Product",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "tbl_Pictures",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "tbl_Orders",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "tbl_OrderItem",
                newName: "UpdateDate");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "tbl_Address",
                newName: "UpdateDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "tbl_Pictures",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getdate()");
        }
    }
}
