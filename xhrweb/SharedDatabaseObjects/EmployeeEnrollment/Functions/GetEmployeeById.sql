CREATE OR REPLACE FUNCTION employee.GetEmployeeById(id uuid)
RETURNS TABLE("Id" uuid, "EmployeeId" character varying,"CompanyId" uuid, "FullName" character varying) 
    LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		select emp."Id" ,emp."EmployeeId" ,emp."CompanyId" ,emp."FullName" from employee."Employees" emp
		WHERE "IsDeleted" = False and emp."Id" = id;
	END;
$function$;

