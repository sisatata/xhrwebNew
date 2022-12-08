CREATE OR REPLACE FUNCTION main.GetSalaryRuleActiveByCompany(companyId uuid)
RETURNS TABLE (
		"Id" uuid ,
		"RuleName" character varying(50) ,
		"Description" character varying(250) ,
		"StartDate" timestamp(6),
		"EndDate" timestamp(6) ,
		"IsDeleted" boolean ,
		"CompanyId" uuid 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			SalaryRule."Id" , 
			SalaryRule."RuleName" , 
			SalaryRule."Description" , 
			SalaryRule."StartDate" , 
			SalaryRule."EndDate" , 
			SalaryRule."IsDeleted" , 
			SalaryRule."CompanyId" 
		 FROM main."SalaryRules" AS SalaryRule 
		 WHERE SalaryRule."IsDeleted" = False and SalaryRule."CompanyId" = companyId
		and date(SalaryRule."StartDate") <= current_date and  date(SalaryRule."EndDate") >=current_date;
	END;
$BODY$
LANGUAGE plpgsql;

