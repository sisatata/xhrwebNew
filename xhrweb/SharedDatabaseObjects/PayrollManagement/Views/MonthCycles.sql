create or replace
view payroll."EmployeeSalaryComponentVMs" as
select
	sc."Id",
	sc."EmployeeSalaryId",
	sc."SalaryStructureComponentId",
	sc."ComponentAmount",
	sc."CompanyId",
	sc."IsDeleted",
	ssc."SalaryComponentName"  as "DisplayName"
from
	payroll."EmployeeSalaryComponents" sc
inner join payroll."SalaryStructureComponents" ssc on
	sc."SalaryStructureComponentId" = ssc."Id"
where
	sc."IsDeleted" = false
	and ssc."IsDeleted" = false ;