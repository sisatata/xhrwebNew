CREATE OR REPLACE FUNCTION employee.getemployeeprofilebyemployeeid(employeeid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" character varying, "FullName" character varying, "FullNameLC" character varying, "DateOfBirth" text, "JoiningDate" text, "BranchName" character varying, "DepartmentName" character varying, "DesignationName" character varying, "GenderName" character varying, "EmployeeImageUri" text)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		select
			e."Id" ,
			e."EmployeeId" ,
			e."FullName" ,
			e."FullNameLC" ,
			to_char( e."DateOfBirth",'dd/MM/yyyy') "DateOfBirth" ,
			to_char(ee."JoiningDate",'dd/MM/yyyy') "JoiningDate",
			b."BranchName" ,
			d."DepartmentName" ,
			d2."DesignationName" ,
			gender."LookUpTypeName" "GenderName",
			ee."EmployeeImageUri"
		from
			employee."Employees" e
		left join main."Branch" b on
			e."BranchId" = b."Id"
		left join main."Departments" d on
			e."DepartmentId" = d."Id"
		left join main."Designations" d2 on
			e."PositionId" = d2."Id"
		left join main."CommonLookUpTypes" gender on
			e."GenderId" = gender ."Id"
		left join employee."EmployeeEnrollments" ee on
			e."Id" = ee."EmployeeId" 
		where e."Id" = employeeid ;
	END;
$function$
;
