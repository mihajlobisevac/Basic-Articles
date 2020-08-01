using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BasicArticles.Server.Migrations
{
    public partial class InitialArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
                    IntroText = table.Column<string>(maxLength: 300, nullable: true),
                    BodyText = table.Column<string>(maxLength: 3000, nullable: true),
                    ImagePath = table.Column<string>(maxLength: 100, nullable: true),
                    PublishedDate = table.Column<DateTime>(nullable: false),
                    UpdatedDate = table.Column<DateTime>(nullable: false),
                    Category = table.Column<int>(nullable: false),
                    User = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");
        }
    }
}
