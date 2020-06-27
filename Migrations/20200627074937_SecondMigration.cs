using Microsoft.EntityFrameworkCore.Migrations;

namespace Votesys.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ItemID",
                table: "TB_T_Vote",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ItemID",
                table: "TB_T_Vote",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
