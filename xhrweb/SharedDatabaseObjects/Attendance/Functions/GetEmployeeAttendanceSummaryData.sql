CREATE OR REPLACE FUNCTION attendance.GetEmployeeAttendanceSummaryData(employeeid uuid, startDate timestamp, endDate timestamp)
 RETURNS TABLE("EmployeeId" uuid, "TotalPresent" numeric, "TotalOnTime" numeric, "TotalLate" numeric, "TotalAbsent" numeric, "TotalLeave" numeric, "TotalWorkingInWHdays" numeric)
 LANGUAGE plpgsql
AS $function$
	begin
		RETURN QUERY
			SELECT pd."EmployeeId" AS EmployeeId --, MONTH(pd.PunchDate) AS MonthNumber
				, SUM(CASE WHEN pd."Status" IN('P','L') THEN 1.0 ELSE 0 END) AS TotalPresent
				, SUM(CASE WHEN pd."Status" IN('P') THEN 1.0 ELSE 0 END) AS TotalOnTime
				, SUM(CASE WHEN pd."Status" IN('L') THEN 1.0 ELSE 0 END) AS TotalLate
				, SUM(CASE WHEN pd."Status" IN('A') THEN 1.0 ELSE 0 END) AS TotalAbsent
				, SUM(CASE WHEN pd."Status" IN('CL', 'SL', 'EL', 'ML','CCL','WPL') THEN 1.0 ELSE 0 END) AS TotalLeave
				, SUM(CASE WHEN pd."Status" IN('W', 'H') THEN 1.0 ELSE 0 END) AS TotalWorkingInWHdays
			FROM  attendance."AttendanceProcessedData" pd
			WHERE pd."PunchDate" between startDate and endDate 
				AND pd."EmployeeId" = employeeId
			GROUP BY pd."EmployeeId";
	END;
$function$
;
