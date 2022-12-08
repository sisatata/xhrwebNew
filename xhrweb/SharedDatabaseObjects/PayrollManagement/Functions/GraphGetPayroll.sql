CREATE OR REPLACE FUNCTION payroll.graphgetpayroll(companyid uuid, monthcycleid uuid)
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
FROM 
payroll."EmployeeSalaryProcessedDatas" espd 
where espd."IsDeleted"  = false and espd."CompanyId" = companyid
and espd."MonthCycleId" = monthcycleid
group  by espd."MonthCycleId" 
order by "MonthCycleId"  desc limit 1;
	END;
$function$
;