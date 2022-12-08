CREATE OR REPLACE FUNCTION main.getmonthcycleidbymonthcyclename(companyid uuid, financialyearid uuid, monthname character varying)
 RETURNS TABLE("MonthCycleName" character varying, "Id" uuid)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
		SELECT 
		mc."MonthCycleName" ,
		mc."Id" 
		FROM main."MonthCycles" as mc   
		WHERE mc."IsDeleted" = False and mc."CompanyId" = companyid and mc."FinancialYearId"=financialYearId and 
		mc."MonthCycleName" = monthname
	order by "SortOrder" ;
	
END;
$function$
;
