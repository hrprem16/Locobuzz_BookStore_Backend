using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WishlistTable",
                columns: table => new
                {
                    WishlistId = table.Column<int>(name: "Wishlist_Id", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    Bookid = table.Column<int>(name: "Book_id", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistTable", x => x.WishlistId);
                    table.ForeignKey(
                        name: "FK_WishlistTable_UserTable_Book_id",
                        column: x => x.Bookid,
                        principalTable: "UserTable",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_WishlistTable_UserTable_userId",
                        column: x => x.userId,
                        principalTable: "UserTable",
                        principalColumn: "userId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistTable_Book_id",
                table: "WishlistTable",
                column: "Book_id");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistTable_userId",
                table: "WishlistTable",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WishlistTable");
        }
    }
}
