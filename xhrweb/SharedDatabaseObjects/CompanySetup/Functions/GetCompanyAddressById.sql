CREATE OR REPLACE FUNCTION main.getcompanyaddressbyid(id uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "AddressLine1" character varying, "AddressLine2" character varying, "Village" character varying, "PostOffice" character varying, "ThanaId" smallint, "DistrictId" smallint, "CountryId" smallint, "Latitude" numeric, "Longitude" numeric, "AddressTypeId" uuid, "IsDeleted" boolean)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			CompanyAddress."Id" , 
			CompanyAddress."CompanyId" , 
			CompanyAddress."AddressLine1" , 
			CompanyAddress."AddressLine2" , 
			CompanyAddress."Village" , 
			CompanyAddress."PostOffice" , 
			CompanyAddress."ThanaId" , 
			CompanyAddress."DistrictId" , 
			CompanyAddress."CountryId" , 
			CompanyAddress."Latitude" , 
			CompanyAddress."Longitude" , 
			CompanyAddress."AddressTypeId" , 
			CompanyAddress."IsDeleted" 
		 FROM main."CompanyAddresses" AS CompanyAddress 
		 WHERE CompanyAddress."IsDeleted" = False and CompanyAddress."Id" = id;
	END;
$function$
;
