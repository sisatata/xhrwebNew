           
  CREATE OR REPLACE FUNCTION main.getcompanynameandaddressbycompanyid(companyid uuid)
 RETURNS TABLE("CompanyName" character varying, "CompanyAddress" character varying)
 LANGUAGE plpgsql
AS $function$
BEGIN
	RETURN QUERY
	select c."CompanyName", ca."AddressLine1"  from main."Company" c
	inner join main."CompanyAddresses" as ca on c."Id" = ca."CompanyId"
	where  c."IsDeleted" = false  and ca."IsDeleted" = false and c."Id"= companyid limit  1 ;
	END;
$function$
;
