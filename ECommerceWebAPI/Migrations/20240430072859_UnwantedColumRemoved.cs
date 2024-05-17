using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UnwantedColumRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "tbl_Category");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "tbl_Category",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
