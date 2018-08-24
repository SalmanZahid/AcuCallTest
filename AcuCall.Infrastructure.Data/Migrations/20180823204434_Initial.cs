using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AcuCall.Infrastructure.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(maxLength: 100, nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: false) 
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    SessionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login_DateTime = table.Column<DateTime>(nullable: false),
                    Logout_DateTime = table.Column<DateTime>(nullable: true),
                    Username = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.SessionId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_Login_DateTime",
                table: "UserSessions",
                column: "Login_DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_Logout_DateTime",
                table: "UserSessions",
                column: "Logout_DateTime");


            var sp = @"Create PROC GetUserSessionReport
                        @Month int,
                        @Year int
                        AS
                        BEGIN
                        select cast(Login_DateTime as date) as [Date],
                                 max(coalesce(logins, 0) - coalesce(logouts, 0)) as MaxUsers
                          from (select l.Login_DateTime,
                                       (select count(*)
                                        from [UserSessions] l2
                                        where MONTH(l2.Login_DateTime) = @Month AND YEAR(l2.Login_DateTime) = @Year AND l2.Login_DateTime <= l.Login_DateTime
                                       ) as logins,
                                       (select count(*)
                                        from [UserSessions] l2
                                        where MONTH(l2.Logout_DateTime) = @Month AND YEAR(l2.Logout_DateTime) = @Year AND l2.Logout_DateTime <= l.Login_DateTime
                                       ) as logouts
                                from [UserSessions] l
                               ) l
                          group by cast(Login_DateTime as date);
                          END";

            migrationBuilder.Sql(sp);

            migrationBuilder.Sql("INSERT INTO Users(Username, Password, FirstName, LastName) Values('admin', 'admin', 'Salman', 'Zahid')");            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            base.Down(migrationBuilder);
        }
    }
}
