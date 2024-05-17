using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class WebApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Category");

            migrationBuilder.DropTable(
                name: "tbl_OrderItem");

            migrationBuilder.DropTable(
                name: "tbl_ShoppingCart");

            migrationBuilder.DropTable(
                name: "tbl_User_Role_Intermidate");

            migrationBuilder.DropTable(
                name: "tbl_Role");

            migrationBuilder.DropTable(
                name: "tbl_User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureId = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SystemName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CustomerGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PINCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_OrderItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_OrderItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_OrderItem_tbl_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "tbl_Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OrderItem_tbl_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_OrderItem_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ShoppingCart",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShoppingCartType = table.Column<int>(type: "int", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ShoppingCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_ShoppingCart_tbl_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_ShoppingCart_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_User_Role_Intermidate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_User_Role_Intermidate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_User_Role_Intermidate_tbl_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_User_Role_Intermidate_tbl_User_UserId",
                        column: x => x.UserId,
                        principalTable: "tbl_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OrderItem_OrderId",
                table: "tbl_OrderItem",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OrderItem_ProductId",
                table: "tbl_OrderItem",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_OrderItem_UserId",
                table: "tbl_OrderItem",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ShoppingCart_ProductId",
                table: "tbl_ShoppingCart",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ShoppingCart_UserId",
                table: "tbl_ShoppingCart",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_User_Role_Intermidate_RoleId",
                table: "tbl_User_Role_Intermidate",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_User_Role_Intermidate_UserId",
                table: "tbl_User_Role_Intermidate",
                column: "UserId");
        }
    }
}
