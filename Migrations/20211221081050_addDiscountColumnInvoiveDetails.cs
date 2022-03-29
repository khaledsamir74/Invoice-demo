using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoiceDemo.Migrations
{
    public partial class addDiscountColumnInvoiveDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Discount",
                table: "InvoiceDetails",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "InvoiceDetails");
        }
    }
}
