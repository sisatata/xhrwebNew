CREATE OR REPLACE FUNCTION payroll.getmonthlydepartmentsalary(companyid uuid, financialyearid uuid, monthcycleid uuid)
 RETURNS TABLE("Salary" numeric, "DepartmentName" character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
 select sum(espd."GrossSalary") as "Salary" , d."DepartmentName" from payroll."EmployeeSalaryProcessedDatas" espd 
 join main."Departments" d on d."Id" = espd."DepartmentId" 
 where espd."CompanyId" = companyid
 and espd."IsDeleted" =false
 and espd."FinancialYearId" = financialyearid and espd."MonthCycleId" = monthcycleid
 group by espd."DepartmentId" , d."DepartmentName" 
union 
select 0 , d2."DepartmentName" from main."Departments" d2 where d2."CompanyId" =companyid;	
END;
$function$
;
