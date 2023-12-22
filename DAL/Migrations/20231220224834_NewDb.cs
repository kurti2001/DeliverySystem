using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class NewDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    IdPackage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    BarcodePackage = table.Column<int>(type: "int", maxLength: 12, nullable: false),
                    SentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DestinationAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.IdPackage);
                });

            migrationBuilder.CreateTable(
                name: "PostalOffice",
                columns: table => new
                {
                    PostalOfficeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeName = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalOffice", x => x.PostalOfficeId);
                });

            migrationBuilder.CreateTable(
                name: "Recepsionists",
                columns: table => new
                {
                    
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recepsionists", x => x.Email);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_BarcodePackage",
                table: "Packages",
                column: "BarcodePackage",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostalOffice_Address",
                table: "PostalOffice",
                column: "Address");

            migrationBuilder.CreateIndex(
                name: "IX_PostalOffice_Location",
                table: "PostalOffice",
                column: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Recepsionists_PhoneNumber",
                table: "Recepsionists",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "PostalOffice");

            migrationBuilder.DropTable(
                name: "Recepsionists");
        }
    }
}
