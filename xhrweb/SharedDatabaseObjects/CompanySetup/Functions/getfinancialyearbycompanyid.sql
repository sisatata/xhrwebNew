CREATE OR REPLACE FUNCTION main.getFinancialYearByCompanyId(compnayid uuid)
RETURNS TABLE(
	"Id" uuid, 
	"CompanyId" uuid, 
	"CompanyName" character varying, 
	"FinancialYearName" character varying,
	"FinancialYearLocalizedName" character varying, 
	"FinancialYearStartDate" timestamp without time zone,
	"FinancialYearEndDate" timestamp without time zone,
	"IsRunningYear" boolean ,
	"SortOrder"  smallint
) 
AS $$
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
		WHERE fy."IsDeleted" = False and fy."CompanyId" = compnayId;
	
END;
$$
LANGUAGE plpgsql;
