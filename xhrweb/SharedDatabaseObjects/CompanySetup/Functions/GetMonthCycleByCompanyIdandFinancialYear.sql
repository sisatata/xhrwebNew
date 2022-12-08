CREATE OR REPLACE FUNCTION main.GetMonthCycleByCompanyIdandFinancialYear(compnayid uuid,financialYearId uuid)
RETURNS TABLE(
	"Id" uuid, 
	"CompanyId" uuid, 
	"CompanyName" character varying, 
	"FinancialYearId" uuid,
	"FinancialYearName" character varying,
	"MonthCycleName" character varying, 
	"MonthCycleLocalizedName" character varying, 
	"MonthStartDate" timestamp without time zone,
	"MonthEndDate" timestamp without time zone,
	"TotalWorkingDays" double precision,
	"RunningFlag" boolean,
	"IsSelected" boolean,
	"SortOrder" int,
	"IsDeleted" boolean
) 
AS $$
BEGIN
	RETURN QUERY
		SELECT mc."Id"
			, c."Id" as "CompanyId"
			, c."CompanyName"
			,fy."Id" as "FinancialYearId"
			, fy."FinancialYearName"
			, mc."MonthCycleName"
			, mc."MonthCycleLocalizedName"
			, mc."MonthStartDate"
			, mc."MonthEndDate"
			, mc."TotalWorkingDays"
			, mc."RunningFlag"
			,mc."IsSelected"
			,mc."SortOrder"
			,mc."IsDeleted"
		FROM main."MonthCycles" as mc
		    INNER JOIN main."FinancialYears" as fy ON fy."Id"=mc."FinancialYearId"
			INNER JOIN main."Company" as c on mc."CompanyId" = c."Id"
		WHERE mc."IsDeleted" = False and mc."CompanyId" = compnayId and mc."FinancialYearId"=financialYearId;
	
END;
$$
LANGUAGE plpgsql;
