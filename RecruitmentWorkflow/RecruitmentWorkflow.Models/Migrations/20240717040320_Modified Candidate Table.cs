using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecruitmentWorkflow.Models.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedCandidateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_CandidateStatusMaster_CandidateStatusMasterId",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_Candidates_RecruitmentStageMaster_RecruitmentStageMasterId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_CandidateStatusMasterId",
                table: "Candidates");

            migrationBuilder.DropIndex(
                name: "IX_Candidates_RecruitmentStageMasterId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CandidateStatusMasterId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "CurrentStage",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "RecruitmentStageMasterId",
                table: "Candidates");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Candidates");

            migrationBuilder.CreateTable(
                name: "MyCandidates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    CandidateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CandidateEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentInterviewerId = table.Column<int>(type: "int", nullable: false),
                    CurrentStage = table.Column<int>(type: "int", nullable: false),
                    CurrentStageStatus = table.Column<int>(type: "int", nullable: false),
                    CandidateStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyCandidates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyCandidates");

            migrationBuilder.AddColumn<int>(
                name: "CandidateStatusMasterId",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentStage",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecruitmentStageMasterId",
                table: "Candidates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Candidates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CandidateStatusMasterId",
                table: "Candidates",
                column: "CandidateStatusMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_RecruitmentStageMasterId",
                table: "Candidates",
                column: "RecruitmentStageMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_CandidateStatusMaster_CandidateStatusMasterId",
                table: "Candidates",
                column: "CandidateStatusMasterId",
                principalTable: "CandidateStatusMaster",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Candidates_RecruitmentStageMaster_RecruitmentStageMasterId",
                table: "Candidates",
                column: "RecruitmentStageMasterId",
                principalTable: "RecruitmentStageMaster",
                principalColumn: "Id");
        }
    }
}
