CREATE OR REPLACE FUNCTION employee.getemployeemanagerbyemployee(employeeid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeId" uuid, "ManagerId" uuid, "ManagerName" character varying, "IsPrimaryManager" boolean, "AssignedBy" uuid, "AssignDate" timestamp without time zone, "UnAssignedBy" uuid, "UnAssignDate" timestamp without time zone, "IsDeleted" boolean, "CompanyId" uuid, "ManagerTypeId" uuid, "ManagerTypeName" character varying, "EmployeeName" character varying)
 LANGUAGE plpgsql
AS $function$
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
			EmployeeManager."ManagerTypeId" ,
			cl."LookUpTypeName"  as ManagerTypeName,
			emp."FullName" as "EmployeeName"
		 FROM employee."EmployeeManagers" AS EmployeeManager
		 inner join employee."Employees" emp on  EmployeeManager."EmployeeId" = emp."Id" 
		 left join employee."Employees" Mangr on EmployeeManager."ManagerId" = Mangr."Id"
		 left join main."CommonLookUpTypes"  cl on EmployeeManager."ManagerTypeId" = cl."Id" 
		 WHERE EmployeeManager."IsDeleted" = False and EmployeeManager."EmployeeId"   = employeeId;
	END;
$function$
;

--DROP FUNCTION employee.getemployeemanagerbyemployee(uuid) 
--select * from employee.getemployeemanagerbyemployee('e6a94443-8f81-4e15-be9d-3fd178ed4212');