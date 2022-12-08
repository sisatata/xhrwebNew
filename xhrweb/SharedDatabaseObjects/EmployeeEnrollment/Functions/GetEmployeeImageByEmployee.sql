CREATE OR REPLACE FUNCTION employee.getemployeeimagebyemployee(
	employeeid uuid)
RETURNS TABLE("EmployeeImageId" uuid, "EmployeeId" uuid, "FamilyMemberId" uuid, "Photo" text, "PhotoId" uuid, "CompanyId" uuid, "IsDeleted" bit) 
    LANGUAGE 'plpgsql'
    VOLATILE 
    COST 100
    ROWS 1000
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeImage."EmployeeImageId" , 
			EmployeeImage."EmployeeId" , 
			EmployeeImage."FamilyMemberId" , 
			EmployeeImage."Photo" , 
			EmployeeImage."PhotoId" , 
			EmployeeImage."CompanyId" , 
			EmployeeImage."IsDeleted" 
		 FROM employee."EmployeeImage" AS EmployeeImage 
		 WHERE EmployeeImage."IsDeleted" = False AND EmployeeImage."EmployeeId" = employeeId;
	END;
$BODY$;

