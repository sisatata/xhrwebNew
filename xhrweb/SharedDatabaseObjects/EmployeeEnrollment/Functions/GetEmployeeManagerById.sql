CREATE OR REPLACE FUNCTION employee.GetEmployeeManagerById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"EmployeeId" uuid ,
		"ManagerId" uuid ,
		"ManagerName" character varying ,
		"IsPrimaryManager" boolean ,
		"AssignedBy" uuid ,
		"AssignDate" date ,
		"UnAssignedBy" uuid ,
		"UnAssignDate" date ,
		"IsDeleted" boolean ,
		"CompanyId" uuid ,
		"ManagerTypeId" uuid 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeManager."Id" , 
			EmployeeManager."EmployeeId" , 
			EmployeeManager."ManagerId" , 
			Mangr."FullName" as ManagerName,
			EmployeeManager."IsPrimaryManager" , 
			EmployeeManager."AssignedBy" , 
			EmployeeManager."AssignDate" , 
			EmployeeManager."UnAssignedBy" , 
			EmployeeManager."UnAssignDate" , 
			EmployeeManager."IsDeleted" , 
			EmployeeManager."CompanyId" , 
			EmployeeManager."ManagerTypeId" 
		 FROM employee."EmployeeManagers" AS EmployeeManager left join employee."Employees" Mangr on EmployeeManager."ManagerId" = Mangr."Id"
		 WHERE EmployeeManager."IsDeleted" = False and EmployeeManager."Id"   = id;
	END;
$BODY$
LANGUAGE plpgsql;

