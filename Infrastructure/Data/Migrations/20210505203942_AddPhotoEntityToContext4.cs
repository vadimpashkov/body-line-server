using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Data.Migrations
{
    public partial class AddPhotoEntityToContext4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_AspNetUsers_MesseurId",
                table: "Records");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Massaures_MesseurId",
                table: "Records",
                column: "MesseurId",
                principalTable: "Massaures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Records_Massaures_MesseurId",
                table: "Records");

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AspNetUsers_MesseurId",
                table: "Records",
                column: "MesseurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
