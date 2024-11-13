using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProject.Infrustrucure.Migrations
{
    /// <inheritdoc />
    public partial class addTableBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "basket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerId = table.Column<int>(type: "int", nullable: false),
                    nameIdShope = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tootal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    countElementBasket = table.Column<int>(type: "int", nullable: false),
                    codeBasket = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basket", x => x.Id);
                    table.ForeignKey(
                        name: "FK_basket_customers_customerId",
                        column: x => x.customerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "basket_s",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    basketId = table.Column<int>(type: "int", nullable: false),
                    itemId = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    totoal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basket_s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_basket_s_basket_basketId",
                        column: x => x.basketId,
                        principalTable: "basket",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_basket_customerId",
                table: "basket",
                column: "customerId");

            migrationBuilder.CreateIndex(
                name: "IX_basket_s_basketId",
                table: "basket_s",
                column: "basketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "basket_s");

            migrationBuilder.DropTable(
                name: "basket");
        }
    }
}
