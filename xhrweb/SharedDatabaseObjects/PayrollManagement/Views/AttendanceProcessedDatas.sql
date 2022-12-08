CREATE OR REPLACE VIEW payroll."AttendanceProcessedDatas"
AS SELECT "AttendanceProcessedData"."EmployeeId",
    "AttendanceProcessedData"."PunchDate",
    "AttendanceProcessedData"."TimeIn",
    "AttendanceProcessedData"."TimeOut",
    "AttendanceProcessedData"."ShiftIn",
    "AttendanceProcessedData"."ShiftOut",
    "AttendanceProcessedData"."BreakIn",
    "AttendanceProcessedData"."BreakOut",
    "AttendanceProcessedData"."BreakLate",
    "AttendanceProcessedData"."Late",
    "AttendanceProcessedData"."ShiftCode",
    "AttendanceProcessedData"."RegularHour",
    "AttendanceProcessedData"."OTHour",
    "AttendanceProcessedData"."Status",
    "AttendanceProcessedData"."FinancialYearId",
    "AttendanceProcessedData"."MonthCycleId"
   FROM attendance."AttendanceProcessedData";