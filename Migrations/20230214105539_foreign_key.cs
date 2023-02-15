using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIPractice.Migrations
{
    /// <inheritdoc />
    public partial class foreignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Categories_categoryId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_categoryId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "categoryId",
                table: "Medicines");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_CategotyId",
                table: "Medicines",
                column: "CategotyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Categories_CategotyId",
                table: "Medicines",
                column: "CategotyId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Categories_CategotyId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_CategotyId",
                table: "Medicines");

            migrationBuilder.AddColumn<byte>(
                name: "categoryId",
                table: "Medicines",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_categoryId",
                table: "Medicines",
                column: "categoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Categories_categoryId",
                table: "Medicines",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
