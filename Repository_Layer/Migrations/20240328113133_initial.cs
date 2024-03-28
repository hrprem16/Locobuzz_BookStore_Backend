using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    userId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "BookTable",
                columns: table => new
                {
                    Bookid = table.Column<int>(name: "Book_id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bookname = table.Column<string>(name: "Book_name", type: "nvarchar(max)", nullable: false),
                    BookDescription = table.Column<string>(name: "Book_Description", type: "nvarchar(max)", nullable: false),
                    BookAuthor = table.Column<string>(name: "Book_Author", type: "nvarchar(max)", nullable: false),
                    Bookimage = table.Column<string>(name: "Book_image", type: "nvarchar(max)", nullable: false),
                    BookPrice = table.Column<int>(name: "Book_Price", type: "int", nullable: false),
                    BookDiscountPrice = table.Column<int>(name: "Book_Discount_Price", type: "int", nullable: false),
                    BookQuantity = table.Column<int>(name: "Book_Quantity", type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTable", x => x.Bookid);
                    table.ForeignKey(
                        name: "FK_BookTable_UserTable_userId",
                        column: x => x.userId,
                        principalTable: "UserTable",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookTable_userId",
                table: "BookTable",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookTable");

            migrationBuilder.DropTable(
                name: "UserTable");
        }
    }
}
