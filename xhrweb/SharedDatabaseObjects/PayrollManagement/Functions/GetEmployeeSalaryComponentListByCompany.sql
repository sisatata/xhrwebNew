CREATE OR REPLACE FUNCTION payroll.GetEmployeeSalaryComponentListByCompany(employeeSalaryId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"EmployeeSalaryId" uuid ,
		"SalaryStructureComponentId" uuid ,
		"ComponentAmount" Decimal ,
		"CompanyId" uuid ,
		"IsDeleted" boolean
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			EmployeeSalaryComponent."Id" , 
			EmployeeSalaryComponent."EmployeeSalaryId" , 
			EmployeeSalaryComponent."SalaryStructureComponentId" , 
			EmployeeSalaryComponent."ComponentAmount" , 
			EmployeeSalaryComponent."CompanyId" , 
			EmployeeSalaryComponent."IsDeleted" 
		 FROM payroll."EmployeeSalaryComponents" AS EmployeeSalaryComponent 
		 WHERE EmployeeSalaryComponent."IsDeleted" = False and EmployeeSalaryComponent."EmployeeSalaryId" = employeeSalaryId;
	END;
$BODY$
LANGUAGE plpgsql;