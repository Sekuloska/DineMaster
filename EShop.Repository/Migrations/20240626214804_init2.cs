using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EShop.Repository.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemInOrder_MenuItem_MenuItemId",
                table: "ItemInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemInOrder_Order_OrderId",
                table: "ItemInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Restaurant_RestaurantId",
                table: "Menu");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_userId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_Restaurant_RestaurantId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_userId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Order_OrderId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurant",
                table: "Restaurant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu",
                table: "Menu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemInOrder",
                table: "ItemInOrder");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "Restaurant",
                newName: "Restaurants");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "MenuItem",
                newName: "MenuItems");

            migrationBuilder.RenameTable(
                name: "Menu",
                newName: "Menus");

            migrationBuilder.RenameTable(
                name: "ItemInOrder",
                newName: "ItemsInOrder");

            migrationBuilder.RenameIndex(
                name: "IX_Review_userId",
                table: "Reviews",
                newName: "IX_Reviews_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_OrderId",
                table: "Reviews",
                newName: "IX_Reviews_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_userId",
                table: "Orders",
                newName: "IX_Orders_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_RestaurantId",
                table: "Orders",
                newName: "IX_Orders_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItem_MenuId",
                table: "MenuItems",
                newName: "IX_MenuItems_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_Menu_RestaurantId",
                table: "Menus",
                newName: "IX_Menus_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemInOrder_OrderId",
                table: "ItemsInOrder",
                newName: "IX_ItemsInOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemInOrder_MenuItemId",
                table: "ItemsInOrder",
                newName: "IX_ItemsInOrder_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemsInOrder",
                table: "ItemsInOrder",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "DeliveryPeople",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RestaurantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryPeople_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    DatePayment = table.Column<int>(type: "int", nullable: true),
                    PaymentMethod = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryPersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryStatus = table.Column<int>(type: "int", nullable: true),
                    DeliveryDate = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deliveries_DeliveryPeople_DeliveryPersonId",
                        column: x => x.DeliveryPersonId,
                        principalTable: "DeliveryPeople",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Deliveries_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DeliveryPersonId",
                table: "Deliveries",
                column: "DeliveryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_OrderId",
                table: "Deliveries",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPeople_RestaurantId",
                table: "DeliveryPeople",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_OrderId",
                table: "Payments",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsInOrder_MenuItems_MenuItemId",
                table: "ItemsInOrder",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsInOrder_Orders_OrderId",
                table: "ItemsInOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Restaurants_RestaurantId",
                table: "Menus",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_userId",
                table: "Orders",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Restaurants_RestaurantId",
                table: "Orders",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_userId",
                table: "Reviews",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Orders_OrderId",
                table: "Reviews",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsInOrder_MenuItems_MenuItemId",
                table: "ItemsInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemsInOrder_Orders_OrderId",
                table: "ItemsInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_Menus_MenuId",
                table: "MenuItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Restaurants_RestaurantId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_userId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Restaurants_RestaurantId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_userId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Orders_OrderId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "Deliveries");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "DeliveryPeople");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Restaurants",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MenuItems",
                table: "MenuItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ItemsInOrder",
                table: "ItemsInOrder");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "Restaurants",
                newName: "Restaurant");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "Menu");

            migrationBuilder.RenameTable(
                name: "MenuItems",
                newName: "MenuItem");

            migrationBuilder.RenameTable(
                name: "ItemsInOrder",
                newName: "ItemInOrder");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_userId",
                table: "Review",
                newName: "IX_Review_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_OrderId",
                table: "Review",
                newName: "IX_Review_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_userId",
                table: "Order",
                newName: "IX_Order_userId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_RestaurantId",
                table: "Order",
                newName: "IX_Order_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_Menus_RestaurantId",
                table: "Menu",
                newName: "IX_Menu_RestaurantId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuItems_MenuId",
                table: "MenuItem",
                newName: "IX_MenuItem_MenuId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsInOrder_OrderId",
                table: "ItemInOrder",
                newName: "IX_ItemInOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemsInOrder_MenuItemId",
                table: "ItemInOrder",
                newName: "IX_ItemInOrder_MenuItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Restaurant",
                table: "Restaurant",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu",
                table: "Menu",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MenuItem",
                table: "MenuItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ItemInOrder",
                table: "ItemInOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInOrder_MenuItem_MenuItemId",
                table: "ItemInOrder",
                column: "MenuItemId",
                principalTable: "MenuItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemInOrder_Order_OrderId",
                table: "ItemInOrder",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Restaurant_RestaurantId",
                table: "Menu",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Menu_MenuId",
                table: "MenuItem",
                column: "MenuId",
                principalTable: "Menu",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_userId",
                table: "Order",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Restaurant_RestaurantId",
                table: "Order",
                column: "RestaurantId",
                principalTable: "Restaurant",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_userId",
                table: "Review",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Order_OrderId",
                table: "Review",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }
    }
}
