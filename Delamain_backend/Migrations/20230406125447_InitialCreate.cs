using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Delamain_backend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HospitalLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    lat = table.Column<double>(type: "double precision", nullable: false),
                    lng = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HospitalLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IcuDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Outcome = table.Column<bool>(type: "boolean", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    Bmi = table.Column<double>(type: "double precision", nullable: true),
                    Hypertensive = table.Column<bool>(type: "boolean", nullable: true),
                    Atrialfibrillation = table.Column<bool>(type: "boolean", nullable: true),
                    ChdWithNoMi = table.Column<bool>(type: "boolean", nullable: true),
                    Diabetes = table.Column<bool>(type: "boolean", nullable: true),
                    Deficiencyanemias = table.Column<bool>(type: "boolean", nullable: true),
                    Depression = table.Column<bool>(type: "boolean", nullable: true),
                    Hyperlipemia = table.Column<bool>(type: "boolean", nullable: true),
                    Copd = table.Column<bool>(type: "boolean", nullable: true),
                    HeartRate = table.Column<double>(type: "double precision", nullable: true),
                    RespitoryRate = table.Column<double>(type: "double precision", nullable: true),
                    Temperature = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcuDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "bytea", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "bytea", nullable: false),
                    VerificationToken = table.Column<string>(type: "text", nullable: true),
                    VerifieAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "text", nullable: true),
                    ResetTokenExpires = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "queuemodels",
                columns: table => new
                {
                    queueID = table.Column<string>(type: "text", nullable: false),
                    queueordernum = table.Column<int>(type: "integer", nullable: false),
                    pushbackcount = table.Column<int>(type: "integer", nullable: false),
                    Riskscore = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_queuemodels", x => x.queueID);
                });

            migrationBuilder.CreateTable(
                name: "riskmodals",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    age10to50 = table.Column<double>(type: "double precision", nullable: false),
                    age51to100 = table.Column<double>(type: "double precision", nullable: false),
                    male = table.Column<double>(type: "double precision", nullable: false),
                    female = table.Column<double>(type: "double precision", nullable: false),
                    BMI18to25 = table.Column<double>(type: "double precision", nullable: false),
                    BMIoutsideof18to25 = table.Column<double>(type: "double precision", nullable: false),
                    hypertensive = table.Column<double>(type: "double precision", nullable: false),
                    atrialfibrillation = table.Column<double>(type: "double precision", nullable: false),
                    CHD_with_no_MI = table.Column<double>(type: "double precision", nullable: false),
                    diabetes = table.Column<double>(type: "double precision", nullable: false),
                    deficiencyanemias = table.Column<double>(type: "double precision", nullable: false),
                    depression = table.Column<double>(type: "double precision", nullable: false),
                    hyperlipmia = table.Column<double>(type: "double precision", nullable: false),
                    COPD = table.Column<double>(type: "double precision", nullable: false),
                    hr60to100 = table.Column<double>(type: "double precision", nullable: false),
                    hroutside60to100 = table.Column<double>(type: "double precision", nullable: false),
                    rr12to16 = table.Column<double>(type: "double precision", nullable: false),
                    rroutside12to16 = table.Column<double>(type: "double precision", nullable: false),
                    goodbodytemp = table.Column<double>(type: "double precision", nullable: false),
                    outsidegoodbodytemp = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_riskmodals", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "userReqmodels",
                columns: table => new
                {
                    userReqID = table.Column<string>(type: "text", nullable: false),
                    queueordernum = table.Column<int>(type: "integer", nullable: false),
                    pushbackcount = table.Column<int>(type: "integer", nullable: false),
                    Riskscore = table.Column<double>(type: "double precision", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    Symptoms = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    BMI = table.Column<double>(type: "double precision", nullable: false),
                    diabetes = table.Column<bool>(type: "boolean", nullable: false),
                    deficiencyanemias = table.Column<bool>(type: "boolean", nullable: false),
                    hypertensive = table.Column<bool>(type: "boolean", nullable: false),
                    hyperlipemia = table.Column<bool>(type: "boolean", nullable: false),
                    atrialfibrillation = table.Column<bool>(type: "boolean", nullable: false),
                    CHD_with_no_MI = table.Column<bool>(type: "boolean", nullable: false),
                    COPD = table.Column<bool>(type: "boolean", nullable: false),
                    depression = table.Column<bool>(type: "boolean", nullable: false),
                    heart_rate = table.Column<double>(type: "double precision", nullable: false),
                    respitory_rate = table.Column<double>(type: "double precision", nullable: false),
                    tempurature = table.Column<double>(type: "double precision", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    geoloc = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userReqmodels", x => x.userReqID);
                });

            migrationBuilder.CreateTable(
                name: "userdetails",
                columns: table => new
                {
                    userReqID = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    Symptoms = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    gender = table.Column<string>(type: "text", nullable: false),
                    BMI = table.Column<double>(type: "double precision", nullable: false),
                    diabetes = table.Column<bool>(type: "boolean", nullable: false),
                    deficiencyanemias = table.Column<bool>(type: "boolean", nullable: false),
                    hypertensive = table.Column<bool>(type: "boolean", nullable: false),
                    hyperlipemia = table.Column<bool>(type: "boolean", nullable: false),
                    atrialfibrillation = table.Column<bool>(type: "boolean", nullable: false),
                    CHD_with_no_MI = table.Column<bool>(type: "boolean", nullable: false),
                    COPD = table.Column<bool>(type: "boolean", nullable: false),
                    depression = table.Column<bool>(type: "boolean", nullable: false),
                    heart_rate = table.Column<double>(type: "double precision", nullable: false),
                    respitory_rate = table.Column<double>(type: "double precision", nullable: false),
                    tempurature = table.Column<double>(type: "double precision", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    geoloc = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: false),
                    queueID = table.Column<string>(type: "text", nullable: false),
                    QueuemodelqueueID = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userdetails", x => x.userReqID);
                    table.ForeignKey(
                        name: "FK_userdetails_queuemodels_QueuemodelqueueID",
                        column: x => x.QueuemodelqueueID,
                        principalTable: "queuemodels",
                        principalColumn: "queueID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_userdetails_QueuemodelqueueID",
                table: "userdetails",
                column: "QueuemodelqueueID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HospitalLocations");

            migrationBuilder.DropTable(
                name: "IcuDatas");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "riskmodals");

            migrationBuilder.DropTable(
                name: "userdetails");

            migrationBuilder.DropTable(
                name: "userReqmodels");

            migrationBuilder.DropTable(
                name: "queuemodels");
        }
    }
}
