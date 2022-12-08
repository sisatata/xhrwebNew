CREATE OR REPLACE FUNCTION employee.getemployeeaddressbyid(
	employeeaddressid uuid)
RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "AddressLine1" character varying, "AddressLine2" character varying, "Village" character varying, "PostOffice" character varying, "ThanaId" uuid, "DistrictId" uuid, "CountryId" uuid, "Latitude" numeric, "Longitude" numeric, "AddressTypeId" uuid, "IsDeleted" boolean) 
    LANGUAGE 'plpgsql'
    VOLATILE 
    COST 100
    ROWS 1000
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeAddress."Id" , 
			EmployeeAddress."EmployeeId" , 
			EmployeeAddress."AddressLine1" , 
			EmployeeAddress."AddressLine2" , 
			EmployeeAddress."Village" , 
			EmployeeAddress."PostOffice" , 
			EmployeeAddress."ThanaId" , 
			EmployeeAddress."DistrictId" , 
			EmployeeAddress."CountryId" , 
			EmployeeAddress."Latitude" , 
			EmployeeAddress."Longitude" , 
			EmployeeAddress."AddressTypeId" , 
			EmployeeAddress."IsDeleted" 
		 FROM employee."EmployeeAddresses" AS EmployeeAddress 
		 WHERE EmployeeAddress."IsDeleted" = False AND EmployeeAddress."Id" = employeeAddressId;
	END;
$BODY$;
