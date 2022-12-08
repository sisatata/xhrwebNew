CREATE OR REPLACE FUNCTION leave.getemployeeleavesummarydata(employeeid uuid)
 RETURNS TABLE("LeaveTypeName" character varying, "LeaveTypeShortName" character varying, "MaximumBalance" numeric, "UsedBalance" numeric, "RemainingBalance" numeric, "LeaveCalendar" character varying)
 LANGUAGE plpgsql
AS $function$
declare startDate timestamp; endDate timestamp;
	begin
		--select fy."FinancialYearStartDate" into startDate from main."FinancialYears" fy  where fy."Id" ='811e792c-0b56-422a-9c10-3d566a5ab49b';
		select mc."MonthStartDate" , mc."MonthEndDate" into startDate, endDate from main."MonthCycles" mc inner join employee."Employees" e on mc."CompanyId" = e."CompanyId" 
			where "MonthStartDate" <= current_date and "MonthEndDate" >=  current_date and e."Id" = employeeId ;--'9f341a89-1805-4afd-9c35-8c765c954d78';
		RETURN QUERY
			select
				lt."LeaveTypeName",
				lt."LeaveTypeShortName" ,
				cast(nullif(lb."MaximumBalance",0) as numeric) MaximumBalance,
				cast(nullif(lb."UsedBalance",0) as numeric)  UsedBalance,
				cast(nullif(lb."RemainingBalance",0) as numeric) RemainingBalance,
				lb."LeaveCalendar" 
			from
				leave."LeaveBalance" lb
				inner join employee."Employees" e on lb."EmployeeId"  = e."Id" 
				inner join leave."LeaveTypes" lt on 	lb."LeaveTypeId" = lt."Id"	and e."CompanyId" = lt."CompanyId"	and lt."IsDeleted" = false
				inner join main."FinancialYears" fy on 	lb."LeaveCalendar" = fy."FinancialYearName" 	and fy."CompanyId" = e."CompanyId"
			where
				lb."EmployeeId" = employeeId and startDate between fy."FinancialYearStartDate"  and fy."FinancialYearEndDate" and lb."IsDeleted" = false;
	END;
$function$
;
