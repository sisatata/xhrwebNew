CREATE OR REPLACE FUNCTION main.getofficenoticebycompany(companyid uuid)
 RETURNS TABLE("Id" uuid, "CompanyId" uuid, "Subject" character varying, "Details" character varying, "IsGeneral" boolean, "IsSectionSpecific" boolean, "ApplicableSections" text, "PublishDate" timestamp without time zone, "StartDate" timestamp without time zone, "EndDate" timestamp without time zone, "IsDeleted" boolean, "IsPublished" boolean)
 LANGUAGE plpgsql
AS $function$
	BEGIN
	RETURN QUERY
		SELECT 
			OfficeNotice."Id" , 
			OfficeNotice."CompanyId" , 
			OfficeNotice."Subject" , 
			OfficeNotice."Details" , 
			OfficeNotice."IsGeneral" , 
			OfficeNotice."IsSectionSpecific" , 
			OfficeNotice."ApplicableSections" , 
			OfficeNotice."PublishDate" , 
			OfficeNotice."StartDate" , 
			OfficeNotice."EndDate" , 
			OfficeNotice."IsDeleted" , 
			OfficeNotice."IsPublished" 
		 FROM main."OfficeNotices" AS OfficeNotice 
		 WHERE OfficeNotice."IsDeleted" = False and OfficeNotice."CompanyId"  = companyId order by OfficeNotice."PublishDate" desc ;
	END;
$function$
;
