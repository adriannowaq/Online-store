using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Migrations
{
    public partial class AddedAddressesCompletedinorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AddressesCompleted",
                table: "Orders",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressesCompleted",
                table: "Orders");
        }
    }
}
