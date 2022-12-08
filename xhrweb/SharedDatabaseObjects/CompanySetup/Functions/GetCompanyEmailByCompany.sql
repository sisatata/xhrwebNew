CREATE OR REPLACE FUNCTION main.getcompanyemailbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "EmailAddress" character varying, "Remarks" character varying, "IsPrimary" boolean)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			CompanyEmail."Id" , 
			CompanyEmail."CompanyId" , 
			CompanyEmail."EmailAddress" , 
			CompanyEmail."Remarks" , 
			CompanyEmail."IsPrimary" 
		 FROM main."CompanyEmails" AS CompanyEmail 
		 WHERE CompanyEmail."IsDeleted" = False and CompanyEmail."CompanyId" = companyId;
	END;
$function$
;
