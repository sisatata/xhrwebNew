CREATE OR REPLACE FUNCTION employee.getemployeelistbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" character varying, "CompanyId" uuid, "CompanyName" character varying, "BranchId" uuid, "BranchName" character varying, "DepartmentId" uuid, "DepartmentName" character varying, "PositionId" uuid, "PositionName" character varying, "FullName" character varying, "FullNameLC" character varying, "DateOfBirth" timestamp without time zone, "ReferenceNumber" character varying, "NationalityId" uuid, "NationalityName" character varying, "GenderId" uuid, "GenderName" character varying, employeeimageuri text, "GradeId" uuid, "GradeName" character varying, "LoginId" character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	select e."Id", e."EmployeeId", e."CompanyId", c."CompanyName",
	e."BranchId",b."BranchName",e."DepartmentId",d."DepartmentName", 
	e."PositionId",des."DesignationName" as "PositionName",
	e."FullName",e."FullNameLC" as "FullNameLC", e."DateOfBirth",e."ReferenceNumber",
	e."NationalityId", NAT."LookUpTypeName" AS NationalityName, e."GenderId", Gend."LookUpTypeName" AS GenderName,
	ee."EmployeeImageUri" as "EmployeeImageUri",
	ee."GradeId" ,
	g."GradeName" ,
	anu."Email" AS "LoginId"
	from employee."Employees" as e
	INNER JOIN main."CommonLookUpTypes" AS NAT ON e."NationalityId" = NAT."Id"
	INNER JOIN main."CommonLookUpTypes" AS Gend ON e."GenderId" = Gend."Id"
	inner join main."Company" as c on e."CompanyId" = c."Id"
	inner join main."Branch" as b on e."BranchId" = b."Id"
	inner join main."Departments" as d on e."DepartmentId" = d."Id"
	inner join main."Designations" as des on e."PositionId" = des."Id"
	left join employee."EmployeeEnrollments" ee on e."Id" = ee."EmployeeId" 
	left join main."Grades" g on ee."GradeId" =g."Id" 
	left join "access"."AspNetUsers" anu  on  anu."Id" =cast (e."LoginId" as  text) 
    where e."IsDeleted" = false and e."CompanyId" = companyid and anu."Email"  is not null;
	END;
$function$
;
