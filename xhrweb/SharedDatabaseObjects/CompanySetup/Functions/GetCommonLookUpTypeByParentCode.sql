CREATE OR REPLACE FUNCTION main.getcommonlookuptypebyparentcode(companyid uuid, parentcode character varying)
 RETURNS TABLE("Id" uuid, "ShortCode" character varying, "LookUpTypeName" character varying, "LookUpTypeNameLC" character varying, "Remarks" character varying, "CompanyId" uuid, "ParentId" uuid, "SortOrder" smallint)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			CommonLookUpType."Id" , 
			CommonLookUpType."ShortCode" , 
			CommonLookUpType."LookUpTypeName" , 
			CommonLookUpType."LookUpTypeNameLC" , 
			CommonLookUpType."Remarks" , 
			CommonLookUpType."CompanyId" , 
			CommonLookUpType."ParentId" , 
			CommonLookUpType."SortOrder" 
		 FROM main."CommonLookUpTypes" AS CommonLookUpType inner join main."CommonLookUpTypes" P on CommonLookUpType."ParentId" = P."Id" 
		 WHERE CommonLookUpType."IsDeleted" = False 
		 and (CommonLookUpType."CompanyId" is null or CommonLookUpType."CompanyId" = companyid)
		 and lower(P."ShortCode")  = lower(parentcode) order by CommonLookUpType."SortOrder",CommonLookUpType."LookUpTypeName" ;
	END;
$function$
;
