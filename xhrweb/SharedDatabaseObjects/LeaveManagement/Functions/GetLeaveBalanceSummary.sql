CREATE OR REPLACE FUNCTION leave.getleavebalancesummary(companyid uuid, leavecalender character varying)
 RETURNS TABLE("EmployeeId" uuid, "EmployeeName" character varying, "LeaveTypeId" uuid, "LeaveTypeName" character varying, "LeaveCalendar" character varying, "MaximumBalance" double precision, "UsedBalance" double precision, "RemainingBalance" double precision, "BranchId" uuid, "DepartmentId" uuid, "PositionId" uuid, "BranchName" character varying, "DepartmentName" character varying, "DesignationName" character varying, "LoginId" character varying, "CompanyEmployeeId" character varying)
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
            , lb."RemainingBalance",
            e."BranchId" ,
            e."DepartmentId" ,
            e."PositionId" ,
            b."BranchName" ,
            d."DepartmentName" ,
            d2."DesignationName" ,
            anu."Email" as "LoginId",
            e."EmployeeId" as "CompanyEmployeeId"
        from leave."LeaveBalance" as lb
            INNER JOIN leave."LeaveTypes"  as lt ON lt."Id"=lb."LeaveTypeId"
			INNER JOIN employee."Employees" as e ON e."Id"=lb."EmployeeId"
			inner join main."Branch" b on  e."BranchId"  = b."Id" 
		    inner join main."Departments" d  on e."DepartmentId"  = d."Id" 
		    inner join main."Designations" d2 on e."PositionId"  = d2."Id" 
		     inner join "access"."AspNetUsers" anu  on e."LoginId" :: text = anu."Id" 
        where lb."CompanyId" = companyid and lb."LeaveCalendar"=leaveCalender
        and lb."IsDeleted" = false and lt."IsDeleted" =false and e."IsDeleted" = false 
        and d."IsDeleted" = false and b."IsDeleted" = false and d2."IsDeleted" = false;
	
END;
$function$
;
