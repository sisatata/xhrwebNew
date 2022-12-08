CREATE OR REPLACE FUNCTION payroll.chartgetlastmonthdepartmentwisesalary(companyid uuid, financialyearname character varying, monthcyclename character varying)
RETURNS TABLE("Salary" character varying, "DepartmentName" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY

select cast(sum(espd."NetPayableAmount") as character varying) , d2."DepartmentName"  
from 
(select * from main."FinancialYears" fy where fy."CompanyId" =companyid
and fy."FinancialYearName"  = financialyearname)as  ff
join main."MonthCycles" mc on mc."FinancialYearId" = ff."Id"
join payroll."EmployeeSalaryProcessedDatas" espd on espd."MonthCycleId" = mc."Id" 
join main."Departments" d2 on espd."DepartmentId" = d2."Id" 
where mc."MonthCycleName"  = monthcyclename and espd."IsDeleted"  = false and mc."IsDeleted"  = false
group by d2."Id" , d2."DepartmentName" , espd."MonthCycleId" 
having d2."CompanyId" = companyid  ;
	END;
$function$
;