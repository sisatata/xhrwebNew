CREATE OR REPLACE FUNCTION payroll.chartgetlastmonthpayroll(companyid uuid, financialyearname character varying, monthcyclename character varying)
 RETURNS TABLE("GrossSalary" numeric, "PayableSalary" numeric, "NetPayableAmount" numeric, "TotalDeductedAmount" numeric)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
SELECT 

cast(SUM(espd."GrossSalary") as numeric ) as "GrossSalary",
cast(SUM(espd."PayableSalary") as numeric )as "PayableSalary",
cast(SUM(espd."NetPayableAmount") as numeric )as "NetPayableAmount",
cast(SUM(espd."TotalDeductionAmount") as numeric )as "TotalDeductedAmount"
from 
(select * from main."FinancialYears" fy where fy."CompanyId" =companyid 
and fy."FinancialYearName"  =financialyearname)as  ff
join main."MonthCycles" mc on mc."FinancialYearId" = ff."Id"
join payroll."EmployeeSalaryProcessedDatas" espd  on espd."MonthCycleId" = mc."Id" 
where mc."MonthCycleName"  =monthcyclename and espd."IsDeleted"  = false and mc."IsDeleted"  = false
group by espd."MonthCycleId"  order by "MonthCycleId"  desc limit 1;
	END;
$function$
;