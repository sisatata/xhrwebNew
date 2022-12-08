CREATE OR REPLACE FUNCTION main.getcompanylogouri(id uuid)
 RETURNS TABLE("Id" uuid, "CompanyLogoUri" text)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	select c."Id", c."CompanyLogoUri"
	from main."Company" as c
    where c."IsDeleted" = false and c."Id" = id;
	END;
$function$
;