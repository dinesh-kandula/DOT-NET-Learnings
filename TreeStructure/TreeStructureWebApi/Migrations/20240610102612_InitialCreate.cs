using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TreeStructureWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Employees_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "EmployeeId", "JobTitle", "Name", "ParentId", "Role" },
                values: new object[,]
                {
                    { 1, "JD001", "Founder", "John Doe", null, 4 },
                    { 2, "JS002", "Director", "Jane Smith", 1, 3 },
                    { 3, "BJ003", "Manager", "Bob Johnson", 2, 2 },
                    { 9, "KW009", "Product Manager", "Kevin White", 2, 2 },
                    { 16, "OW016", "Marketing Manager", "Olivia Walker", 2, 2 },
                    { 18, "SL018", "Sales Manager", "Samantha Lee", 2, 2 },
                    { 29, "JW029", "Product Manager", "Jonathan White", 2, 2 },
                    { 4, "AB004", "Team Lead", "Alice Brown", 3, 1 },
                    { 10, "LN010", "UX Designer", "Lisa Nguyen", 9, 0 },
                    { 11, "MP011", "Backend Engineer", "Michael Patel", 9, 0 },
                    { 12, "RK012", "Frontend Engineer", "Rachel Kim", 9, 0 },
                    { 13, "CH013", "Full Stack Engineer", "Christopher Hall", 9, 0 },
                    { 14, "JM014", "Data Scientist", "Jessica Martin", 9, 0 },
                    { 15, "BT015", "Data Analyst", "Brian Thompson", 9, 0 },
                    { 17, "AB017", "Marketing Specialist", "Alexander Brown", 16, 0 },
                    { 19, "JK019", "Sales Representative", "Julia Kim", 18, 0 },
                    { 20, "DP020", "Customer Support", "Daniel Patel", 18, 0 },
                    { 21, "JL021", "Software Engineer", "Jacob Lee", 3, 0 },
                    { 22, "JC022", "QA Engineer", "Jasmine Chen", 3, 0 },
                    { 23, "JW023", "DevOps Engineer", "Jordan White", 3, 0 },
                    { 24, "JB024", "Team Lead", "Jessica Brown", 3, 1 },
                    { 30, "JB030", "UX Designer", "Jacqueline Brown", 9, 0 },
                    { 31, "JD031", "Backend Engineer", "Joshua Davis", 9, 0 },
                    { 32, "JK032", "Frontend Engineer", "Jessica Kim", 9, 0 },
                    { 33, "JH033", "Full Stack Engineer", "Joseph Hall", 9, 0 },
                    { 34, "JM034", "Data Scientist", "Jennifer Martin", 9, 0 },
                    { 5, "MD005", "Team Employee", "Mike Davis", 4, 0 },
                    { 6, "EC006", "Software Engineer", "Emily Chen", 4, 0 },
                    { 7, "DL007", "QA Engineer", "David Lee", 4, 0 },
                    { 8, "ST008", "DevOps Engineer", "Sarah Taylor", 4, 0 },
                    { 25, "JD025", "Team Employee", "James Davis", 4, 0 },
                    { 26, "JC026", "Software Engineer", "Jennifer Chen", 4, 0 },
                    { 27, "JL027", "QA Engineer", "Joseph Lee", 4, 0 },
                    { 28, "JT028", "DevOps Engineer", "Julia Taylor", 4, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ParentId",
                table: "Employees",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
