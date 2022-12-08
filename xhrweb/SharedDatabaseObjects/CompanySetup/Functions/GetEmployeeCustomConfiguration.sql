CREATE OR REPLACE FUNCTION main.GetEmployeeCustomConfiguration(employeeId uuid, code character varying)
RETURNS TABLE (
		"Id" uuid ,
		"Code" character varying(50) ,
		"CustomValue" character varying(100) ,
		"Description" character varying(250) ,
		"StartDate" timestamp,
		"EndDate" timestamp ,
		"IsDeleted" boolean ,
		"EmployeeId" uuid 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeCustomConfiguration."Id" , 
			EmployeeCustomConfiguration."Code" , 
			EmployeeCustomConfiguration."CustomValue" , 
			EmployeeCustomConfiguration."Description" , 
			EmployeeCustomConfiguration."StartDate" , 
			EmployeeCustomConfiguration."EndDate" , 
			EmployeeCustomConfiguration."IsDeleted" , 
			EmployeeCustomConfiguration."EmployeeId"  
		 FROM employee."EmployeeCustomConfigurations" AS EmployeeCustomConfiguration 
		 WHERE EmployeeCustomConfiguration."IsDeleted" = False and EmployeeCustomConfiguration."EmployeeId" = employeeId and EmployeeCustomConfiguration."Code" = code;
	END;
$BODY$
LANGUAGE plpgsql;



DROP FUNCTION main.getconfigurationsbycompany(uuid);

select * from main.GetEmployeeCustomConfiguration('3162f4b7-ac94-44df-a928-c7d247beb99f') 
