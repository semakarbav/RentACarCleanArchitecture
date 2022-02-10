using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class lastChanges1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CorporateCustomers_Customers_CustomerId",
                table: "CorporateCustomers");

            migrationBuilder.DropForeignKey(
                name: "FK_IndividualCustomers_Customers_CustomerId",
                table: "IndividualCustomers");

            migrationBuilder.DropIndex(
                name: "IX_IndividualCustomers_CustomerId",
                table: "IndividualCustomers");

            migrationBuilder.DropIndex(
                name: "IX_CorporateCustomers_CustomerId",
                table: "CorporateCustomers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_IndividualCustomers_CustomerId",
                table: "IndividualCustomers",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CorporateCustomers_CustomerId",
                table: "CorporateCustomers",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CorporateCustomers_Customers_CustomerId",
                table: "CorporateCustomers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IndividualCustomers_Customers_CustomerId",
                table: "IndividualCustomers",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
