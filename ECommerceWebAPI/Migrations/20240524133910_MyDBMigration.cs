using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class MyDBMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Pictures_tbl_Product_ProductId",
                table: "tbl_Pictures");

            migrationBuilder.DropIndex(
                name: "IX_tbl_Pictures_ProductId",
                table: "tbl_Pictures");

            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "tbl_Pictures",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "ImageData",
                table: "tbl_Pictures",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Pictures_ProductId",
                table: "tbl_Pictures",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Pictures_tbl_Product_ProductId",
                table: "tbl_Pictures",
                column: "ProductId",
                principalTable: "tbl_Product",
                principalColumn: "Id");
        }
    }
}
