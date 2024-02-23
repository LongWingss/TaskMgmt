using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskMgmt.DataAccess.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            string query = File.ReadAllText("../TaskMgmt.DataAccess/Migrations/SeedData.sql");
            migrationBuilder.Sql(query);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
