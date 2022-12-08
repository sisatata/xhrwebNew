CREATE OR REPLACE FUNCTION main.getcompanybyid(id uuid)
 RETURNS TABLE("Id" uuid, "CompanyName" character varying, "ShortName" character varying, "CompanyLocalizedName" character varying, "CompanySlogan" character varying, "LicenseKey" character varying, "SortOrder" bigint)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	select c."Id", c."CompanyName", c."ShortName", c."CompanyLocalizedName", c."CompanySlogan", c."LicenseKey", c."SortOrder"
	from main."Company" as c
    where c."IsDeleted" = false and c."Id" = id;
	END;
$function$
;
