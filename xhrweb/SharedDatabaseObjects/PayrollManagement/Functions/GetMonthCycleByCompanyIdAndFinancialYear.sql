CREATE OR REPLACE FUNCTION payroll.GetMonthCycleByCompanyIdAndFinancialYear(companyId uuid, financialYearId uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "CompanyName" character varying, "FinancialYearId" uuid, "FinancialYearName" character varying, "MonthCycleName" character varying, "MonthCycleLocalizedName" character varying, "MonthStartDate" timestamp without time zone, "MonthEndDate" timestamp without time zone, "TotalWorkingDays" double precision, "RunningFlag" boolean, "IsSelected" boolean, "SortOrder" integer, "IsDeleted" boolean)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
		SELECT mc."Id"
			, c."Id" as "CompanyId"
			, c."CompanyName"
			, fy."Id" as "FinancialYearId"
			, fy."FinancialYearName"
			, mc."MonthCycleName"
			, mc."MonthCycleLocalizedName"
			, mc."MonthStartDate"
			, mc."MonthEndDate"
			, mc."TotalWorkingDays"
			, mc."RunningFlag"
			, mc."IsSelected"
			, mc."SortOrder"
			, mc."IsDeleted"
		FROM main."MonthCycles" as mc
		    INNER JOIN main."FinancialYears" as fy ON fy."Id"=mc."FinancialYearId"
			INNER JOIN main."Company" as c on mc."CompanyId" = c."Id"
		WHERE mc."IsDeleted" = False and mc."FinancialYearId"=financialYearId and mc."Id" in (
		select espd."MonthCycleId"  from payroll."EmployeeSalaryProcessedDatas" espd 
		where espd."CompanyId" = companyId
		);
	
END;
$function$
;
