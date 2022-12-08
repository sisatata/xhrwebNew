CREATE OR REPLACE FUNCTION main.GetConfirmationRuleById(id uuid)
RETURNS TABLE (
		"Id" uuid ,
		"RuleName" character varying(50) ,
		"Description" character varying(500) ,
		"StartDate" timestamp(6) ,
		"EndDate" timestamp(6) ,
		"ConfirmationType" int2 ,
		"ConfirmationMonths" int2 ,
		"SortOrder" int2 ,
		"IsActive" boolean ,
		"IsDeleted" boolean ,
		"CompanyId" uuid 
)
AS $BODY$
	BEGIN
	RETURN QUERY
		SELECT 
			ConfirmationRule."Id" , 
			ConfirmationRule."RuleName" , 
			ConfirmationRule."Description" , 
			ConfirmationRule."StartDate" , 
			ConfirmationRule."EndDate" , 
			ConfirmationRule."ConfirmationType" , 
			ConfirmationRule."ConfirmationMonths" , 
			ConfirmationRule."SortOrder" , 
			ConfirmationRule."IsActive" , 
			ConfirmationRule."IsDeleted" , 
			ConfirmationRule."CompanyId" 
		 FROM main."ConfirmationRules" AS ConfirmationRule 
		 WHERE ConfirmationRule."IsDeleted" = False and ConfirmationRule."Id" = id;
	END;
$BODY$
LANGUAGE plpgsql;

