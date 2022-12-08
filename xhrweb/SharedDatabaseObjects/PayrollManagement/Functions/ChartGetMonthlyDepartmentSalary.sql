CREATE OR REPLACE FUNCTION payroll.chartgetmonthlydepartmentsalary(companyid uuid, financialyearid uuid, monthcycleid uuid)
 RETURNS TABLE("Salary" character varying, "DepartmentName" character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
 select cast(sum(espd."NetPayableAmount") as character varying) , d."DepartmentName"  from main."Departments" d 
left join payroll."EmployeeSalaryProcessedDatas" espd on espd."DepartmentId" = d."Id" 
where d."IsDeleted" =false and (espd."IsDeleted" is not true)
group by d."Id" , d."DepartmentName", espd."MonthCycleId"
having d."CompanyId" = companyid and espd."MonthCycleId"  = monthcycleid;
END;
$function$
;
