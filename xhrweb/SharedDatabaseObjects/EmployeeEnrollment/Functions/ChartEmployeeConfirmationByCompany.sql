CREATE OR REPLACE FUNCTION employee.chartemployeeconfirmationbycompany(companyid uuid, startdate timestamp without time zone, enddate timestamp without time zone)
 RETURNS TABLE("FullName" character varying, "DepartmentName" character varying, "DesignationName" character varying, "JoiningDate" text, "ResignDate" text, "StatusId" smallint, "GrossSalary" numeric, "PermanentDate" text)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
		--    e."EmployeeId" ,
		    e."FullName" ,
		    d."DepartmentName" ,
		 --   c."CompanyName" ,
		    d2."DesignationName" ,
		  --  g."GradeName" ,
		 --   clut."LookUpTypeName" ,
		  --  ea."AddressLine1" ,
		  --  ep."PhoneNumber" ,
		    to_char(ee."JoiningDate",'dd-Mon-yyyy') as "JoiningDate",
		   to_char(ee."QuitDate",'dd-Mon-yyyy') as "ResignDate" ,
		    ee."StatusId" ,
		    es."GrossSalary" ,
		   
		   to_char(ee."PermanentDate" ,'dd-Mon-yyyy') as "PermanentDate"
		from employee."EmployeeEnrollments" ee 
		inner join employee."Employees" e on e."Id"  = ee."EmployeeId" 
		inner join main."Departments" d on d."Id"  = e."DepartmentId" 
		inner join main."Designations" d2 on d2."Id" = e."PositionId" 
		--inner join main."Grades" g on g."Id" = ee."GradeId" 
		--inner join main."CommonLookUpTypes" clut on clut."Id" = e."GenderId" 
		--inner join main."Company" c on c."Id" = companyid
		inner join payroll."EmployeeSalaries" es on es."EmployeeId"  = ee."EmployeeId" 
	  --  left join employee."EmployeeAddresses" ea on ea."EmployeeId" = e."Id" 
	   -- left join employee."EmployeePhones" ep on ep."EmployeeId" = (SELECT 1 FROM   employee."employeephones" ep2 WHERE ep2."EmployeeId" = e."Id")
	    
		where e."IsDeleted"  = false  and
		ee."IsDeleted" = false and 
		d."IsDeleted" = false and 
		d2."IsDeleted"  = false and 
		--g."IsDeleted"  = false  and 
		es."IsDeleted" = false and
		e."CompanyId"  = companyid
		
		and (startdate is null
	or ee."PermanentDate" ::date >= startdate)
	and (enddate is null
	or ee."PermanentDate" :: date <= enddate)
	order by 
	ee."PermanentDate" ;
		
	 
	END;
$function$
;
