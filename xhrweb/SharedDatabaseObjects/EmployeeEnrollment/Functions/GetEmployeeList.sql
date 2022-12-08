-- FUNCTION: employee.getemployeelist()

DROP FUNCTION employee.getemployeelist();

CREATE OR REPLACE FUNCTION employee.getemployeelist(
	)
    RETURNS TABLE("Id" uuid, "EmployeeId" character varying, "CompanyId" uuid, "CompanyName" character varying, "BranchId" uuid
				  , "BranchName" character varying, "DepartmentId" uuid, "DepartmentName" character varying, "PositionId" uuid
				  , "PositionName" character varying, "FullName" character varying, "FullNameLC" character varying
				  , "DateOfBirth" timestamp without time zone, "ReferenceNumber" character varying
				  , "NationalityId" uuid,"NationalityName" character varying ,"GenderId" uuid,"GenderName" character varying) 
    LANGUAGE 'plpgsql'

    
    
AS $BODY$
BEGIN
	RETURN QUERY
	select e."Id", e."EmployeeId", e."CompanyId", c."CompanyName",
	e."BranchId",b."BranchName",e."DepartmentId",d."DepartmentName", 
	e."PositionId",des."DesignationName" as "PositionName",
	e."FullName",e."FullNameLC" as "FullNameLC", e."DateOfBirth",e."ReferenceNumber",
	e."NationalityId", NAT."LookUpTypeName" AS NationalityName, e."GenderId", Gend."LookUpTypeName" AS GenderName
	from employee."Employees" as e
	INNER JOIN main."CommonLookUpTypes" AS NAT ON e."NationalityId" = NAT."Id"
	INNER JOIN main."CommonLookUpTypes" AS Gend ON e."GenderId" = Gend."Id"
	left join main."Company" as c on e."CompanyId" = c."Id"
	left join main."Branch" as b on e."BranchId" = b."Id"

	left join main."Departments" as d on e."DepartmentId" = d."Id"
	left join main."Designations" as des on e."PositionId" = des."Id"

    where e."IsDeleted" = False;
	
END;
$BODY$;


