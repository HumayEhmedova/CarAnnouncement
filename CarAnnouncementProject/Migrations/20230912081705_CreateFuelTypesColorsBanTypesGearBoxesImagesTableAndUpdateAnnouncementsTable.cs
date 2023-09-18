using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAnnouncementProject.Migrations
{
    public partial class CreateFuelTypesColorsBanTypesGearBoxesImagesTableAndUpdateAnnouncementsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BanTypeId",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FuelTypeId",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GearBoxId",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BanTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BanName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuelTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuelTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GearBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GearBoxes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnnouncementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Announcements_AnnouncementId",
                        column: x => x.AnnouncementId,
                        principalTable: "Announcements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_BanTypeId",
                table: "Announcements",
                column: "BanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_ColorId",
                table: "Announcements",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_FuelTypeId",
                table: "Announcements",
                column: "FuelTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_GearBoxId",
                table: "Announcements",
                column: "GearBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_AnnouncementId",
                table: "Images",
                column: "AnnouncementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_BanTypes_BanTypeId",
                table: "Announcements",
                column: "BanTypeId",
                principalTable: "BanTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Colors_ColorId",
                table: "Announcements",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_FuelTypes_FuelTypeId",
                table: "Announcements",
                column: "FuelTypeId",
                principalTable: "FuelTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_GearBoxes_GearBoxId",
                table: "Announcements",
                column: "GearBoxId",
                principalTable: "GearBoxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_BanTypes_BanTypeId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Colors_ColorId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_FuelTypes_FuelTypeId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_GearBoxes_GearBoxId",
                table: "Announcements");

            migrationBuilder.DropTable(
                name: "BanTypes");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "FuelTypes");

            migrationBuilder.DropTable(
                name: "GearBoxes");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_BanTypeId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_ColorId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_FuelTypeId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_GearBoxId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "BanTypeId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "FuelTypeId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "GearBoxId",
                table: "Announcements");
        }
    }
}
