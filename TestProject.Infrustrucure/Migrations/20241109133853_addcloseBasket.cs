using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProject.Infrustrucure.Migrations
{
    /// <inheritdoc />
    public partial class addcloseBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "closeBasket",
                table: "basket",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "closeBasket",
                table: "basket");
        }
    }
}
