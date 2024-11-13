using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProject.Infrustrucure.Migrations
{
    /// <inheritdoc />
    public partial class addLoguserInCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "loguser",
                table: "basket",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "loguser",
                table: "basket");
        }
    }
}
