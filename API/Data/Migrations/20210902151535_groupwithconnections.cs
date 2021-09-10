using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Data.Migrations
{
    public partial class groupwithconnections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliverySchedule_AspNetUsers_DeliverymanId",
                table: "DeliverySchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliverySchedule_Orders_OrderId",
                table: "DeliverySchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliverySchedule",
                table: "DeliverySchedule");

            migrationBuilder.DropColumn(
                name: "DateRead",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "DeliverySchedule",
                newName: "DeliverySchedules");

            migrationBuilder.RenameIndex(
                name: "IX_DeliverySchedule_OrderId",
                table: "DeliverySchedules",
                newName: "IX_DeliverySchedules_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliverySchedule_DeliverymanId",
                table: "DeliverySchedules",
                newName: "IX_DeliverySchedules_DeliverymanId");

            migrationBuilder.AddColumn<bool>(
                name: "isRead",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliverySchedules",
                table: "DeliverySchedules",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    ConnectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.ConnectionId);
                    table.ForeignKey(
                        name: "FK_Connections_Groups_GroupName",
                        column: x => x.GroupName,
                        principalTable: "Groups",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_GroupName",
                table: "Connections",
                column: "GroupName");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliverySchedules_AspNetUsers_DeliverymanId",
                table: "DeliverySchedules",
                column: "DeliverymanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliverySchedules_Orders_OrderId",
                table: "DeliverySchedules",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliverySchedules_AspNetUsers_DeliverymanId",
                table: "DeliverySchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_DeliverySchedules_Orders_OrderId",
                table: "DeliverySchedules");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliverySchedules",
                table: "DeliverySchedules");

            migrationBuilder.DropColumn(
                name: "isRead",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "DeliverySchedules",
                newName: "DeliverySchedule");

            migrationBuilder.RenameIndex(
                name: "IX_DeliverySchedules_OrderId",
                table: "DeliverySchedule",
                newName: "IX_DeliverySchedule_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_DeliverySchedules_DeliverymanId",
                table: "DeliverySchedule",
                newName: "IX_DeliverySchedule_DeliverymanId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRead",
                table: "Messages",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliverySchedule",
                table: "DeliverySchedule",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliverySchedule_AspNetUsers_DeliverymanId",
                table: "DeliverySchedule",
                column: "DeliverymanId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliverySchedule_Orders_OrderId",
                table: "DeliverySchedule",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
