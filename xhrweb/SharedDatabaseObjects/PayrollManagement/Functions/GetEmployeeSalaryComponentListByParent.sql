CREATE OR REPLACE FUNCTION payroll.getemployeesalarycomponentlistbyparent(employeesalaryid uuid)
 RETURNS TABLE("Id" uuid, "EmployeeSalaryId" uuid, "SalaryStructureComponentId" uuid, "ComponentAmount" numeric, "CompanyId" uuid, "IsDeleted" boolean, "SalaryComponentName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeSalaryComponent."Id" , 
			EmployeeSalaryComponent."EmployeeSalaryId" , 
			EmployeeSalaryComponent."SalaryStructureComponentId" , 
			EmployeeSalaryComponent."ComponentAmount" , 
			EmployeeSalaryComponent."CompanyId" , 
			EmployeeSalaryComponent."IsDeleted" ,
			ssc."SalaryComponentName" 
		 FROM payroll."EmployeeSalaryComponents" AS EmployeeSalaryComponent 
		 inner join payroll."SalaryStructureComponents" ssc  on ssc ."Id" = EmployeeSalaryComponent."SalaryStructureComponentId"
		 WHERE EmployeeSalaryComponent."IsDeleted" = False 
		and EmployeeSalaryComponent."EmployeeSalaryId" = employeeSalaryId order by ssc ."ValueType" desc, ssc."SortOrder" ;
	END;
$function$
;
