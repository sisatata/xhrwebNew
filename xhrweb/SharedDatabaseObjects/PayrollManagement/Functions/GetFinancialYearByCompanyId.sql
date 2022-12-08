CREATE OR REPLACE FUNCTION payroll.GetFinancialYearByCompanyId(companyId uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "CompanyName" character varying, "FinancialYearName" character varying, "FinancialYearLocalizedName" character varying, "FinancialYearStartDate" timestamp without time zone, "FinancialYearEndDate" timestamp without time zone, "IsRunningYear" boolean, "SortOrder" smallint)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
		SELECT fy."Id"
			, c."Id" as "CompanyId"
			, c."CompanyName"
			, fy."FinancialYearName"
			, fy."FinancialYearLocalizedName"
			, fy."FinancialYearStartDate"
			, fy."FinancialYearEndDate"
			, fy."IsRunningYear"
			, fy."SortOrder"
		FROM main."FinancialYears" as fy
			INNER JOIN main."Company" as c on fy."CompanyId" = c."Id"
		WHERE fy."IsDeleted" = False and fy."Id" in (select espd."FinancialYearId"  
	from payroll."EmployeeSalaryProcessedDatas" espd where espd."CompanyId"=companyId);
	
END;
$function$
;


--select * from payroll.GetFinancialYearByCompanyId('ab5aeca2-7a4a-4a20-bb96-383e72e839dc');