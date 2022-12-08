CREATE OR REPLACE FUNCTION employee.getemployeedashboardattendancedata(employeeid uuid)
 RETURNS TABLE("EmployeeId" uuid, "TotalPresent" numeric, "TotalOnTime" numeric, "TotalLate" numeric, "TotalAbsent" numeric, "TotalLeave" numeric, "TotalWorkingInWHdays" numeric)
 LANGUAGE plpgsql
AS $function$
declare startDate timestamp; endDate timestamp;
	begin
		--select fy."FinancialYearStartDate" into startDate from main."FinancialYears" fy  where fy."Id" ='811e792c-0b56-422a-9c10-3d566a5ab49b';
		select mc."MonthStartDate" , mc."MonthEndDate" into startDate, endDate from main."MonthCycles" mc inner join employee."Employees" e on mc."CompanyId" = e."CompanyId" 
			where "MonthStartDate" <= current_date and "MonthEndDate" >=  current_date and e."Id" = employeeId ;--'9f341a89-1805-4afd-9c35-8c765c954d78';
		RETURN QUERY
			SELECT pd."EmployeeId" AS EmployeeId --, MONTH(pd.PunchDate) AS MonthNumber
				, SUM(CASE WHEN pd."Status" IN('P','L') THEN 1.0 ELSE 0 END) AS TotalPresent
				, SUM(CASE WHEN pd."Status" IN('P') THEN 1.0 ELSE 0 END) AS TotalOnTime
				, SUM(CASE WHEN pd."Status" IN('L') THEN 1.0 ELSE 0 END) AS TotalLate
				, SUM(CASE WHEN pd."Status" IN('A') THEN 1.0 ELSE 0 END) AS TotalAbsent
				, SUM(CASE WHEN pd."Status" IN('CL', 'SL', 'EL', 'ML','CCL','WPL') THEN 1.0 ELSE 0 END) AS TotalLeave
				, SUM(CASE WHEN pd."Status" IN('W', 'H') THEN 1.0 ELSE 0 END) AS TotalWorkingInWHdays
				--, SUM(CASE WHEN pd."Status" IN ('P', 'L', 'W', 'H') AND pd."OTHour" > 0 THEN datepart(HH,pd."OTHour") ELSE 0 END) + 
				--	SUM(CASE WHEN pd."Status" IN ('P', 'L', 'W', 'H') AND pd."OTHour" > 0 THEN datepart(MI,pd."OTHour") ELSE 0 END)/60.0 OTDays
				--,Min(pd."PunchDate") as FromDate
				--,Max(pd."PunchDate") as ToDate
			FROM  attendance."AttendanceProcessedData" pd
			WHERE pd."PunchDate" between startDate and endDate 
				AND pd."EmployeeId" =employeeId
			GROUP BY pd."EmployeeId";
	END;
$function$
;
