using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddPhotoEntityToContext1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Massages_MassageTypeId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Massages",
                table: "Massages");

            migrationBuilder.RenameTable(
                name: "Massages",
                newName: "MassagesType");

            migrationBuilder.AddColumn<string>(
                name: "Src",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MassagesType",
                table: "MassagesType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_MassagesType_MassageTypeId",
                table: "Records",
                column: "MassageTypeId",
                principalTable: "MassagesType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_MassagesType_MassageTypeId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MassagesType",
                table: "MassagesType");

            migrationBuilder.DropColumn(
                name: "Src",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "MassagesType",
                newName: "Massages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Massages",
                table: "Massages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Massages_MassageTypeId",
                table: "Records",
                column: "MassageTypeId",
                principalTable: "Massages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
