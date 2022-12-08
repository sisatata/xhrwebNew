CREATE OR REPLACE FUNCTION payroll.chartgetyearlypayroll(companyid uuid, financialyearid uuid)
 RETURNS TABLE("GrossSalary" character varying, "PayableSalary" character varying, "NetPayableAmount" character varying, "TotalDeductedAmount" character varying)
 LANGUAGE plpgsql
AS $function$
	BEGIN
RETURN QUERY
SELECT 
cast (SUM(espd."GrossSalary") as character varying) as "GrossSalary",
cast(SUM(espd."PayableSalary") as character varying )as "PayableSalary",
cast(SUM(espd."NetPayableAmount") as character varying )as "NetPayableAmount",
cast(SUM(espd."TotalDeductionAmount") as character varying )as "TotalDeductedAmount"
FROM 
payroll."EmployeeSalaryProcessedDatas" espd 
where espd."IsDeleted"  = false and espd."CompanyId" = companyid and espd."FinancialYearId" = financialyearid
group  by "FinancialYearId" 
;
	END;
$function$
;


--DROP FUNCTION payroll.chartgetyearlypayroll(uuid,uuid) 