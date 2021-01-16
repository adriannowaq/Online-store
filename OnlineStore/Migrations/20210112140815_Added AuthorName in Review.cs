using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Migrations
{
    public partial class AddedAuthorNameinReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Reviews",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AlterColumn<byte>(
                name: "Count",
                table: "CartItems",
                type: "tinyint unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Reviews");

            migrationBuilder.AlterColumn<int>(
                name: "Count",
                table: "CartItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint unsigned");
        }
    }
}
