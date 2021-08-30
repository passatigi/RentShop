using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class deliveryver1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliverymanSchedules_AspNetUsers_DeliveryManId",
                table: "DeliverymanSchedules");

            migrationBuilder.RenameColumn(
                name: "DeliveryManId",
                table: "DeliverymanSchedules",
                newName: "DeliverymanId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliverymanSchedules_DeliveryManId",
                table: "DeliverymanSchedules",
                newName: "IX_DeliverymanSchedules_DeliverymanId");

            migrationBuilder.AlterColumn<int>(
                name: "DeliverymanId",
                table: "DeliverymanSchedules",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDelivery",
                table: "DeliverymanSchedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDelivery",
                table: "DeliverymanSchedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "DeliverySchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DeliverymanId = table.Column<int>(type: "int", nullable: false),
                    isShipping = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverySchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliverySchedule_AspNetUsers_DeliverymanId",
                        column: x => x.DeliverymanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliverySchedule_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliverySchedule_DeliverymanId",
                table: "DeliverySchedule",
                column: "DeliverymanId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliverySchedule_OrderId",
                table: "DeliverySchedule",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliverymanSchedules_AspNetUsers_DeliverymanId",
                table: "DeliverymanSchedules",
                column: "DeliverymanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliverymanSchedules_AspNetUsers_DeliverymanId",
                table: "DeliverymanSchedules");

            migrationBuilder.DropTable(
                name: "DeliverySchedule");

            migrationBuilder.DropColumn(
                name: "EndDelivery",
                table: "DeliverymanSchedules");

            migrationBuilder.DropColumn(
                name: "StartDelivery",
                table: "DeliverymanSchedules");

            migrationBuilder.RenameColumn(
                name: "DeliverymanId",
                table: "DeliverymanSchedules",
                newName: "DeliveryManId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliverymanSchedules_DeliverymanId",
                table: "DeliverymanSchedules",
                newName: "IX_DeliverymanSchedules_DeliveryManId");

            migrationBuilder.AlterColumn<int>(
                name: "DeliveryManId",
                table: "DeliverymanSchedules",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliverymanSchedules_AspNetUsers_DeliveryManId",
                table: "DeliverymanSchedules",
                column: "DeliveryManId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
