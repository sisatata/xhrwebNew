CREATE OR REPLACE FUNCTION main.getfinancialyearidbyyearname(companyid uuid, financialyear character varying)
 RETURNS TABLE("FinancialYearId" uuid, "FinancialYearName" character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
		SELECT 
		fy."Id"  as "FinancialYearId",
		fy."FinancialYearName" 
		FROM main."FinancialYears" as fy	
		WHERE fy."IsDeleted" = False and fy."CompanyId" = companyid and fy."FinancialYearName" = financialyear;
	
END;
$function$
;
