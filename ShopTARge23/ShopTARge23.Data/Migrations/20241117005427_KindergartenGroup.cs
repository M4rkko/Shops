using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopTARge23.Data.Migrations
{
    /// <inheritdoc />
    public partial class KindergartenGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
           name: "KindergartenGroup",
           columns: table => new
           {
               Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
               GroupName = table.Column<string>(type: "nvarchar(64)", nullable: false),
               ChildrenCount = table.Column<int> (type: "int", nullable: false),
               Teacher = table.Column<string> (type: "nvarchar(64)", nullable: false),
               CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
               ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
           },
           constraints: table =>
           {
               table.PrimaryKey("PK_KindergartenGroup", x => x.Id);
           });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "KindergartenGroup");
        }
    }
}
