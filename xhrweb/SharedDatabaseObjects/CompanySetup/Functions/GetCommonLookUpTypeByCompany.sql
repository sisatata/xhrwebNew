CREATE OR REPLACE FUNCTION main.getcommonlookuptypebycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "ShortCode" character varying, "LookUpTypeName" character varying, "LookUpTypeNameLC" character varying, "Remarks" character varying, "CompanyId" uuid, "ParentId" uuid, "SortOrder" smallint)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
	select * from (
		SELECT 
			CommonLookUpType."Id" , 
			CommonLookUpType."ShortCode" , 
			CommonLookUpType."LookUpTypeName" , 
			CommonLookUpType."LookUpTypeNameLC" , 
			CommonLookUpType."Remarks" , 
			CommonLookUpType."CompanyId" , 
			CommonLookUpType."ParentId" , 
			CommonLookUpType."SortOrder" 
		 FROM main."CommonLookUpTypes" AS CommonLookUpType 
		 WHERE CommonLookUpType."IsDeleted" = False and CommonLookUpType."CompanyId" = companyId and CommonLookUpType."ParentId" is null
		union 
	SELECT 
			CommonLookUpType."Id" , 
			CommonLookUpType."ShortCode" , 
			CommonLookUpType."LookUpTypeName" , 
			CommonLookUpType."LookUpTypeNameLC" , 
			CommonLookUpType."Remarks" , 
			CommonLookUpType."CompanyId" , 
			CommonLookUpType."ParentId" , 
			CommonLookUpType."SortOrder" 
		 FROM main."CommonLookUpTypes" AS CommonLookUpType 
		 WHERE CommonLookUpType."IsDeleted" = False and CommonLookUpType."CompanyId" is null and CommonLookUpType."ParentId" is null ) rru 
		order by rru."SortOrder" ;
	END;
$function$
;
