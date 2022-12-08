CREATE OR REPLACE FUNCTION leave.getleavebalancesummarybyemployee(employeeid uuid, leavecalender character varying)
 RETURNS TABLE("EmployeeId" uuid, "EmployeeName" character varying, "LeaveTypeId" uuid, "LeaveTypeName" character varying, "LeaveCalendar" character varying, "MaximumBalance" double precision, "UsedBalance" double precision, "RemainingBalance" double precision)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
      Select lb."EmployeeId"
	        , e."FullName" as EmployeeName
		    , lb."LeaveTypeId"
			, lt."LeaveTypeName"
			, lb."LeaveCalendar"
			, lb."MaximumBalance"
			, lb."UsedBalance"
            , lb."RemainingBalance"
        from leave."LeaveBalance" as lb
        INNER JOIN employee."Employees" as e ON e."Id"=lb."EmployeeId"
        INNER JOIN leave."LeaveTypes"  as lt ON lt."Id"=lb."LeaveTypeId" and lt."CompanyId"  = e."CompanyId" and lt."IsDeleted" = false
		Where lb."EmployeeId"=employeeId and lb."LeaveCalendar"=leaveCalender;
	
END;
$function$
;
