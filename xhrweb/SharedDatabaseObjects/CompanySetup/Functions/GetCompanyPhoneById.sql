CREATE OR REPLACE FUNCTION main.getcompanyphonebyid(id uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "PhoneNumber" character varying, "PhoneTypeId" uuid)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			CompanyPhone."Id" , 
			CompanyPhone."CompanyId" , 
			CompanyPhone."PhoneNumber" , 
			CompanyPhone."PhoneTypeId" 
		 FROM main."CompanyPhones" AS CompanyPhone 
		 WHERE CompanyPhone."IsDeleted" = False and CompanyPhone."Id"  = id ;
	END;
$function$
;
