create or replace
view leave."AttendanceProcessedDatas"
as
select
	"Id",
	"EmployeeId",
	"PunchDate",
	"PunchYear",
	"PunchMonth",
	"Status",
	"FinancialYearId",
	"MonthCycleId"
from
	attendance."AttendanceProcessedData"
where
	"Status" in('P', 'L');
