CREATE OR REPLACE FUNCTION main.GetSalaryRuleById(id uuid)
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
		 WHERE SalaryRule."IsDeleted" = False and SalaryRule."Id" = id;
	END;
$BODY$
LANGUAGE plpgsql;

