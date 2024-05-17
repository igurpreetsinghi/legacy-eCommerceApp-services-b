using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "tbl_Product");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "tbl_Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
