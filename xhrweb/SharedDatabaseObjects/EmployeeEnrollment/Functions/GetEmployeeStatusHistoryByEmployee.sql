CREATE OR REPLACE FUNCTION employee.getemployeestatushistorybyemployee(
	employeeid uuid)
RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "StatusId" uuid, "ChangedDate" timestamp without time zone, "Remarks" character varying) 
    LANGUAGE 'plpgsql'
    VOLATILE 
    COST 100
    ROWS 1000
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeStatusHistory."Id" , 
			EmployeeStatusHistory."EmployeeId" , 
			EmployeeStatusHistory."StatusId" , 
			EmployeeStatusHistory."ChangedDate" , 
			EmployeeStatusHistory."Remarks" , 
			ES."EmployeeStatusName",
			ES."EmployeeStatusNameLC", 
			ES."Rank"
		 FROM employee."EmployeeStatusHistories" AS EmployeeStatusHistory 
		 INNER JOIN employee."EmployeeStatuses" AS ES ON EmployeeStatusHistory."StatusId" = ES."StatusId"
		 WHERE EmployeeStatusHistory."IsDeleted" = False AND EmployeeStatusHistory."EmployeeId"  = employeeId;
	END;
$BODY$;

