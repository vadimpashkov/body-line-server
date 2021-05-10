using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    public partial class AddPhotoEntityToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Masseur_AspNetUsers_UserId",
                table: "Masseur");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_AspNetUsers_MesseurId",
                table: "Record");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_AspNetUsers_UserId",
                table: "Record");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_MassageType_MassageTypeId",
                table: "Record");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Record",
                table: "Record");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Masseur",
                table: "Masseur");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MassageType",
                table: "MassageType");

            migrationBuilder.RenameTable(
                name: "Record",
                newName: "Records");

            migrationBuilder.RenameTable(
                name: "Masseur",
                newName: "Massaures");

            migrationBuilder.RenameTable(
                name: "MassageType",
                newName: "Massages");

            migrationBuilder.RenameIndex(
                name: "IX_Record_UserId",
                table: "Records",
                newName: "IX_Records_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Record_MesseurId",
                table: "Records",
                newName: "IX_Records_MesseurId");

            migrationBuilder.RenameIndex(
                name: "IX_Record_MassageTypeId",
                table: "Records",
                newName: "IX_Records_MassageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Masseur_UserId",
                table: "Massaures",
                newName: "IX_Massaures_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Records",
                table: "Records",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Massaures",
                table: "Massaures",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Massages",
                table: "Massages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Src = table.Column<string>(type: "text", nullable: true),
                    Alt = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Massaures_AspNetUsers_UserId",
                table: "Massaures",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AspNetUsers_MesseurId",
                table: "Records",
                column: "MesseurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_AspNetUsers_UserId",
                table: "Records",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Massages_MassageTypeId",
                table: "Records",
                column: "MassageTypeId",
                principalTable: "Massages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Massaures_AspNetUsers_UserId",
                table: "Massaures");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_AspNetUsers_MesseurId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_AspNetUsers_UserId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Massages_MassageTypeId",
                table: "Records");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Records",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Massaures",
                table: "Massaures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Massages",
                table: "Massages");

            migrationBuilder.RenameTable(
                name: "Records",
                newName: "Record");

            migrationBuilder.RenameTable(
                name: "Massaures",
                newName: "Masseur");

            migrationBuilder.RenameTable(
                name: "Massages",
                newName: "MassageType");

            migrationBuilder.RenameIndex(
                name: "IX_Records_UserId",
                table: "Record",
                newName: "IX_Record_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Records_MesseurId",
                table: "Record",
                newName: "IX_Record_MesseurId");

            migrationBuilder.RenameIndex(
                name: "IX_Records_MassageTypeId",
                table: "Record",
                newName: "IX_Record_MassageTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Massaures_UserId",
                table: "Masseur",
                newName: "IX_Masseur_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Record",
                table: "Record",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Masseur",
                table: "Masseur",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MassageType",
                table: "MassageType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Masseur_AspNetUsers_UserId",
                table: "Masseur",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_AspNetUsers_MesseurId",
                table: "Record",
                column: "MesseurId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_AspNetUsers_UserId",
                table: "Record",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Record_MassageType_MassageTypeId",
                table: "Record",
                column: "MassageTypeId",
                principalTable: "MassageType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
