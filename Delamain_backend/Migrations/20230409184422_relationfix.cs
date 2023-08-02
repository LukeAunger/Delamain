using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Delamain_backend.Migrations
{
    /// <inheritdoc />
    public partial class relationfix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userdetails_queuemodels_QueuemodelqueueID",
                table: "userdetails");

            migrationBuilder.DropIndex(
                name: "IX_userdetails_QueuemodelqueueID",
                table: "userdetails");

            migrationBuilder.DropColumn(
                name: "QueuemodelqueueID",
                table: "userdetails");

            migrationBuilder.RenameColumn(
                name: "queueID",
                table: "userdetails",
                newName: "queueId");

            migrationBuilder.CreateIndex(
                name: "IX_userdetails_queueId",
                table: "userdetails",
                column: "queueId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_userdetails_queuemodels_queueId",
                table: "userdetails",
                column: "queueId",
                principalTable: "queuemodels",
                principalColumn: "queueID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userdetails_queuemodels_queueId",
                table: "userdetails");

            migrationBuilder.DropIndex(
                name: "IX_userdetails_queueId",
                table: "userdetails");

            migrationBuilder.RenameColumn(
                name: "queueId",
                table: "userdetails",
                newName: "queueID");

            migrationBuilder.AddColumn<string>(
                name: "QueuemodelqueueID",
                table: "userdetails",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userdetails_QueuemodelqueueID",
                table: "userdetails",
                column: "QueuemodelqueueID");

            migrationBuilder.AddForeignKey(
                name: "FK_userdetails_queuemodels_QueuemodelqueueID",
                table: "userdetails",
                column: "QueuemodelqueueID",
                principalTable: "queuemodels",
                principalColumn: "queueID");
        }
    }
}
