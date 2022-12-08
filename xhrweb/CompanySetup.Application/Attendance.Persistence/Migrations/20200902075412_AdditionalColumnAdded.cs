using Microsoft.EntityFrameworkCore.Migrations;

namespace Attendance.Persistence.Migrations
{
    public partial class AdditionalColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClockInAddress",
                schema: "attendance",
                table: "AttendanceProcessedData",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockOutAddress",
                schema: "attendance",
                table: "AttendanceProcessedData",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeRemarks",
                schema: "attendance",
                table: "AttendanceProcessedData",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance9",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance9",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance9",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance8",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance8",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance8",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance7",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance7",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance7",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance6",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance6",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance6",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance5",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance5",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance5",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance4",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance4",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance3",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance3",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance3",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance2",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance2",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance2",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance12",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance12",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance12",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance11",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance11",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance11",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance10",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance10",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance10",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance1",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance1",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance1",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClockInAddress",
                schema: "attendance",
                table: "AttendanceProcessedData");

            migrationBuilder.DropColumn(
                name: "ClockOutAddress",
                schema: "attendance",
                table: "AttendanceProcessedData");

            migrationBuilder.DropColumn(
                name: "EmployeeRemarks",
                schema: "attendance",
                table: "AttendanceProcessedData");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance9");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance9");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance9");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance8");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance8");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance8");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance7");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance7");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance7");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance6");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance6");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance6");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance5");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance5");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance5");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance4");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance4");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance4");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance3");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance3");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance3");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance2");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance2");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance2");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance12");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance12");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance12");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance11");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance11");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance11");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance10");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance10");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance10");

            migrationBuilder.DropColumn(
                name: "ClockTimeAddress",
                schema: "attendance",
                table: "Attendance1");

            migrationBuilder.DropColumn(
                name: "ClockTimeType",
                schema: "attendance",
                table: "Attendance1");

            migrationBuilder.DropColumn(
                name: "Remarks",
                schema: "attendance",
                table: "Attendance1");
        }
    }
}
