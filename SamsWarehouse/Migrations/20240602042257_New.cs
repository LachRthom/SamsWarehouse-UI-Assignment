using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SamsWarehouse.Migrations
{
    /// <inheritdoc />
    public partial class New : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinalisedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingLists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShoppingListId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingListItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingListItems_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "ItemName", "Unit", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Granny Smith Apples", "1kg", 5.50m },
                    { 2, "Fresh tomatoes", "500g", 5.90m },
                    { 3, "Watermelon", "Whole", 6.60m },
                    { 4, "Cucumber", "1 whole", 1.90m },
                    { 5, "Red potato washed", "1kg", 4.00m },
                    { 6, "Red tipped bananas", "1kg", 4.90m },
                    { 7, "Red onion", "1kg", 3.50m },
                    { 8, "Carrots", "1kg", 2.00m },
                    { 9, "Iceburg Lettuce", "1", 2.50m },
                    { 10, "Helga's Wholemeal", "1", 3.70m },
                    { 11, "Free range chicken", "1kg", 7.50m },
                    { 12, "Manning Valley 6-pk", "6 eggs", 3.60m },
                    { 13, "A2 light milk", "1 litre", 2.90m },
                    { 14, "Chobani Strawberry Yoghurt", "1", 1.50m },
                    { 15, "Lurpak Salted Blend", "250g", 5.00m },
                    { 16, "Bega Farmers Tasty", "250g", 4.00m },
                    { 17, "Babybel Mini", "100g", 4.20m },
                    { 18, "Cobram EVOO", "375ml", 8.00m },
                    { 19, "Heinz Tomato Soup", "535g", 2.50m },
                    { 20, "John West Tuna can", "95g", 1.50m },
                    { 21, "Cadbury Dairy Milk", "200g", 5.00m },
                    { 22, "Coca Cola", "2 litre", 2.85m },
                    { 23, "Smith's Original Share Pack Crisps", "170g", 3.29m },
                    { 24, "Birds Eye Fish Fingers", "375g", 4.50m },
                    { 25, "Berri Orange Juice", "2 litre", 6.00m },
                    { 26, "Vegemite", "380g", 6.00m },
                    { 27, "Cheddar Shapes", "175g", 2.00m },
                    { 28, "Colgate Total Toothpaste Original", "110g", 3.50m },
                    { 29, "Milo Chocolate Malt", "200g", 4.00m },
                    { 30, "Weet Bix Sanitarium Organic", "750g", 5.33m },
                    { 31, "Lindt Excellence 70% Cocoa Block", "100g", 4.25m },
                    { 32, "Original Tim Tams Chocolate", "200g", 3.65m },
                    { 33, "Philadelphia Original Cream Cheese", "250g", 4.30m },
                    { 34, "Moccona Classic Instant Medium Roast", "100g", 6.00m },
                    { 35, "Capilano Squeezable Honey", "500g", 7.35m },
                    { 36, "Nutella jar", "400g", 4.00m },
                    { 37, "Arnott's Scotch Finger", "250g", 2.85m },
                    { 38, "South Cape Greek Feta", "200g", 5.00m },
                    { 39, "Sacla Pasta Tomato Basil Sauce", "420g", 4.50m },
                    { 40, "Primo English Ham", "100g", 3.00m },
                    { 41, "Primo Short cut rindless Bacon", "175g", 5.00m },
                    { 42, "Golden Circle Pineapple Pieces in natural juice", "440g", 3.25m },
                    { 43, "San Remo Linguine Pasta No 1", "500g", 1.95m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "PasswordHash" },
                values: new object[,]
                {
                    { 1, "user1@example.com", "$2a$11$dc2ZJ.NoFAjRdV/JARj95uP7eUTxoQ8xQeXLCSPbURGrDolM6TfAK" },
                    { 2, "user2@example.com", "$2a$11$c8l6Hd5xUm54ZygLNTw4PuxHQaQMWoVGQqoDnjmDF3HaM2kKDAI.e" },
                    { 3, "user3@example.com", "$2a$11$ouHpM9LrcYHTF2b1bayDhesq5q/B8kLYsg/TG9UbD7AtQd418gjau" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItems_ProductId",
                table: "ShoppingListItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItems_ShoppingListId",
                table: "ShoppingListItems",
                column: "ShoppingListId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_UserId",
                table: "ShoppingLists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShoppingListItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
