using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERestaurant.Repository.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeliveryPeople_RestaurantId",
                table: "DeliveryPeople");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPeople_RestaurantId",
                table: "DeliveryPeople",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DeliveryPeople_RestaurantId",
                table: "DeliveryPeople");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryPeople_RestaurantId",
                table: "DeliveryPeople",
                column: "RestaurantId",
                unique: true);
        }
    }
}
