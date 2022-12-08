CREATE OR REPLACE FUNCTION employee.getemployeephonebyid(
	employeephoneid uuid)
RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "PhoneNumber" character varying, "PhoneTypeId" uuid) 
    LANGUAGE 'plpgsql'
    VOLATILE 
    COST 100
    ROWS 1000
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeePhone."Id" , 
			EmployeePhone."EmployeeId" , 
			EmployeePhone."PhoneNumber" , 
			EmployeePhone."PhoneTypeId" 
		 FROM employee."EmployeePhones" AS EmployeePhone 
		 WHERE EmployeePhone."IsDeleted" = False  AND EmployeePhone."Id" = employeePhoneId;
	END;
$BODY$;

