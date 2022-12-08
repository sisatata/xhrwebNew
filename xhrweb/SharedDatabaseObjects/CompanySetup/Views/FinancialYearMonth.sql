CREATE OR REPLACE VIEW main."FinancialYearMonth"
as select
	mnth."MonthNumber",
	Yr."YearNumber",
	mnth. "Id",
	mnth."FinancialYearId",
	mnth."MonthCycleName",
	mnth."MonthCycleLocalizedName",
	mnth."MonthStartDate",
	mnth."MonthEndDate",
	mnth."TotalWorkingDays",
	mnth."RunningFlag",
	mnth."IsSelected",
	mnth."SortOrder" "MonthSortOrder",
	Yr."CompanyId",
	Yr."FinancialYearName",
	Yr."FinancialYearLocalizedName",
	Yr."FinancialYearStartDate",
	Yr."FinancialYearEndDate",
	Yr."IsRunningYear" ,
	Yr."SortOrder" "YearSortOrder"
from
	(
	select
		row_number() over (partition by mc."CompanyId" 
	order by
		mc."MonthStartDate" ) as "MonthNumber", mc.*
	from
		main."MonthCycles" mc where "IsDeleted"=false) Mnth
inner join (
	select
		row_number () over(partition by fy."CompanyId"
	order by
		fy."FinancialYearStartDate" ) as "YearNumber", fy."Id", fy."CompanyId", fy."FinancialYearName", fy."FinancialYearLocalizedName", fy."FinancialYearStartDate", fy."FinancialYearEndDate", fy."IsRunningYear", fy."SortOrder", fy."IsDeleted"
	from
		main."FinancialYears" fy where "IsDeleted"=false) Yr on
	Mnth."FinancialYearId" = Yr."Id";
	
;