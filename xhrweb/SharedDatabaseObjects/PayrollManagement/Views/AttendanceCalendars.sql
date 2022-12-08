create or replace
view payroll."AttendanceCalendars" as
select
	"Id",
	"CompanyId",
	"FinancialYearName",
	"FinancialYearStartDate",
	"FinancialYearEndDate",
	"IsRunningYear",
	"SortOrder",
	"IsDeleted"
from
	main."FinancialYears"
where
	"IsDeleted" = false
	and "IsRunningYear" = true ;