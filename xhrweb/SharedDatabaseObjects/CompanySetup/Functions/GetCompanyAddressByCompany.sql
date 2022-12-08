CREATE OR REPLACE FUNCTION main.getcompanyaddressbycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "AddressLine1" character varying, "AddressLine2" character varying, "Village" character varying, "PostOffice" character varying, "ThanaId" uuid, "DistrictId" uuid, "CountryId" uuid, "Latitude" numeric, "Longitude" numeric, "AddressTypeId" uuid, "IsDeleted" boolean
 ,"DistrictName" character varying,"ThanaName" character varying ,"AddressTypeName" character varying  )
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
			CompanyAddress."IsDeleted" ,
			d."Name" as "DistrictName",
			u."UpazilaName" as "ThanaName",
			addrtype ."LookUpTypeName" as "AddressTypeName"
		 FROM main."CompanyAddresses" AS CompanyAddress inner join main."Districts" d on CompanyAddress."DistrictId" = d."Id" 
		 inner join main."Upazilas" u on CompanyAddress."DistrictId" = u."DistrictId" and CompanyAddress."ThanaId" = u."Id" 
		 left join main."CommonLookUpTypes" addrtype on CompanyAddress."AddressTypeId" = addrtype ."Id" 
		 WHERE CompanyAddress."IsDeleted" = False and CompanyAddress."CompanyId" = companyId;
	END;
$function$
;


 --DROP FUNCTION getcompanyaddressbycompany(uuid)

--select * from main.getcompanyaddressbycompany('ab5aeca2-7a4a-4a20-bb96-383e72e839dc');