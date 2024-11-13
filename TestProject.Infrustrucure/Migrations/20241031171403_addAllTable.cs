using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestProject.Infrustrucure.Migrations
{
    /// <inheritdoc />
    public partial class addAllTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "peapleBusnise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name_user = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name_Shope = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    region_shope = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_peapleBusnise", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t1",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    des = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t1", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t2",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    des = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t2", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "t3",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    des = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t3", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "vendors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GivenNames = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    DocId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DocType = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Timestamp_create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    loguser = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "controlTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_vendor = table.Column<int>(type: "int", nullable: true),
                    M_Code = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Last_sore = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    visitor = table.Column<long>(type: "bigint", nullable: true),
                    shopeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    region = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    city = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Street = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    NerestPoint = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_controlTables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_controlTables_vendors_id_vendor",
                        column: x => x.id_vendor,
                        principalTable: "vendors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_controlTables_id_vendor",
                table: "controlTables",
                column: "id_vendor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "controlTables");

            migrationBuilder.DropTable(
                name: "peapleBusnise");

            migrationBuilder.DropTable(
                name: "t1");

            migrationBuilder.DropTable(
                name: "t2");

            migrationBuilder.DropTable(
                name: "t3");

            migrationBuilder.DropTable(
                name: "vendors");
        }
    }
}
