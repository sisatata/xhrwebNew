CREATE OR REPLACE FUNCTION employee.GetEmployeeDeviceById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"AccessToken" character varying(250) ,
		"Location" character varying(150) ,
		"Device" character varying(250) ,
		"OperatingSystem" character varying(100) ,
		"OSVersion" character varying(50) ,
		"UserId" uuid ,
		"EmployeeId" uuid ,
		"IsDeleted" boolean 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeDevice."Id" , 
			EmployeeDevice."AccessToken" , 
			EmployeeDevice."Location" , 
			EmployeeDevice."Device" , 
			EmployeeDevice."OperatingSystem" , 
			EmployeeDevice."OSVersion" , 
			EmployeeDevice."UserId" , 
			EmployeeDevice."EmployeeId" , 
			EmployeeDevice."IsDeleted"
		 FROM employee."EmployeeDevices" AS EmployeeDevice 
		 WHERE EmployeeDevice."IsDeleted" = False and EmployeeDevice."Id" = id;
	END;
$BODY$
LANGUAGE plpgsql;