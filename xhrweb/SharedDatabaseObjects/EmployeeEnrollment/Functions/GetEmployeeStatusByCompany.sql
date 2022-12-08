CREATE OR REPLACE FUNCTION employee.getemployeestatusbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeStatusName" character varying, "EmployeeStatusNameLC" character varying, "Rank" smallint, "CompanyId" uuid,"StatusId" smallint)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeStatus."Id" , 
			EmployeeStatus."EmployeeStatusName" , 
			EmployeeStatus."EmployeeStatusNameLC" , 
			EmployeeStatus."Rank" , 
			EmployeeStatus."CompanyId",
			EmployeeStatus."StatusId"
		 FROM employee."EmployeeStatuses" AS EmployeeStatus 
		 WHERE EmployeeStatus."IsDeleted" = False AND EmployeeStatus."CompanyId" = companyid;
	END;
$function$
;


DROP FUNCTION employee.getemployeestatusbycompany(uuid)