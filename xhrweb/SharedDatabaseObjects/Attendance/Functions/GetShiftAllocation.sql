CREATE OR REPLACE FUNCTION attendance.getshiftallocation(companyid uuid, branchid uuid, financialyearid uuid, monthcycleid uuid, departmentid uuid, designationid uuid, employeeid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "EmployeeName" character varying, "CompanyId" uuid, "CompanyName" character varying, "BranchId" uuid, "BranchName" character varying, "DepartmentId" uuid, "DepartmentName" character varying, "DesignationId" uuid, "DesignationName" character varying, "FinancialYearId" uuid, "DutyYear" character varying, "MonthCycleId" uuid, "DutyMonth" character varying, "RotationDay" integer, "JoiningDate" timestamp without time zone, "QuitDate" timestamp without time zone, "StatusId" smallint, "PermanentDate" timestamp without time zone, "C1" text, "C2" text, "C3" text, "C4" text, "C5" text, "C6" text, "C7" text, "C8" text, "C9" text, "C10" text, "C11" text, "C12" text, "C13" text, "C14" text, "C15" text, "C16" text, "C17" text, "C18" text, "C19" text, "C20" text, "C21" text, "C22" text, "C23" text, "C24" text, "C25" text, "C26" text, "C27" text, "C28" text, "C29" text, "C30" text, "C31" text)
 LANGUAGE plpgsql
AS $function$
BEGIN
	if(employeeId is not null)
	then
	 RETURN QUERY
        Select  sa."Id"
		        , e."Id" as EmployeeId
		        , e."FullName" as EmployeeName
				, c."Id" as CompanyId
				, c."CompanyName" as CompanyName
				, b."Id" as BranchId
				, b."BranchName" as BranchName
				, d."Id" as "DepartmentId"
			    , d."DepartmentName"
				, ds."Id" as DesignationId
				, ds."DesignationName"
				, sa."FinancialYearId"
				, sa."DutyYear" as DutyYear
				, sa."MonthCycleId" as MonthCycleId
				, sa."DutyMonth" as DutyMonth
				,sa."RotationDay" as RotationDay
				,ee."JoiningDate" as JoiningDate
				,ee."QuitDate" as QuitDate
				,ee."StatusId" as StatusId
				,ee."PermanentDate" as PermanentDate
                ,sa."C1"
				,sa."C2"
				,sa."C3"
				,sa."C4"
				,sa."C5"
				,sa."C6"
				,sa."C7"
				,sa."C8"
				,sa."C9"
				,sa."C10"
				,sa."C11"
				,sa."C12"
				,sa."C13"
				,sa."C14"
				,sa."C15"
				,sa."C16"
				,sa."C17"
				,sa."C18"
				,sa."C19"
				,sa."C20"
				,sa."C21"
				,sa."C22"
				,sa."C23"
				,sa."C24"
				,sa."C25"
				,sa."C26"
				,sa."C27"
				,sa."C28"
				,sa."C29"
				,sa."C30"
				,sa."C31"
         from  employee."Employees" e 
		    inner join main."Designations" as ds ON ds."Id"=e."PositionId"
            inner join main."Departments" as d on d."Id" = e."DepartmentId"
			INNER JOIN main."Branch" as b ON b."Id"=e."BranchId"
			INNER JOIN main."Company" as c ON c."Id"=e."CompanyId"
			inner JOIN employee."EmployeeEnrollments" as ee ON ee."EmployeeId"=e."Id"  and ee."StatusId" = 1
			LEFT JOIN attendance."ShiftAllocations"  as sa on sa."EmployeeId"=e."Id"
			             and  sa."FinancialYearId"=financialyearid and sa."MonthCycleId"=monthcycleid
        where  e."IsDeleted" = false and  ( e."CompanyId" = companyId )
		      -- and (branchId is null OR e."BranchId"=branchId)
		       and e."Id"= employeeId;
			   	else
	    RETURN QUERY
		Select  sa."Id"
		        , e."Id" as EmployeeId
		        , e."FullName" as EmployeeName
				, c."Id" as CompanyId
				, c."CompanyName" as CompanyName
				, b."Id" as BranchId
				, b."BranchName" as BranchName
				, d."Id" as "DepartmentId"
			    , d."DepartmentName"
				, ds."Id" as DesignationId
				, ds."DesignationName"
				, sa."FinancialYearId"
				, sa."DutyYear" as DutyYear
				, sa."MonthCycleId" as MonthCycleId
				, sa."DutyMonth" as DutyMonth
				,sa."RotationDay" as RotationDay
				,ee."JoiningDate" as JoiningDate
				,ee."QuitDate" as QuitDate
				,ee."StatusId" as StatusId
				,ee."PermanentDate" as PermanentDate
                ,sa."C1"
				,sa."C2"
				,sa."C3"
				,sa."C4"
				,sa."C5"
				,sa."C6"
				,sa."C7"
				,sa."C8"
				,sa."C9"
				,sa."C10"
				,sa."C11"
				,sa."C12"
				,sa."C13"
				,sa."C14"
				,sa."C15"
				,sa."C16"
				,sa."C17"
				,sa."C18"
				,sa."C19"
				,sa."C20"
				,sa."C21"
				,sa."C22"
				,sa."C23"
				,sa."C24"
				,sa."C25"
				,sa."C26"
				,sa."C27"
				,sa."C28"
				,sa."C29"
				,sa."C30"
				,sa."C31"
         from  employee."Employees" e 
		    inner join main."Designations" as ds ON ds."Id"=e."PositionId"
            inner join main."Departments" as d on d."Id" = e."DepartmentId"
			INNER JOIN main."Branch" as b ON b."Id"=e."BranchId"
			INNER JOIN main."Company" as c ON c."Id"=e."CompanyId"
			inner JOIN employee."EmployeeEnrollments" as ee ON ee."EmployeeId"= e."Id"  and ee."StatusId" = 1
			LEFT JOIN attendance."ShiftAllocations"  as sa on sa."EmployeeId" = e."Id"
			             --and  sa."DutyYear"=financialYear and sa."DutyMonth"=monthName
			             and  sa."FinancialYearId"=financialyearid and sa."MonthCycleId"=monthcycleid
        where e."IsDeleted" = false and ( e."CompanyId" = companyid )
		       and ( e."BranchId"=branchid)
		       and (e."DepartmentId"='00000000-0000-0000-0000-000000000000' OR departmentId is null OR e."DepartmentId"=departmentId )
		       and (e."PositionId"='00000000-0000-0000-0000-000000000000' OR designationId is null OR e."PositionId"=designationId ) 
			   and (e."Id"='00000000-0000-0000-0000-000000000000' OR employeeId is null OR e."Id" = employeeId )
			   ; 
	end if;		   
END
$function$
;
