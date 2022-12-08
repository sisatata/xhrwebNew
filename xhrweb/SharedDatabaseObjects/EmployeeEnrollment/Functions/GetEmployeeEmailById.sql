
CREATE OR REPLACE FUNCTION employee.getemployeeemailbyid(
	employeeemailid uuid)
RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "EmailAddress" character varying, "IsPrimary" boolean) 
    LANGUAGE 'plpgsql'
    VOLATILE 
    COST 100
    ROWS 1000
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeEmail."Id" , 
			EmployeeEmail."EmployeeId" , 
			EmployeeEmail."EmailAddress" , 
			EmployeeEmail."IsPrimary" 
		 FROM employee."EmployeeEmails" AS EmployeeEmail 
		 WHERE EmployeeEmail."IsDeleted" = False AND EmployeeEmail."Id" = employeeEmailId;
	END;
$BODY$;

