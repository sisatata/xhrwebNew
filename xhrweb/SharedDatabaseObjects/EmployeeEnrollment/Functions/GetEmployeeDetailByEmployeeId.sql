CREATE OR REPLACE FUNCTION "employee".GetEmployeeDetailByEmployeeId(employeeId uuid)
RETURNS TABLE (
	"Id" uuid,
    "EmployeeId" uuid,
    "FathersName" character varying(50) ,
    "MothersName" character varying(50),
    "SpouseName" character varying(50) ,
    "MaritalStatusId" uuid,
    "ReligionId" uuid,
    "NID" character varying(20) ,
    "BID" character varying(20) ,
    "BloodGroupId" uuid,
    "IsDeleted" boolean 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT ED."Id"
		, ED."EmployeeId"
		, ED."FathersName"
		, ED."MothersName"
		, ED."SpouseName"
		, ED."MaritalStatusId"
		, ED."ReligionId"
		, ED."NID"
		, ED."BID"
		, ED."BloodGroupId"
		, ED."IsDeleted" 
		FROM employee."EmployeeDetails" AS ED
		WHERE ED."EmployeeId" = employeeId AND ED."IsDeleted" = False;
	END;
$BODY$
LANGUAGE plpgsql;