CREATE OR REPLACE FUNCTION employee.rptemployeejoinbycompany(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("EmployeeId" character varying, "DesigantionOrder" bigint, "FullName" character varying, "DepartmentName" character varying, "CompanyName" character varying, "DesignationName" character varying, "GradeName" character varying, "JoiningDate" text, "ResignDate" text, "StatusId" smallint, "EmployeeImageUri" text)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		    e."EmployeeId" ,
		    d2."SortOrder"  as "DesigantionOrder",
		    e."FullName" ,
		    d."DepartmentName" ,
		    c."CompanyName" ,
		    d2."DesignationName" ,
		    g."GradeName" ,
		   -- clut."LookUpTypeName" ,
		    to_char(ee."JoiningDate",'dd-Mon-yyyy') as "JoiningDate",
		    to_char(ee."QuitDate",'dd-Mon-yyyy') as "ResignDate" ,
		    ee."StatusId" ,
		   -- es."GrossSalary" ,
		    ee."EmployeeImageUri" 
		from employee."EmployeeEnrollments" ee 
		inner join employee."Employees" e on e."Id"  = ee."EmployeeId" 
		inner join main."Departments" d on d."Id"  = e."DepartmentId" 
		inner join main."Designations" d2 on d2."Id" = e."PositionId" 
		inner join main."Grades" g on g."Id" = ee."GradeId" 
		--inner join main."CommonLookUpTypes" clut on clut."Id" = e."GenderId" 
		inner join main."Company" c on c."Id" = companyid
	--	left join payroll."EmployeeSalaries" es on es."EmployeeId"  = ee."EmployeeId" 
		where e."IsDeleted"  = false  and
		ee."IsDeleted" = false and 
		d."IsDeleted" = false and 
		d2."IsDeleted"  = false and 
		g."IsDeleted"  = false  and 
	--	es."IsDeleted" = false and
		e."CompanyId"  = companyid
		and (startdate is null
	or ee."JoiningDate" ::date >= startdate)
	and (enddate is null
	or ee."JoiningDate" :: date <= enddate)
	order by 
	ee."JoiningDate" ;		 
	END;
$function$
;
